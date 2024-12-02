using DBLWD6.CustomORM.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DBLWD6.API.Services
{
    public class DbService
    {
        string _dbName;
        string _connStr;
        public TableWrapper<Article> ArticleTable { get; set; }
        public TableWrapper<Category> CategoryTable { get; set; }
        public TableWrapper<FAQ> FAQTable { get; set; }
        public TableWrapper<Manufacturer> ManufacturerTable { get; set; }
        public TableWrapper<Order> OrderTable { get; set; }
        public TableWrapper<Partner> PartnerTable { get; set; }
        public TableWrapper<PickupPoint> PickupPointTable { get; set; }
        public TableWrapper<Product> ProductTable { get; set; }
        public TableWrapper<ProductPickupPoint> ProductPickupPointTable { get; set; }
        public TableWrapper<Profile> ProfileTable { get; set; }
        public TableWrapper<PromoCode> PromoCodeTable { get; set; }
        public TableWrapper<Review> ReviewTable { get; set; }
        public TableWrapper<Supplier> SupplierTable { get; set; }
        public TableWrapper<Supply> SupplyTable { get; set; }
        public TableWrapper<User> UserTable { get; set; }
        public TableWrapper<Vacancy> VacancyTable { get; set; }
        public DbService(string connectionString, string dbName, string connStr)
        {
            _dbName = dbName;
            _connStr = connStr;
            ArticleTable = new(dbName, connectionString);
            CategoryTable = new(dbName, connectionString);
            FAQTable = new(dbName, connectionString);
            ManufacturerTable = new(dbName, connectionString);
            OrderTable = new(dbName, connectionString);
            PartnerTable = new(dbName, connectionString);
            PickupPointTable = new(dbName, connectionString);
            ProductTable = new(dbName, connectionString);
            ProductPickupPointTable = new(dbName, connectionString);
            ProfileTable = new(dbName, connectionString);
            PromoCodeTable = new(dbName, connectionString);
            ReviewTable = new(dbName, connectionString);
            SupplierTable = new(dbName, connectionString);
            SupplyTable = new(dbName, connectionString);
            UserTable = new(dbName, connectionString);
            VacancyTable = new(dbName, connectionString);
        }
        public async Task InitDBConnection()
        {
            await ArticleTable.InitAsync();
            await CategoryTable.InitAsync();
            await FAQTable.InitAsync();
            await ManufacturerTable.InitAsync();
            await OrderTable.InitAsync();
            await PartnerTable.InitAsync();
            await PickupPointTable.InitAsync();
            await ProductTable.InitAsync();
            await ProductPickupPointTable.InitAsync();
            await ProfileTable.InitAsync();
            await PromoCodeTable.InitAsync();
            await ReviewTable.InitAsync();
            await SupplierTable.InitAsync();
            await SupplyTable.InitAsync();
            await UserTable.InitAsync();
            await VacancyTable.InitAsync();
        }

        public async Task SetTrigger()
        {
            string triggerQuery = $"""
                            USE {_dbName};

                            IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'tr_DecreaseProductQuantity')
                                BEGIN
                                    EXEC('
                                        CREATE TRIGGER tr_DecreaseProductQuantity
                                        ON [Order]
                                        AFTER INSERT
                                        AS
                                        BEGIN
                                            UPDATE p
                                            SET p.[Count] = p.[Count] - i.[Quantity]
                                            FROM Product p
                                            INNER JOIN inserted i ON p.Id = i.ProductId;
                                        END;
                                    ');
                                END;


                            IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'tr_CheckProductQuantity')
                                BEGIN
                                   EXEC('
                                        CREATE TRIGGER tr_CheckProductQuantity
                                        ON [Order]
                                        INSTEAD OF INSERT
                                        AS
                                        BEGIN
                                            IF EXISTS (
                                                SELECT 1
                                                FROM inserted i
                                                JOIN Product p ON i.ProductId = p.Id
                                                WHERE i.[Quantity] > p.[Count]
                                            )
                                            BEGIN
                                                THROW 50001, ''Cannot create order: Requested quantity exceeds available stock'', 1;
                                                RETURN;
                                            END

                                            INSERT INTO [Order] (
                                                [Date],
                                                [Quantity],
                                                [ProductId],
                                                [UserId],
                                                [PickupPointId],
                                                [PromoCodeId]
                                            )
                                            SELECT 
                                                [Date],
                                                [Quantity],
                                                [ProductId],
                                                [UserId],
                                                [PickupPointId],
                                                [PromoCodeId]
                                            FROM inserted;
                                        END;
                                    ');
                                END;

                            
                            IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'tr_ProductExistCheck')
                                BEGIN   
                                    EXEC('
                                        CREATE TRIGGER tr_ProductExistCheck
                                        ON Product
                                        INSTEAD OF UPDATE, DELETE
                                        AS
                                        BEGIN
                                            IF EXISTS (
                                                SELECT 1
                                                FROM deleted d
                                                LEFT JOIN Product p ON d.Id = p.Id
                                                WHERE p.Id IS NULL
                                            )
                                            BEGIN
                                                THROW 50002, ''Product ID being deleted does not exist'', 1;
                                                RETURN;
                                            END

                                            IF EXISTS (
                                                SELECT 1
                                                FROM inserted i
                                                LEFT JOIN Product p ON i.Id = p.Id
                                                WHERE p.Id IS NULL
                                            )
                                            BEGIN
                                                THROW 50003, ''Product ID being updated does not exist'', 1;
                                                RETURN;
                                            END

                                            IF EXISTS (SELECT 1 FROM inserted) 
                                            BEGIN
                                                UPDATE p
                                                SET 
                                                    [Name] = i.[Name],
                                                    ArticleNumber = i.ArticleNumber,
                                                    [Description] = i.[Description],
                                                    PricePerUnit = i.PricePerUnit,
                                                    [Image] = i.[Image],
                                                    CategoryId = i.CategoryId,
                                                    [Count] = i.[Count]
                                                FROM Product p
                                                INNER JOIN inserted i ON p.Id = i.Id;
                                            END
                                            ELSE 
                                            BEGIN
                                                DELETE p
                                                FROM Product p
                                                INNER JOIN deleted d ON p.Id = d.Id;
                                            END
                                        END;
                                    ');
                                END;
                            """;

            using (var connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = triggerQuery;
                    Console.WriteLine(triggerQuery, "Initialize Table and Procedures");
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Triggers initialized successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Triggers initializing error: {ex.Message}", triggerQuery);
                    }
                }
            }
        }
    }
}
