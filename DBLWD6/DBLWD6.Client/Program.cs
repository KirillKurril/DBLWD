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
                Console.WriteLine("1. Product Service");
                Console.WriteLine("2. Category Service");
                Console.WriteLine("3. Manufacturer Service");
                Console.WriteLine("4. Supplier Service");
                Console.WriteLine("5. Partner Service");
                Console.WriteLine("6. PickupPoint Service");
                Console.WriteLine("7. Order Service");
                Console.WriteLine("8. User Service");
                Console.WriteLine("9. PromoCode Service");
                Console.WriteLine("10. Vacancy Service");
                Console.WriteLine("11. Article Service");
                Console.WriteLine("12. FAQ Service");
                Console.WriteLine("13. Review Service");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var productService = new ProductService(_baseUrl);
                        await productService.DemonstrateAllMethods();
                        break;
                    case "2":
                        var categoryService = new CategoryService(_baseUrl);
                        await categoryService.DemonstrateAllMethods();
                        break;
                    case "3":
                        var manufacturerService = new ManufacturerService(_baseUrl);
                        await manufacturerService.DemonstrateAllMethods();
                        break;
                    case "4":
                        var supplierService = new SupplierService(_baseUrl);
                        await supplierService.DemonstrateAllMethods();
                        break;
                    case "5":
                        var partnerService = new PartnerService(_baseUrl);
                        await partnerService.DemonstrateAllMethods();
                        break;
                    case "6":
                        var pickupPointService = new PickupPointService(_baseUrl);
                        await pickupPointService.DemonstrateAllMethods();
                        break;
                    case "7":
                        var orderService = new OrderService(_baseUrl);
                        await orderService.DemonstrateAllMethods();
                        break;
                    case "8":
                        var userService = new UserService(_baseUrl);
                        await userService.DemonstrateAllMethods();
                        break;
                    case "9":
                        var promoCodeService = new PromoCodeService(_baseUrl);
                        await promoCodeService.DemonstrateAllMethods();
                        break;
                    case "10":
                        var vacancyService = new VacancyService(_baseUrl);
                        await vacancyService.DemonstrateAllMethods();
                        break;
                    case "11":
                        var articleService = new ArticleService(_baseUrl);
                        await articleService.DemonstrateAllMethods();
                        break;
                    case "12":
                        var faqService = new FAQService(_baseUrl);
                        await faqService.DemonstrateAllMethods();
                        break;
                    case "13":
                        var reviewService = new ReviewService(_baseUrl);
                        await reviewService.DemonstrateAllMethods();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
