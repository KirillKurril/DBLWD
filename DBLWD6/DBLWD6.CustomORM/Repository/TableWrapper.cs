using DBLWD6.CustomORM.Model;
using DBLWD6.CustomORM.Services;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Data;
using System.Reflection;


namespace DBLWD6.CustomORM.Repository
{
    public class TableWrapper<T> : ITableWrapper<T>
    {
        string _accessString;
        string _dbName;
        string _tableName;
        string _addProcedureName;
        string _updateProcedureName;
        string _deleteProcedureName;
        string _selectByIdProcedureName;
        string _selectByConditionsProcedureName;
        string _initQuery;
        PropertyInfo[] _modelProperties;
        public TableWrapper(string dbName, string accessString)
        {
            _dbName = dbName;
            _accessString = accessString;
            _modelProperties = typeof(T).GetProperties();
            SQLEntity createTable = Converter<T>.GetCreateTableQuery();
            SQLEntity addProcedure = Converter<T>.GetAddProcedure();
            SQLEntity updateProcedure = Converter<T>.GetUpdateProcedure();
            SQLEntity deleteProcedure = Converter<T>.GetDeleteProcedure();
            SQLEntity selectByIdProcedure = Converter<T>.GetSelectByIdProcedure();
            SQLEntity selectByConditionsProcedure = Converter<T>.GetSelectByConditionsProcedure();

            _tableName = createTable.Name;
            _addProcedureName = addProcedure.Name;
            _updateProcedureName = updateProcedure.Name;
            _deleteProcedureName = deleteProcedure.Name;
            _selectByIdProcedureName = selectByIdProcedure.Name;
            _selectByConditionsProcedureName = selectByConditionsProcedure.Name;

             _initQuery =
                $"""
                BEGIN TRY
                    BEGIN TRANSACTION;
                        USE {_dbName};
                        GO
                        {createTable.Query}
                        GO
                        {addProcedure.Query}
                        GO
                        {deleteProcedure.Query}
                        GO
                        {selectByIdProcedure.Query}
                        GO
                        {selectByConditionsProcedure.Query}
                        GO
                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                END CATCH;
                """;
        }

        public async Task<TableWrapper<T>> CreateWrapperAsync(string dbName, string accessString)
        {
            TableWrapper<T> wrapper = new (dbName, accessString);

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _initQuery;
                    try
                    {
                        await command.ExecuteNonQueryAsync(); 
                        Console.WriteLine($"Table {_tableName} initilized successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Table {_tableName} initilizing error: {ex.Message}");
                        Console.WriteLine(_initQuery);
                        throw; 
                    }
                }
            }
            return wrapper;
        }
        public async Task Add(T objectToAdd)
        {
            var parameters = _modelProperties
                .Where(prop => prop.GetValue(objectToAdd) != null)
                .Select(prop =>
                {
                    var value = prop.GetValue(objectToAdd);
                    string valueStr = value?.ToString() ?? string.Empty;

                    if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Guid))
                        return $"@{prop.Name}Var = '{valueStr}'";
                    else
                        return $"@{prop.Name}Var = {valueStr}";
                })
                .ToArray();

            string addQuery =
                $"""
                USE {_tableName};
                GO

                EXECUTE {_addProcedureName}
                    {string.Join(", ", parameters)}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = addQuery;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Data {typeof(T)} type added successfully to {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while trying to add {typeof(T)} entity to {_dbName} db.\nAdding error: {ex.Message}");
                        Console.WriteLine(addQuery);
                        throw;
                    }
                }
            }
        }
        public async Task Update(T newObject, int prevObjectId)
        {
            var parameters = _modelProperties
                .Where(prop => prop.GetValue(newObject) != null)
                .Select(prop =>
                {
                    var value = prop.GetValue(newObject);
                    string valueStr = value?.ToString() ?? string.Empty;

                    if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Guid))
                        return $"@{prop.Name}Var = '{valueStr}'";
                    else
                        return $"@{prop.Name}Var = {valueStr}";
                })
                .ToArray();

            string updateQuery =
                $"""
                USE {_tableName};
                GO

                EXECUTE {_updateProcedureName}
                    @IdVar = {prevObjectId},
                    {string.Join(", ", parameters)}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Data {typeof(T)} type updated successfully in {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while trying to update {typeof(T)} entity in {_dbName} db.\nUpdate error: {ex.Message}");
                        Console.WriteLine(updateQuery);
                        throw;
                    }
                }
            }
        }

        public async Task Delete(int objectToDeletId)
        {
            string deleteQuery =
                $"""
                USE {_tableName};
                GO

                EXECUTE {_deleteProcedureName}
                    @IdVar = {objectToDeletId}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Data {typeof(T)} type deleted successfully from {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while trying to delete {typeof(T)} entity from {_dbName} db.\nDelete error: {ex.Message}");
                        Console.WriteLine(deleteQuery);
                        throw;
                    }
                }
            }
        }

        public async Task<T> GetById(int id)
        {
            string selectQuery =
                $"""
                USE {_tableName};
                GO

                EXECUTE {_selectByIdProcedureName}
                    @IdVar = {id}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = Activator.CreateInstance<T>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var property = _modelProperties.FirstOrDefault(p => p.Name.Equals(reader.GetName(i), StringComparison.OrdinalIgnoreCase));
                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        property.SetValue(result, reader.GetValue(i));
                                    }
                                }
                                return result;
                            }
                            else
                            {
                                return default;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while trying to fetch data by ID {id} for {typeof(T)}.\nGet error: {ex.Message}");
                        Console.WriteLine(selectQuery);
                        throw;
                    }
                }
            }
        }


        public async Task<IEnumerable<T>> GetCollection(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }


    }
}
