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
                Console.WriteLine("2. Order Service Demonstration");
                Console.WriteLine("3. User Service Demonstration");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var productDemo = new ProductDemonstrationService(_baseUrl);
                        await productDemo.DemonstrateAllMethods();
                        break;
                    case "2":
                        var orderDemo = new OrderDemonstrationService(_baseUrl);
                        await orderDemo.DemonstrateAllMethods();
                        break;
                    case "3":
                        var userDemo = new UserDemonstrationService(_baseUrl);
                        await userDemo.DemonstrateAllMethods();
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
