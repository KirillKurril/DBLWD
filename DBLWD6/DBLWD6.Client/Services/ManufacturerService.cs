using System.Linq;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class ManufacturerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ManufacturerService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Manufacturer";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manufacturer Service Demonstration");
                Console.WriteLine("1. Get Manufacturers Collection");
                Console.WriteLine("2. Get Manufacturer by ID");
                Console.WriteLine("3. Add Manufacturer");
                Console.WriteLine("4. Update Manufacturer");
                Console.WriteLine("5. Delete Manufacturer");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetManufacturers();
                        break;
                    case "2":
                        await DemonstrateGetManufacturerById();
                        break;
                    case "3":
                        await DemonstrateAddManufacturer();
                        break;
                    case "4":
                        await DemonstrateUpdateManufacturer();
                        break;
                    case "5":
                        await DemonstrateDeleteManufacturer();
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

        private async Task DemonstrateGetManufacturers()
        {
            Console.Clear();
            Console.WriteLine("Get Manufacturers Collection Demonstration");
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
                var manufacturers = JsonSerializer.Deserialize<IEnumerable<Manufacturer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (manufacturers != null)
                {
                    Console.WriteLine("\nManufacturers found:");
                    foreach (var manufacturer in manufacturers)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {manufacturer.Id}");
                        Console.WriteLine($"Name: {manufacturer.Name}");
                        Console.WriteLine($"Address: {manufacturer.Address}");
                        Console.WriteLine($"Phone: {manufacturer.Phone}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo manufacturers found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetManufacturerById()
        {
            Console.Clear();
            Console.WriteLine("Get Manufacturer by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int manufacturerId;

            if (choice == "1")
            {
                manufacturerId = 1;
            }
            else
            {
                Console.Write("Enter manufacturer ID: ");
                manufacturerId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{manufacturerId}");
                var content = await response.Content.ReadAsStringAsync();
                var manufacturer = JsonSerializer.Deserialize<Manufacturer>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (manufacturer != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {manufacturer.Id}");
                    Console.WriteLine($"Name: {manufacturer.Name}");
                    Console.WriteLine($"Address: {manufacturer.Address}");
                    Console.WriteLine($"Phone: {manufacturer.Phone}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nManufacturer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddManufacturer()
        {
            Console.Clear();
            Console.WriteLine("Add Manufacturer Demonstration");
            Console.WriteLine("1. Use predefined manufacturer");
            Console.WriteLine("2. Enter custom manufacturer details");

            var choice = Console.ReadLine();
            Manufacturer manufacturer;

            if (choice == "1")
            {
                manufacturer = new Manufacturer
                {
                    Name = "Demo Manufacturer",
                    Address = "Demo Address",
                    Phone = "123-456-7890"
                };
            }
            else
            {
                manufacturer = new Manufacturer();
                Console.Write("Enter manufacturer name: ");
                manufacturer.Name = Console.ReadLine() ?? "";

                Console.Write("Enter manufacturer address: ");
                manufacturer.Address = Console.ReadLine() ?? "";

                Console.Write("Enter manufacturer phone: ");
                manufacturer.Phone = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, manufacturer);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateManufacturer()
        {
            Console.Clear();
            Console.WriteLine("Update Manufacturer Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Manufacturer manufacturer;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                manufacturer = new Manufacturer
                {
                    Id = 1,
                    Name = "Updated Demo Manufacturer",
                    Address = "Updated Demo Address",
                    Phone = "987-654-3210"
                };
            }
            else
            {
                Console.Write("Enter previous manufacturer ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                manufacturer = new Manufacturer();
                Console.Write("Enter new manufacturer name: ");
                manufacturer.Name = Console.ReadLine() ?? "";

                Console.Write("Enter new manufacturer address: ");
                manufacturer.Address = Console.ReadLine() ?? "";

                Console.Write("Enter new manufacturer phone: ");
                manufacturer.Phone = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", manufacturer);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteManufacturer()
        {
            Console.Clear();
            Console.WriteLine("Delete Manufacturer Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int manufacturerId;

            if (choice == "1")
            {
                manufacturerId = 1;
            }
            else
            {
                Console.Write("Enter manufacturer ID to delete: ");
                manufacturerId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{manufacturerId}");
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
