using DBLWD6.Client.Services; 

namespace DBLWD6.Client
{
    internal class Program
    {
        static string _baseUrl = "http://localhost:5010/api/";
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("API Demonstration Client");
                Console.WriteLine("1. Product Service Demonstration");
                Console.WriteLine("2. Manufacturer Service Demonstration");
                Console.WriteLine("3. PickupPoint Service Demonstration");
                Console.WriteLine("4. Order Service Demonstration");
                Console.WriteLine("5. User Service Demonstration");
                Console.WriteLine("6. Article Service Demonstration");
                Console.WriteLine("7. PromoCode Service Demonstration");
                Console.WriteLine("8. Review Service Demonstration");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var productDemo = new ProductService(_baseUrl);
                        await productDemo.DemonstrateAllMethods();
                        break;
                    case "2":
                        var manufacturerService = new ManufacturerService(_baseUrl);
                        await manufacturerService.DemonstrateAllMethods();
                        break;
                    case "3":
                        var pickupPointService = new PickupPointService(_baseUrl);
                        await pickupPointService.DemonstrateAllMethods();
                        break;
                    case "4":
                        var categoryService = new CategoryService(_baseUrl);
                        await categoryService.DemonstrateAllMethods();
                        break;                        
                    case "5":
                        var articleService = new ArticleService(_baseUrl);
                        await articleService.DemonstrateAllMethods();
                        break;
                    case "6":
                        var FAQService = new FAQService(_baseUrl);
                        await FAQService.DemonstrateAllMethods();
                        break;                        
                    case "7":
                        var promoCodeService = new PromoCodeService(_baseUrl);
                        await promoCodeService.DemonstrateAllMethods();
                        break;
                    case "8":
                        var reviewService = new ReviewService(_baseUrl);
                        await reviewService.DemonstrateAllMethods();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
