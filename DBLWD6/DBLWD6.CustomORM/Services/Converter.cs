using DBLWD6.CustomORM.Attributes;
using DBLWD6.CustomORM.Model;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DBLWD6.CustomORM.Services
{
    internal static class Converter<T>
    {        
        public static SQLEntity GetCreateTableQuery(string dbName)
        {
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            StringBuilder tableColumns = new();
            SharpToTSQLTypeMapper mapper = new();

            StringBuilder foreignKeys = new();

            for (int i = 0; i < modelProperties.Length; i++)
            {
                PropertyInfo modelProperty = modelProperties[i];
                bool nonMapped = modelProperty.GetCustomAttributes(true).Any(a => a is NonMapped);
                if (nonMapped)
                    continue;

                StringBuilder columnDeclaration = new();
                columnDeclaration.Append(modelProperty.Name).Append(" ");
                columnDeclaration.Append(mapper.GetTSQLType(modelProperty.PropertyType));

                object[] attributes = modelProperty.GetCustomAttributes(true)
                    .Where(a => typeof(ICustomORMAttribute).IsAssignableFrom(a.GetType()))
                    .ToArray();

                if (attributes.Count() != 0)
                    columnDeclaration.Append(" ");

                int notFKConstraintsCounter = 0;
                for(int j = 0; j < attributes.Count(); j++)
                {
                    var attribute = attributes[j];
                    if (AttributeService.IsForeignKey(attribute))
                    {
                        if(foreignKeys.Length > 0)
                            foreignKeys.Append(",\n");

                        foreignKeys.Append(
                            AttributeService
                            .GetForeignKeyExpression(attribute, modelProperty.Name));
                        if(j < attributes.Count() - 1)
                            foreignKeys.Append(" ");
                    }
                    else
                    {
                        if (notFKConstraintsCounter++ != 0)
                            columnDeclaration.Append(" ");

                        columnDeclaration.Append(
                            AttributeService
                            .GetConstraintAsString(attribute));
                    }
                }

                if (i < modelProperties.Length - 1)
                    columnDeclaration.Append(",\n");
                else
                    columnDeclaration.Append("\n");

                string columnDeclarationAsString = columnDeclaration.ToString();
                if (columnDeclarationAsString.Contains("UNIQUE") && columnDeclarationAsString.Contains("VARCHAR(MAX)"))
                    columnDeclaration.Replace("VARCHAR(MAX)", "VARCHAR(255)");





                tableColumns.Append(columnDeclaration);
            }
            tableColumns.Append(foreignKeys);

            string tableName = typeof(T).Name;

            string createTableQuery = $"""
                                               IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}' AND type = 'U')
                                               BEGIN
                                                   CREATE TABLE [{tableName}](
                                                       {tableColumns.ToString()}
                                                   );
                                               END;
                                       """;



            return new SQLEntity(tableName, createTableQuery);
        }
        public static SQLEntity GetAddProcedure(string dbName)
        {
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            SharpToTSQLTypeMapper mapper = new();
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_Add{tableName}";

            StringBuilder parameters = new();
            StringBuilder columnNames = new();
            StringBuilder values = new();

            for (int i = 0; i < modelProperties.Length; i++)
            {
                PropertyInfo property = modelProperties[i];
                bool nonMapped = property.GetCustomAttributes(true).Any(a => a is NonMapped);
                if (nonMapped)
                    continue;

                bool isIdentity = property.GetCustomAttributes(true).Any(attr => attr is Identity);

                if (!isIdentity)
                {
                    string paramName = $"@{property.Name}Var";
                    string sqlType = mapper.GetTSQLType(property.PropertyType);
                    bool isNotNull = property.GetCustomAttributes(true).Any(attr => attr is NonNull);

                    if (parameters.Length > 0)
                    {
                        parameters.Append(",\n\t");
                        columnNames.Append(",\n\t");
                        values.Append(",\n\t");
                    }

                    parameters.Append(paramName).Append(" ").Append(sqlType);
                    if (!isNotNull)
                    {
                        parameters.Append(" = NULL");
                    }
                    columnNames.Append(property.Name);
                    values.Append(paramName);
                }
            }

            string addProcedureQuery = $"""
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    (
                        {parameters}
                    )
                    AS
                    BEGIN
                        INSERT INTO [{tableName}]
                        (
                            {columnNames}
                        )
                        VALUES
                        (
                            {values}
                        );
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, addProcedureQuery);
        }
        public static SQLEntity GetUpdateProcedure(string dbName)
        {
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_Update{tableName}";

            var primaryKeyProperty = modelProperties.FirstOrDefault(
                prop => prop.GetCustomAttributes(true).Any(attr => attr is PrimaryKey));

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"No primary key found in {tableName}. Cannot create update procedure.");

            SharpToTSQLTypeMapper mapper = new();
            StringBuilder parameters = new();
            StringBuilder setStatements = new();

            foreach (var property in modelProperties)
            {
                bool nonMapped = property.GetCustomAttributes(true).Any(a => a is NonMapped);
                if (nonMapped)
                    continue;

                bool isIdentity = property.GetCustomAttributes(true).Any(attr => attr is Identity);
                bool isPrimaryKey = property.GetCustomAttributes(true).Any(attr => attr is PrimaryKey);
                
                if (!isIdentity)  
                {
                    string paramName = $"@{property.Name}Var";
                    string sqlType = mapper.GetTSQLType(property.PropertyType);
                    bool isNotNull = property.GetCustomAttributes(true).Any(attr => attr is NonNull);

                    if (parameters.Length > 0)
                    {
                        parameters.Append(",\n\t");
                        if (!isPrimaryKey)
                            setStatements.Append(",\n\t");
                    }

                    parameters.Append(paramName).Append(" ").Append(sqlType);
                    if (!isNotNull && !isPrimaryKey)  
                    {
                        parameters.Append(" = NULL");
                    }

                    if (!isPrimaryKey)  
                    {
                        setStatements.Append(property.Name).Append(" = ").Append(paramName);
                    }
                }
            }

            string updateProcedureQuery = $"""
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    (
                        {parameters}
                    )
                    AS
                    BEGIN
                        UPDATE [{tableName}]
                        SET
                            {setStatements}
                        WHERE {primaryKeyProperty.Name} = @{primaryKeyProperty.Name}Var;
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, updateProcedureQuery);
        }
        public static SQLEntity GetDeleteProcedure(string dbName)
        {
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_Delete{tableName}";


            var primaryKeyProperty = modelProperties.FirstOrDefault(
                prop => prop.GetCustomAttributes(true).Any(attr => attr is PrimaryKey));

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"No primary key found in {tableName}. Cannot create delete procedure.");

            SharpToTSQLTypeMapper mapper = new();
            string paramName = $"@{primaryKeyProperty.Name}Var";
            string sqlType = mapper.GetTSQLType(primaryKeyProperty.PropertyType);

            string deleteProcedureQuery = $"""
            USE {dbName};

            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    (
                        {paramName} {sqlType}
                    )
                    AS
                    BEGIN
                        DELETE FROM [{tableName}]
                        WHERE {primaryKeyProperty.Name} = {paramName};
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, deleteProcedureQuery);
        }
        public static SQLEntity GetSelectByIdProcedure(string dbName)
        {
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_SelectById{tableName}";

            var primaryKeyProperty = modelProperties.FirstOrDefault(
                prop => prop.GetCustomAttributes(true).Any(attr => attr is PrimaryKey));

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"No primary key found in {tableName}. Cannot create select by id procedure.");

            SharpToTSQLTypeMapper mapper = new();
            string paramName = $"@{primaryKeyProperty.Name}Var";
            string sqlType = mapper.GetTSQLType(primaryKeyProperty.PropertyType);

            string selectByIdProcedureQuery = $"""
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    (
                        {paramName} {sqlType}
                    )
                    AS
                    BEGIN
                        SELECT *
                        FROM [{tableName}]
                        WHERE {primaryKeyProperty.Name} = {paramName};
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, selectByIdProcedureQuery);
        }
        public static SQLEntity GetSelectAllProcedure(string dbName)
        {
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_SelectAll{tableName}";

            string selectAllProcedureQuery = $"""
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    AS
                    BEGIN
                        SELECT *
                        FROM [{tableName}];
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, selectAllProcedureQuery);
        }
        private static string GetSqlOperator(ExpressionType expressionType)
        {
            return expressionType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "!=",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                _ => throw new InvalidOperationException($"Operator {expressionType} is not supported")
            };
        }
        private static string ProcessExpression(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                string leftSql = ProcessExpression(binaryExpression.Left);
                string rightSql = ProcessExpression(binaryExpression.Right);
                string op = GetSqlOperator(binaryExpression.NodeType);

                if (binaryExpression.NodeType == ExpressionType.AndAlso || binaryExpression.NodeType == ExpressionType.OrElse)
                {
                    return $"({leftSql} {op} {rightSql})";
                }
                return $"{leftSql} {op} {rightSql}";
            }
            else if (expression is MemberExpression memberExpression) 
            {
                return memberExpression.Member.Name;
            }
            else if (expression is ConstantExpression constantExpression)
            {
                if (constantExpression.Value is string stringValue)
                    return $"'{stringValue}'";
                else if (constantExpression.Value is bool boolValue)
                    return boolValue ? "1" : "0";
                return constantExpression.Value?.ToString() ?? "NULL";
            }
            else if (expression is UnaryExpression unaryExpression)
            {
                return ProcessExpression(unaryExpression.Operand);
            }

            throw new InvalidOperationException($"Expression type {expression.GetType().Name} is not supported");
        }
        public static SQLEntity GetSelectByConditionsProcedure(string dbName, Expression<Func<T, bool>> predicate)
        {
            string tableName = typeof(T).Name;
            string procedureName = $"PRC_SelectByConditions{tableName}";

            string whereClause = ProcessExpression(predicate.Body);

            string selectByConditionsProcedureQuery = $"""
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = '{procedureName}' AND type = 'P')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [{procedureName}]
                    AS
                    BEGIN
                        SELECT *
                        FROM [{tableName}]
                        WHERE {whereClause};
                    END
                ')
            END;
            """;

            return new SQLEntity(procedureName, selectByConditionsProcedureQuery);
        }
        public static SQLEntity GetDropTableQuery(string dbName)
        {
            string tableName = typeof(T).Name;
            string createTableQuery = $"""
                                               IF EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}' AND type = 'U')
                                               BEGIN
                                                   DROP TABLE [{tableName}];
                                               END;
                                       """;

            return new SQLEntity(tableName, createTableQuery);
        }
    }
}
