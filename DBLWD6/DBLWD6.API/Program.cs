using DBLWD6.API.Services;

namespace DBLWD6.API
{
    public class Program
    {
        public async static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();
            await InitializeDatabase(app);
            ConfigureMiddleware(app);
            ConfigureEndpoints(app);

            app.Run();
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddSingleton<DbService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IFAQService, FAQService>();
            builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPickupPointService, PickupPointService>();
            builder.Services.AddScoped<IPartnerService, PartnerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPromoCodeService, PromoCodeService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static async Task InitializeDatabase(WebApplication app)
        {
            var connStr = app.Configuration.GetConnectionString("MicrosoftSQLServer");
            DbService dbService = new DbService(connStr, "DBLWD6");
            await dbService.InitDBConnection();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
        }
        private static void ConfigureEndpoints(WebApplication app)
        {
            app.MapControllers();
        }
    }
}