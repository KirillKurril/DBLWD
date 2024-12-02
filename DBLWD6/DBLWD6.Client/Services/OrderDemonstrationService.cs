using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class OrderDemonstrationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OrderDemonstrationService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Order";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Order Service Demonstration");
                Console.WriteLine("1. Get Orders Collection");
                Console.WriteLine("2. Get Order by ID");
                Console.WriteLine("3. Add Order");
                Console.WriteLine("4. Update Order");
                Console.WriteLine("5. Delete Order");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetOrders();
                        break;
                    case "2":
                        await DemonstrateGetOrderById();
                        break;
                    case "3":
                        await DemonstrateAddOrder();
                        break;
                    case "4":
                        await DemonstrateUpdateOrder();
                        break;
                    case "5":
                        await DemonstrateDeleteOrder();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private async Task DemonstrateGetOrders()
        {
            Console.Clear();
            Console.WriteLine("Get Orders Collection Demonstration");
            Console.WriteLine("1. Use predefined parameters");
            Console.WriteLine("2. Enter custom parameters");
            
            var choice = Console.ReadLine();
            
            int? page = null;
            int? itemsPerPage = null;
            int? userId = null;
            bool? includeProduct = false;
            bool? includeUser = false;
            bool? includePickupPoint = false;
            bool? includePromoCode = false;

            if (choice == "1")
            {
                page = 1;
                itemsPerPage = 10;
                includeProduct = true;
                includeUser = true;
            }
            else
            {
                Console.Write("Enter page number (press Enter to skip): ");
                var pageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(pageInput)) page = int.Parse(pageInput);

                Console.Write("Enter items per page (press Enter to skip): ");
                var itemsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(itemsInput)) itemsPerPage = int.Parse(itemsInput);

                Console.Write("Enter user ID (press Enter to skip): ");
                var userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput)) userId = int.Parse(userInput);

                Console.Write("Include product? (y/n): ");
                includeProduct = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include user? (y/n): ");
                includeUser = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include pickup point? (y/n): ");
                includePickupPoint = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include promo code? (y/n): ");
                includePromoCode = Console.ReadLine()?.ToLower() == "y";
            }

            var url = $"{_baseUrl}?page={page}&itemsPerPage={itemsPerPage}&userId={userId}" +
                     $"&includeProduct={includeProduct}&includeUser={includeUser}" +
                     $"&includePickupPoint={includePickupPoint}&includePromoCode={includePromoCode}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetOrderById()
        {
            Console.Clear();
            Console.WriteLine("Get Order by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int orderId;

            if (choice == "1")
            {
                orderId = 1;
            }
            else
            {
                Console.Write("Enter order ID: ");
                orderId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{orderId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddOrder()
        {
            Console.Clear();
            Console.WriteLine("Add Order Demonstration");
            Console.WriteLine("1. Use predefined order");
            Console.WriteLine("2. Enter custom order details");

            var choice = Console.ReadLine();
            Order order;

            if (choice == "1")
            {
                order = new Order
                {
                    Date = DateTime.Now,
                    Quantity = 1,
                    ProductId = 1,
                    UserId = 1,
                    PickupPointId = 1
                };
            }
            else
            {
                order = new Order();
                Console.Write("Enter date (yyyy-MM-dd): ");
                order.Date = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

                Console.Write("Enter quantity: ");
                order.Quantity = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter product ID: ");
                order.ProductId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter user ID: ");
                order.UserId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter pickup point ID: ");
                order.PickupPointId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter promo code ID (press Enter to skip): ");
                var promoCodeInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(promoCodeInput)) order.PromoCodeId = int.Parse(promoCodeInput);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, order);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateOrder()
        {
            Console.Clear();
            Console.WriteLine("Update Order Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Order order;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                order = new Order
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Quantity = 2,
                    ProductId = 1,
                    UserId = 1,
                    PickupPointId = 1
                };
            }
            else
            {
                Console.Write("Enter previous order ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                order = new Order();
                Console.Write("Enter new order ID: ");
                order.Id = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter date (yyyy-MM-dd): ");
                order.Date = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

                Console.Write("Enter quantity: ");
                order.Quantity = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter product ID: ");
                order.ProductId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter user ID: ");
                order.UserId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter pickup point ID: ");
                order.PickupPointId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter promo code ID (press Enter to skip): ");
                var promoCodeInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(promoCodeInput)) order.PromoCodeId = int.Parse(promoCodeInput);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", order);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteOrder()
        {
            Console.Clear();
            Console.WriteLine("Delete Order Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int orderId;

            if (choice == "1")
            {
                orderId = 1;
            }
            else
            {
                Console.Write("Enter order ID to delete: ");
                orderId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{orderId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int PickupPointId { get; set; }
        public int? PromoCodeId { get; set; }
    }
}
