using DBLWD6.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DBLWD6.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();
            await InitializeDatabase(app);
            ConfigureMiddleware(app);
            ConfigureEndpoints(app);

            app.Run();

            Console.WriteLine("API started");
            
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            string connStr;
            string dbName;
            try
            {
                //connStr = builder.Configuration.GetConnectionString("MicrosoftSQLServer");
                connStr = builder.Configuration.GetConnectionString("MicrosoftSQLServerMiruku");
                dbName = builder.Configuration.GetSection("DbName").Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"----------> Unable to fetch db configuration strings:\n{ex.Message}");
            }
            
            builder.Services.AddSingleton(provider => new DbService(connStr, dbName, connStr));
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
            builder.Services.AddScoped<IVacancyService, VacancyService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static async Task InitializeDatabase(WebApplication app)
        {
            var dbService = app.Services.GetRequiredService<DbService>();
            await dbService.InitDBConnection();
            await dbService.SetTrigger();
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