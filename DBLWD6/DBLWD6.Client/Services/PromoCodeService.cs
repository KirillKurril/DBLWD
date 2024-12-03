using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using DBLWD6.Domain.Entities;

namespace DBLWD6.Client.Services
{
    public class PromoCodeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PromoCodeService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "PromoCode";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("PromoCode Service Demonstration");
                Console.WriteLine("1. Get PromoCodes Collection");
                Console.WriteLine("2. Get PromoCode by ID");
                Console.WriteLine("3. Add PromoCode");
                Console.WriteLine("4. Update PromoCode");
                Console.WriteLine("5. Delete PromoCode");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetPromoCodes();
                        break;
                    case "2":
                        await DemonstrateGetPromoCodeById();
                        break;
                    case "3":
                        await DemonstrateAddPromoCode();
                        break;
                    case "4":
                        await DemonstrateUpdatePromoCode();
                        break;
                    case "5":
                        await DemonstrateDeletePromoCode();
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

        private async Task DemonstrateGetPromoCodes()
        {
            Console.Clear();
            Console.WriteLine("Get PromoCodes Collection Demonstration");
            Console.WriteLine("1. Use predefined parameters");
            Console.WriteLine("2. Enter custom parameters");
            
            var choice = Console.ReadLine();
            
            int? page = null;
            int? itemsPerPage = null;

            if (choice == "1")
            {
                page = 1;
                itemsPerPage = 10;
            }
            else
            {
                Console.Write("Enter page number (press Enter to skip): ");
                var pageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(pageInput)) page = int.Parse(pageInput);

                Console.Write("Enter items per page (press Enter to skip): ");
                var itemsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(itemsInput)) itemsPerPage = int.Parse(itemsInput);
            }

            StringBuilder url = new($"{_baseUrl}?");
            url.Append(page == null ? "" : $"page={page}&");
            url.Append(itemsPerPage == null ? "" : $"itemsPerPage={itemsPerPage}");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var promoCodes = JsonSerializer.Deserialize<IEnumerable<PromoCode>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (promoCodes != null && promoCodes.Any())
                {
                    Console.WriteLine("\nPromoCodes found:");
                    foreach (var promoCode in promoCodes)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {promoCode.Id}");
                        Console.WriteLine($"Code: {promoCode.Code}");
                        Console.WriteLine($"Discount: {promoCode.Discount:P0}");
                        Console.WriteLine($"Expiration: {promoCode.Expiration:g}");
                        Console.WriteLine($"Status: {(promoCode.Expiration > DateTime.Now ? "Active" : "Expired")}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo PromoCodes found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetPromoCodeById()
        {
            Console.Clear();
            Console.WriteLine("Get PromoCode by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int promoCodeId;

            if (choice == "1")
            {
                promoCodeId = 1;
            }
            else
            {
                Console.Write("Enter PromoCode ID: ");
                promoCodeId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{promoCodeId}");
                var content = await response.Content.ReadAsStringAsync();
                var promoCode = JsonSerializer.Deserialize<PromoCode>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (promoCode != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {promoCode.Id}");
                    Console.WriteLine($"Code: {promoCode.Code}");
                    Console.WriteLine($"Discount: {promoCode.Discount:P0}");
                    Console.WriteLine($"Expiration: {promoCode.Expiration:g}");
                    Console.WriteLine($"Status: {(promoCode.Expiration > DateTime.Now ? "Active" : "Expired")}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nPromoCode not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddPromoCode()
        {
            Console.Clear();
            Console.WriteLine("Add PromoCode Demonstration");
            Console.WriteLine("1. Use predefined PromoCode");
            Console.WriteLine("2. Enter custom PromoCode details");

            var choice = Console.ReadLine();
            PromoCode promoCode;

            if (choice == "1")
            {
                promoCode = new PromoCode
                {
                    Code = 12345,
                    Discount = 0.15, // 15% discount
                    Expiration = DateTime.Now.AddDays(30)
                };
            }
            else
            {
                promoCode = new PromoCode();
                Console.Write("Enter PromoCode code (number): ");
                promoCode.Code = int.Parse(Console.ReadLine() ?? "12345");

                Console.Write("Enter discount (0-1, e.g., 0.15 for 15%): ");
                promoCode.Discount = double.Parse(Console.ReadLine() ?? "0.15");

                Console.Write("Enter expiration days from now: ");
                int days = int.Parse(Console.ReadLine() ?? "30");
                promoCode.Expiration = DateTime.Now.AddDays(days);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, promoCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdatePromoCode()
        {
            Console.Clear();
            Console.WriteLine("Update PromoCode Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            PromoCode promoCode;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                promoCode = new PromoCode
                {
                    Id = 1,
                    Code = 54321,
                    Discount = 0.20, // 20% discount
                    Expiration = DateTime.Now.AddDays(60)
                };
            }
            else
            {
                Console.Write("Enter previous PromoCode ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                promoCode = new PromoCode();
                Console.Write("Enter new PromoCode code (number): ");
                promoCode.Code = int.Parse(Console.ReadLine() ?? "54321");

                Console.Write("Enter new discount (0-1, e.g., 0.20 for 20%): ");
                promoCode.Discount = double.Parse(Console.ReadLine() ?? "0.20");

                Console.Write("Enter new expiration days from now: ");
                int days = int.Parse(Console.ReadLine() ?? "60");
                promoCode.Expiration = DateTime.Now.AddDays(days);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", promoCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeletePromoCode()
        {
            Console.Clear();
            Console.WriteLine("Delete PromoCode Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int promoCodeId;

            if (choice == "1")
            {
                promoCodeId = 1;
            }
            else
            {
                Console.Write("Enter PromoCode ID to delete: ");
                promoCodeId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{promoCodeId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
