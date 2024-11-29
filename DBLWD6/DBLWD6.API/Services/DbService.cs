using DBLWD6.API.Entities;
using DBLWD6.CustomORM.Repository;

namespace DBLWD6.API.Services
{
    public class DbService
    {
        TableWrapper<Category> CategoryTable { get; set; }
        TableWrapper<FAQ> FAQTable { get; set; }
        TableWrapper<Manufacturer> ManufacturerTable { get; set; }
        TableWrapper<Partner> PartnerTable { get; set; }
        TableWrapper<PickupPoint> PickupPointTable { get; set; }
        TableWrapper<Product> ProductTable { get; set; }
        TableWrapper<ProductPickupPoint> ProductPickupPointTable { get; set; }
        TableWrapper<Profile> ProfileTable { get; set; }
        TableWrapper<PromoCode> PromoCodeTable { get; set; }
        TableWrapper<Review> ReviewTable { get; set; }
        TableWrapper<Supplier> SupplierTable { get; set; }
        TableWrapper<Supply> SupplyTable { get; set; }
        TableWrapper<User> UserTable { get; set; }
        public DbService(string connectionString, string dbName)
        {
            CategoryTable = new(dbName, connectionString);
            FAQTable = new(dbName, connectionString);
            ManufacturerTable = new(dbName, connectionString);
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
        }
        public async void InitDBConnection()
        {
            await CategoryTable.InitAsync();
        }
    }
}
