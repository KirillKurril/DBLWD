using DBServiceDemo.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DBServiceDemo.Services
{
	public class DbService
	{
		private readonly AppDbContext _context;
		private List<string> tableNames;
		public DbService(AppDbContext context)
		{
			_context = context;
			string commandText =
@"
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
AND TABLE_NAME != 'sysdiagrams';
";

			var connection = _context.Database.GetDbConnection();
			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					using (var reader = command.ExecuteReader())
					{
						List<string> names = new();
						while (reader.Read())
						{
							names.Add(reader[0]?.ToString() ?? "NULL");
						}
						tableNames = names;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				connection.Close();
			}

		}

		private void ExecuteNPrint(string commandText)
		{
			var connection = _context.Database.GetDbConnection();

			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					using (var reader = command.ExecuteReader())
					{
						var columnNames = new List<string>();
						for (int i = 0; i < reader.FieldCount; i++)
						{
							columnNames.Add(reader.GetName(i));
						}

						Console.WriteLine(string.Join(" | ", columnNames));
						Console.WriteLine(new string('-', columnNames.Count * 15));

						while (reader.Read())
						{
							var row = new List<string>();
							for (int i = 0; i < reader.FieldCount; i++)
							{
								row.Add(reader[i]?.ToString() ?? "NULL");
							}
							Console.WriteLine(string.Join(" | ", row));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				connection.Close();
			}
		}

		private void ReadTablesNames()
		{

		}

		public void Read()
		{
			string tableName = ChooseTable();
			string commandText =
@$"
SELECT *
FROM {tableName}
";
			ExecuteNPrint(commandText);
		}

		public void Write()
		{
			string tableName = ChooseTable();
			string commandText =
$@"
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '{tableName}';
";

			List<(string, bool)> fields = new();
			var connection = _context.Database.GetDbConnection();
			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					using (DbDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine($"" +
								$"field: {reader[0]?.ToString()} | " +
								$"type: {reader[1]?.ToString()} | " +
								$"nullable: {reader[2]?.ToString()}");

							var fieldName = reader.GetString(0);

							var isNullable = reader.GetString(2).Equals("YES", StringComparison.OrdinalIgnoreCase);

							fields.Add((fieldName, isNullable));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				connection.Close();
			}

			List<string> values = new();
			foreach ((string, bool) field in fields.Skip(1))
			{
				if (!field.Item2)
				{
					Console.WriteLine($"\nEnter {field.Item1} value:");
					string argument = Console.ReadLine();
					values.Add($"\'{argument}\'");
				}
				else
				{
					Console.WriteLine(
$@"
Do you wanna define {field.Item1} value?
Enter value if so. Otherwise just press Enter
");
					string argument = Console.ReadLine();
					values.Add(string.IsNullOrEmpty(argument) ? "NULL" : $"\'{argument}\'");
				}
			}

			commandText =
$@"
BEGIN TRY
	BEGIN TRANSACTION;
	INSERT INTO {tableName} ({string.Join(", ", fields.Skip(1).Select(t => t.Item1).ToList())})
	VALUES ({string.Join(", ", values)});
	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH
";

			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					int affectedRows = command.ExecuteNonQuery();

					// Проверка результата вставки
					if (affectedRows > 0)
					{
						Console.WriteLine("Insert succed.");
					}
					else
					{
						Console.WriteLine("Insert failure");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				connection.Close();
			}


		}

		public void Update()
		{
			string tableName = ChooseTable();
			string commandText =
@$"
SELECT *
FROM {tableName}
";
			ExecuteNPrint(commandText);
			Console.WriteLine("\nPlease, enter entity id you wanna update");		
			int upadableId = int.Parse(Console.ReadLine());

			commandText =
$@"
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '{tableName}';
";

			List<string> fields = new();
			var connection = _context.Database.GetDbConnection();
			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					using (DbDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine($"" +
								$"field: {reader[0]?.ToString()} | " +
								$"type: {reader[1]?.ToString()} | " +
								$"nullable: {reader[2]?.ToString()}");

							fields.Add(reader.GetString(0));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				connection.Close();
			}

			List<string> updateArguments = new();
			foreach (string field in fields.Skip(1))
			{

					Console.WriteLine(
$@"
Do you wanna change {field} value?
Enter new value if so. Otherwise just press Enter
");
					string argument = Console.ReadLine();

				if (!string.IsNullOrEmpty(argument))
					updateArguments.Add($"{field} = \'{argument}\'");
				
			}

				commandText =
	$@"
BEGIN TRY
	BEGIN TRANSACTION;
	UPDATE {tableName}
	SET {string.Join(", ", updateArguments)}
	WHERE id = {upadableId};
	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH
";
			Console.WriteLine(commandText);
				try
				{
					connection.Open();

					using (var command = connection.CreateCommand())
					{
						command.CommandText = commandText;
						int affectedRows = command.ExecuteNonQuery();

						if (affectedRows > 0)
						{
							Console.WriteLine("Update succed.");
						}
						else
						{
							Console.WriteLine("Update failure");
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error: " + ex.Message);
				}
				finally
				{
					connection.Close();
				}


			}

			public void Delete()
			{
			string tableName = ChooseTable();
			string commandText =
@$"
SELECT *
FROM {tableName}
";
			ExecuteNPrint(commandText);
			Console.WriteLine("\nPlease, enter entity id you wanna delete");
			int removeId = int.Parse(Console.ReadLine());

			commandText =
$@"
BEGIN TRY
	BEGIN TRANSACTION
	DELETE FROM {tableName}
	WHERE id = {removeId};
	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH
";

			Console.WriteLine(commandText);
			var connection = _context.Database.GetDbConnection();
			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					int affectedRows = command.ExecuteNonQuery();

					if (affectedRows > 0)
					{
						Console.WriteLine("Delition succed.");
					}
					else
					{
						Console.WriteLine("Delition failure");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				connection.Close();
			}


		}

		public void Execute()
		{
			Console.WriteLine("Enter your sql query line by line.\nWhen you are done, enter #");
			string commandText = "";
			while (true)
			{
				string qstr = Console.ReadLine();
				if (qstr == "#")
					break;
				commandText += qstr + '\n';
			}

			Console.WriteLine("\nDo you want to make a selection?\ny\\n\n");
			bool selectionFlag = Console.ReadLine() == "y" ? true : false;

			if(selectionFlag)
			{
				ExecuteNPrint(commandText);
				return;
			}


			Console.WriteLine(commandText);
			var connection = _context.Database.GetDbConnection();
			try
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					int affectedRows = command.ExecuteNonQuery();

					if (affectedRows > 0)
					{
						Console.WriteLine("Your transaction succed.");
					}
					else
					{
						Console.WriteLine("Your transaction failed");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				connection.Close();
			}

		}

		private string ChooseTable()
			{
				while (true)
				{
					Console.WriteLine("Choose the table you need");
					Console.WriteLine(string.Join(" | ", tableNames) + '\n');
					string tableName = Console.ReadLine();
					if (tableNames.Contains(tableName))
					{
						Console.WriteLine($"\nChoosen Table: {tableName}\n");
						return tableName;
					}
					else
						Console.WriteLine($"\nYou don't even try.\n");
				}
			}
		}
	}
