using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DBLWD6.Domain.Entities;

namespace DBLWD6.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OrderService(string baseUrl)
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
            bool? includeProduct = null;
            bool? includeUser = null;
            bool? includePickupPoint = null;
            bool? includePromoCode = null;

            if (choice == "1")
            {
                page = 1;
                itemsPerPage = 10;
                includeProduct = true;
                includePickupPoint = true;
            }
            else
            {
                Console.Write("Enter page number (press Enter to skip): ");
                var pageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(pageInput)) page = int.Parse(pageInput);

                Console.Write("Enter items per page (press Enter to skip): ");
                var itemsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(itemsInput)) itemsPerPage = int.Parse(itemsInput);

                Console.Write("Enter user ID to filter by (press Enter to skip): ");
                var userIdInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userIdInput)) userId = int.Parse(userIdInput);

                Console.Write("Include product details? (y/n/Enter to skip): ");
                var includeProductInput = Console.ReadLine()?.ToLower();
                if (!string.IsNullOrEmpty(includeProductInput)) includeProduct = includeProductInput == "y";

                Console.Write("Include user details? (y/n/Enter to skip): ");
                var includeUserInput = Console.ReadLine()?.ToLower();
                if (!string.IsNullOrEmpty(includeUserInput)) includeUser = includeUserInput == "y";

                Console.Write("Include pickup point details? (y/n/Enter to skip): ");
                var includePickupPointInput = Console.ReadLine()?.ToLower();
                if (!string.IsNullOrEmpty(includePickupPointInput)) includePickupPoint = includePickupPointInput == "y";

                Console.Write("Include promo code details? (y/n/Enter to skip): ");
                var includePromoCodeInput = Console.ReadLine()?.ToLower();
                if (!string.IsNullOrEmpty(includePromoCodeInput)) includePromoCode = includePromoCodeInput == "y";
            }

            StringBuilder url = new($"{_baseUrl}?");
            url.Append(page == null ? "" : $"page={page}&");
            url.Append(itemsPerPage == null ? "" : $"itemsPerPage={itemsPerPage}&");
            url.Append(userId == null ? "" : $"userId={userId}&");
            url.Append(includeProduct == null ? "" : $"includeProduct={includeProduct}&");
            url.Append(includeUser == null ? "" : $"includeUser={includeUser}&");
            url.Append(includePickupPoint == null ? "" : $"includePickupPoint={includePickupPoint}&");
            url.Append(includePromoCode == null ? "" : $"includePromoCode={includePromoCode}");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.Id}");
                    Console.WriteLine($"Date: {order.Date}");
                    Console.WriteLine($"Quantity: {order.Quantity}");
                    
                    if (includeProduct == true && order.Product != null)
                    {
                        Console.WriteLine($"Product ID: {order.ProductId}");
                        Console.WriteLine($"Product Name: {order.Product.Name}");
                        Console.WriteLine($"Product Price: {order.Product.Price}");
                    }
                    
                    if (includeUser == true && order.User != null)
                    {
                        Console.WriteLine($"User ID: {order.UserId}");
                        Console.WriteLine($"User Email: {order.User.Email}");
                        Console.WriteLine($"User Name: {order.User.FirstName} {order.User.LastName}");
                    }
                    
                    if (includePickupPoint == true && order.PickupPoint != null)
                    {
                        Console.WriteLine($"Pickup Point ID: {order.PickupPointId}");
                        Console.WriteLine($"Pickup Point Address: {order.PickupPoint.Address}");
                    }
                    
                    if (includePromoCode == true && order.PromoCode != null)
                    {
                        Console.WriteLine($"Promo Code ID: {order.PromoCodeId}");
                        Console.WriteLine($"Promo Code: {order.PromoCode.Code}");
                        Console.WriteLine($"Discount: {order.PromoCode.Discount}%");
                    }
                    
                    Console.WriteLine();
                }
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
            
            Console.Write("Enter order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            Console.Write("Include product details? (y/n): ");
            bool? includeProduct = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Include user details? (y/n): ");
            bool? includeUser = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Include pickup point details? (y/n): ");
            bool? includePickupPoint = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Include promo code details? (y/n): ");
            bool? includePromoCode = Console.ReadLine()?.ToLower() == "y";

            try
            {
                var url = $"{_baseUrl}/{id}?includeProduct={includeProduct}&includeUser={includeUser}&includePickupPoint={includePickupPoint}&includePromoCode={includePromoCode}";
                var response = await _httpClient.GetAsync(url);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Order not found");
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"\nID: {order.Id}");
                Console.WriteLine($"Date: {order.Date}");
                Console.WriteLine($"Quantity: {order.Quantity}");

                if (includeProduct == true && order.Product != null)
                {
                    Console.WriteLine($"Product ID: {order.ProductId}");
                    Console.WriteLine($"Product Name: {order.Product.Name}");
                    Console.WriteLine($"Product Price: {order.Product.Price}");
                }

                if (includeUser == true && order.User != null)
                {
                    Console.WriteLine($"User ID: {order.UserId}");
                    Console.WriteLine($"User Email: {order.User.Email}");
                    Console.WriteLine($"User Name: {order.User.FirstName} {order.User.LastName}");
                }

                if (includePickupPoint == true && order.PickupPoint != null)
                {
                    Console.WriteLine($"Pickup Point ID: {order.PickupPointId}");
                    Console.WriteLine($"Pickup Point Address: {order.PickupPoint.Address}");
                }

                if (includePromoCode == true && order.PromoCode != null)
                {
                    Console.WriteLine($"Promo Code ID: {order.PromoCodeId}");
                    Console.WriteLine($"Promo Code: {order.PromoCode.Code}");
                    Console.WriteLine($"Discount: {order.PromoCode.Discount}%");
                }
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

            var order = new Order();
            
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format");
                return;
            }
            order.Quantity = quantity;

            Console.Write("Enter product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product ID format");
                return;
            }
            order.ProductId = productId;

            Console.Write("Enter user ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid user ID format");
                return;
            }
            order.UserId = userId;

            Console.Write("Enter pickup point ID: ");
            if (!int.TryParse(Console.ReadLine(), out int pickupPointId))
            {
                Console.WriteLine("Invalid pickup point ID format");
                return;
            }
            order.PickupPointId = pickupPointId;

            Console.Write("Enter promo code ID (press Enter to skip): ");
            var promoCodeInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(promoCodeInput) && int.TryParse(promoCodeInput, out int promoCodeId))
            {
                order.PromoCodeId = promoCodeId;
            }

            order.Date = DateTime.Now;

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, order);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Order created successfully");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {error}");
                }
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

            Console.Write("Enter order ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int prevId))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            var order = new Order();
            
            Console.Write("Enter new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format");
                return;
            }
            order.Quantity = quantity;

            Console.Write("Enter new product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product ID format");
                return;
            }
            order.ProductId = productId;

            Console.Write("Enter new user ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid user ID format");
                return;
            }
            order.UserId = userId;

            Console.Write("Enter new pickup point ID: ");
            if (!int.TryParse(Console.ReadLine(), out int pickupPointId))
            {
                Console.WriteLine("Invalid pickup point ID format");
                return;
            }
            order.PickupPointId = pickupPointId;

            Console.Write("Enter new promo code ID (press Enter to skip): ");
            var promoCodeInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(promoCodeInput) && int.TryParse(promoCodeInput, out int promoCodeId))
            {
                order.PromoCodeId = promoCodeId;
            }

            order.Date = DateTime.Now;

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", order);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Order updated successfully");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {error}");
                }
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

            Console.Write("Enter order ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Order deleted successfully");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
