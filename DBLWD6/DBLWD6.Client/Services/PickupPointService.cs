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
    public class PickupPointService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PickupPointService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "PickupPoint";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("PickupPoint Service Demonstration");
                Console.WriteLine("1. Get PickupPoints Collection");
                Console.WriteLine("2. Get PickupPoint by ID");
                Console.WriteLine("3. Add PickupPoint");
                Console.WriteLine("4. Update PickupPoint");
                Console.WriteLine("5. Delete PickupPoint");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetPickupPoints();
                        break;
                    case "2":
                        await DemonstrateGetPickupPointById();
                        break;
                    case "3":
                        await DemonstrateAddPickupPoint();
                        break;
                    case "4":
                        await DemonstrateUpdatePickupPoint();
                        break;
                    case "5":
                        await DemonstrateDeletePickupPoint();
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

        private async Task DemonstrateGetPickupPoints()
        {
            Console.Clear();
            Console.WriteLine("Get PickupPoints Collection Demonstration");
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
                var pickupPoints = JsonSerializer.Deserialize<IEnumerable<PickupPoint>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (pickupPoints != null)
                {
                    Console.WriteLine("\nPickupPoints found:");
                    foreach (var pickupPoint in pickupPoints)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {pickupPoint.Id}");
                        Console.WriteLine($"Name: {pickupPoint.Name}");
                        Console.WriteLine($"Address: {pickupPoint.Address}");
                        Console.WriteLine($"Phone Number: {pickupPoint.PhoneNumber}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo pickup points found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetPickupPointById()
        {
            Console.Clear();
            Console.WriteLine("Get PickupPoint by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int pickupPointId;

            if (choice == "1")
            {
                pickupPointId = 1;
            }
            else
            {
                Console.Write("Enter pickup point ID: ");
                pickupPointId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{pickupPointId}");
                var content = await response.Content.ReadAsStringAsync();
                var pickupPoint = JsonSerializer.Deserialize<PickupPoint>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (pickupPoint != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {pickupPoint.Id}");
                    Console.WriteLine($"Name: {pickupPoint.Name}");
                    Console.WriteLine($"Address: {pickupPoint.Address}");
                    Console.WriteLine($"Phone Number: {pickupPoint.PhoneNumber}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nPickup point not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddPickupPoint()
        {
            Console.Clear();
            Console.WriteLine("Add PickupPoint Demonstration");
            Console.WriteLine("1. Use predefined pickup point");
            Console.WriteLine("2. Enter custom pickup point details");

            var choice = Console.ReadLine();
            PickupPoint pickupPoint;

            if (choice == "1")
            {
                pickupPoint = new PickupPoint
                {
                    Name = "Demo PickupPoint",
                    Address = "Demo Address",
                    PhoneNumber = "123-456-7890"
                };
            }
            else
            {
                pickupPoint = new PickupPoint();
                Console.Write("Enter pickup point name: ");
                pickupPoint.Name = Console.ReadLine() ?? "";

                Console.Write("Enter pickup point address: ");
                pickupPoint.Address = Console.ReadLine() ?? "";

                Console.Write("Enter pickup point phone number: ");
                pickupPoint.PhoneNumber = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, pickupPoint);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdatePickupPoint()
        {
            Console.Clear();
            Console.WriteLine("Update PickupPoint Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            PickupPoint pickupPoint;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                pickupPoint = new PickupPoint
                {
                    Id = 1,
                    Name = "Updated Demo PickupPoint",
                    Address = "Updated Demo Address",
                    PhoneNumber = "987-654-3210"
                };
            }
            else
            {
                Console.Write("Enter previous pickup point ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                pickupPoint = new PickupPoint();
                Console.Write("Enter new pickup point name: ");
                pickupPoint.Name = Console.ReadLine() ?? "";

                Console.Write("Enter new pickup point address: ");
                pickupPoint.Address = Console.ReadLine() ?? "";

                Console.Write("Enter new pickup point phone number: ");
                pickupPoint.PhoneNumber = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", pickupPoint);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeletePickupPoint()
        {
            Console.Clear();
            Console.WriteLine("Delete PickupPoint Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int pickupPointId;

            if (choice == "1")
            {
                pickupPointId = 1;
            }
            else
            {
                Console.Write("Enter pickup point ID to delete: ");
                pickupPointId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{pickupPointId}");
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
