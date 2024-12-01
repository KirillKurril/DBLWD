using DBLWD6.CustomORM.Repository;

namespace DBLWD6.API.Services
{
    public class DbService
    {
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
        public DbService(string connectionString, string dbName)
        {
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
    }
}
