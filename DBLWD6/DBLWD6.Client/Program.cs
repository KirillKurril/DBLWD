 namespace DBLWD6.Client
{
    internal class Program
    {
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
                        var productDemo = new Services.ProductDemonstrationService();
                        await productDemo.DemonstrateAllMethods();
                        break;
                    case "2":
                        var orderDemo = new Services.OrderDemonstrationService();
                        await orderDemo.DemonstrateAllMethods();
                        break;
                    case "3":
                        var userDemo = new Services.UserDemonstrationService();
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
