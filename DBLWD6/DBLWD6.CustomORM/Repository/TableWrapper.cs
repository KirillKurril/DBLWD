using DBLWD6.CustomORM.Model;
using DBLWD6.CustomORM.Services;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using DBLWD6.CustomORM.Attributes;

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
        string _selectAllProcedureName;
        string _initQuery;
        PropertyInfo[] _modelProperties;
        public TableWrapper(string dbName, string accessString)
        {
            _dbName = dbName;
            _accessString = accessString;
            _modelProperties = typeof(T).GetProperties();
            SQLEntity createTable = Converter<T>.GetCreateTableQuery(dbName);
            SQLEntity addProcedure = Converter<T>.GetAddProcedure(dbName);
            SQLEntity updateProcedure = Converter<T>.GetUpdateProcedure(dbName);
            SQLEntity deleteProcedure = Converter<T>.GetDeleteProcedure(dbName);
            SQLEntity selectByIdProcedure = Converter<T>.GetSelectByIdProcedure(dbName);
            SQLEntity selectAllProcedure = Converter<T>.GetSelectAllProcedure(dbName);

            _tableName = createTable.Name;
            _addProcedureName = addProcedure.Name;
            _updateProcedureName = updateProcedure.Name;
            _deleteProcedureName = deleteProcedure.Name;
            _selectByIdProcedureName = selectByIdProcedure.Name;
            _selectAllProcedureName = selectAllProcedure.Name;

            _initQuery =
                $"""
                USE {_dbName};
                
                BEGIN TRY
                    BEGIN TRANSACTION;
                        {createTable.Query}
                        {addProcedure.Query}
                        {updateProcedure.Query}
                        {deleteProcedure.Query}
                        {selectByIdProcedure.Query}
                        {selectAllProcedure.Query}
                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                END CATCH;
                """;

            Logger.LogQuery(_initQuery,$"Table {_tableName} creation string");
        }
        public async Task InitAsync()
        {
            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _initQuery;
                    Logger.LogQuery(_initQuery, "Initialize Table and Procedures");
                    try
                    {
                        await command.ExecuteNonQueryAsync(); 
                        Logger.LogSuccess($"Table {_tableName} initialized successfully.");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Table {_tableName} initializing error: {ex.Message}", _initQuery);
                        throw; 
                    }
                }
            }
        }
        public async Task Add(T objectToAdd)
        {
            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _addProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var prop in _modelProperties.Where(p => !p.GetCustomAttributes(true).Any(attr => attr is PrimaryKey)))
                    {
                        var value = prop.GetValue(objectToAdd);
                        if (value != null)
                        {
                            var param = command.CreateParameter();
                            param.ParameterName = $"@{prop.Name}Var";
                            param.Value = value;
                            command.Parameters.Add(param);
                        }
                    }

                    Logger.LogQuery(command.CommandText, "Add Entity");
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Logger.LogSuccess($"Data {typeof(T)} type added successfully to {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to add {typeof(T)} entity to {_dbName} db.\nAdding error: {ex.Message}", command.CommandText);
                        throw;
                    }
                }
            }
        }
        public async Task Update(T newObject, int prevObjectId)
        {
            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = _updateProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var prop in _modelProperties.Where(p => p.GetValue(newObject) != null && !p.GetCustomAttributes(true).Any(attr => attr is PrimaryKey)))
                    {
                        var value = prop.GetValue(newObject);
                        var param = command.CreateParameter();
                        param.ParameterName = $"@{prop.Name}Var";
                        param.Value = value;
                        command.Parameters.Add(param);
                    }

                    Logger.LogQuery(command.CommandText, "Update Entity");
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Logger.LogSuccess($"Data {typeof(T)} type updated successfully in {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to update {typeof(T)} entity in {_dbName} db.\nUpdate error: {ex.Message}", command.CommandText);
                        throw;
                    }
                }
            }
        }
        public async Task Delete(int objectToDeletId)
        {
            string deleteQuery =
                $"""
                EXECUTE {_deleteProcedureName}
                    @IdVar = {objectToDeletId}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    Logger.LogQuery(deleteQuery, "Delete Entity");
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Logger.LogSuccess($"Data {typeof(T)} type deleted successfully from {_dbName} db");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to delete {typeof(T)} entity from {_dbName} db.\nDelete error: {ex.Message}", deleteQuery);
                        throw;
                    }
                }
            }
        }
        public async Task<T> GetById(int id)
        {
            string selectQuery =
                $"""
                EXECUTE {_selectByIdProcedureName}
                    @IdVar = {id}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    Logger.LogQuery(selectQuery, "Get Entity By Id");
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
                                Logger.LogSuccess($"Successfully retrieved {typeof(T)} with ID {id}");
                                return result;
                            }
                            else
                            {
                                Logger.LogSuccess($"No {typeof(T)} found with ID {id}");
                                return default;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to fetch data by ID {id} for {typeof(T)}.\nGet error: {ex.Message}", selectQuery);
                        throw;
                    }
                }
            }
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            string selectQuery =
                $"""
                    EXECUTE {_selectAllProcedureName}
                """;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    Logger.LogQuery(selectQuery, "Get All Entities");
                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<T> selectedEntities = new();
                            while (await reader.ReadAsync())
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
                                selectedEntities.Add(result);
                            }
                            Logger.LogSuccess($"Successfully retrieved {selectedEntities.Count} {typeof(T)} entities");
                            return selectedEntities;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to fetch all {typeof(T)} entities.\nGet error: {ex.Message}", selectQuery);
                        throw;
                    }
                }
            }
        }
        public async Task<IEnumerable<T>> GetWithConditions(Expression<Func<T, bool>>? predicate)
        {
            if (predicate == null)
            {
                return await GetAll();
            }

            var selectByConditions = Converter<T>.GetSelectByConditionsQuery(_dbName, predicate);
            string selectQuery = selectByConditions.Query;

            using (var connection = new SqlConnection(_accessString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    Logger.LogQuery(selectQuery, "Get Entities With Conditions");
                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<T> selectedEntities = new();
                            while (await reader.ReadAsync())
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
                                selectedEntities.Add(result);
                            }
                            Logger.LogSuccess($"Successfully retrieved {selectedEntities.Count} {typeof(T)} entities matching conditions");
                            return selectedEntities;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error while trying to fetch {typeof(T)} entities with conditions.\nGet error: {ex.Message}", selectQuery);
                        throw;
                    }
                }
            }
        }
    }
}
