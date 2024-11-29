using DBLWD6.CustomORM.Model;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DBLWD6.CustomORM.Services
{
    public static class Converter<T>
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
                StringBuilder columnDeclaration = new();
                columnDeclaration.Append(modelProperty.Name).Append(" ");
                columnDeclaration.Append(mapper.GetTSQLType(modelProperty.GetType()));

                object[] attributes = modelProperty.GetCustomAttributes(true);

                if (attributes.Count() != 0)
                    columnDeclaration.Append(" ");

                foreach (var attribute in attributes)
                {
                    if (AttributeService.IsForeignKey(attribute))
                    {
                        if(foreignKeys.Length > 0)
                            foreignKeys.Append(",\n");

                        foreignKeys.Append(
                            AttributeService
                            .GetForeignKeyExpression(attribute, modelProperty.Name));
                    }
                    else
                        columnDeclaration.Append(
                            AttributeService
                            .GetConstraintAsString(attribute));
                }

                if (i < modelProperties.Length - 1)
                    columnDeclaration.Append(",\n");
                else
                    columnDeclaration.Append("\n");
                
                tableColumns.Append(columnDeclaration);
            }



            string tableName = typeof(T).Name;

            string createTableQuery = $"""
                                       USE {dbName};
                                       GO

                                       CREATE TABLE [{tableName}](
                                       {tableColumns.ToString()}
                                       );
                                       GO
                                       """;
            return new SQLEntity(tableName, createTableQuery);
        }
        public static SQLEntity GetAddProcedure(string dbName)
        {
            return new SQLEntity();
        }
        public static SQLEntity GetUpdateProcedure(string dbName)
        {
            return new SQLEntity();
        }
        public static SQLEntity GetDeleteProcedure(string dbName)
        {
            return new SQLEntity();
        }
        public static SQLEntity GetSelectByIdProcedure(string dbName)
        {
            return new SQLEntity();
        }
        public static SQLEntity GetSelectByConditionsProcedure(string dbName)
        {
            return new SQLEntity();
        }
        public static SQLEntity GetSelectAllProcedure(string dbName, Expression<Func<T, bool>> predicate)
        {
            return new SQLEntity();
        }
    }
}
