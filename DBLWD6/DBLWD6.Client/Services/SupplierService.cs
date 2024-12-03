using System.Text;
using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class SupplierService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SupplierService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Supplier";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Supplier Service Demonstration");
                Console.WriteLine("1. Get Suppliers Collection");
                Console.WriteLine("2. Get Supplier by ID");
                Console.WriteLine("3. Add Supplier");
                Console.WriteLine("4. Update Supplier");
                Console.WriteLine("5. Delete Supplier");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetSuppliers();
                        break;
                    case "2":
                        await DemonstrateGetSupplierById();
                        break;
                    case "3":
                        await DemonstrateAddSupplier();
                        break;
                    case "4":
                        await DemonstrateUpdateSupplier();
                        break;
                    case "5":
                        await DemonstrateDeleteSupplier();
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

        private async Task DemonstrateGetSuppliers()
        {
            Console.Clear();
            Console.WriteLine("Get Suppliers Collection Demonstration");
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
                var suppliers = JsonSerializer.Deserialize<IEnumerable<Supplier>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (suppliers != null)
                {
                    Console.WriteLine("\nSuppliers found:");
                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {supplier.Id}");
                        Console.WriteLine($"Name: {supplier.Name}");
                        Console.WriteLine($"Address: {supplier.Address}");
                        Console.WriteLine($"Phone: {supplier.Phone}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo suppliers found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetSupplierById()
        {
            Console.Clear();
            Console.WriteLine("Get Supplier by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int supplierId;

            if (choice == "1")
            {
                supplierId = 1;
            }
            else
            {
                Console.Write("Enter supplier ID: ");
                supplierId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{supplierId}");
                var content = await response.Content.ReadAsStringAsync();
                var supplier = JsonSerializer.Deserialize<Supplier>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (supplier != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {supplier.Id}");
                    Console.WriteLine($"Name: {supplier.Name}");
                    Console.WriteLine($"Address: {supplier.Address}");
                    Console.WriteLine($"Phone: {supplier.Phone}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nSupplier not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddSupplier()
        {
            Console.Clear();
            Console.WriteLine("Add Supplier Demonstration");
            Console.WriteLine("1. Use predefined supplier");
            Console.WriteLine("2. Enter custom supplier");

            var choice = Console.ReadLine();
            Supplier newSupplier;

            if (choice == "1")
            {
                newSupplier = new Supplier
                {
                    Name = "Test Supplier",
                    Address = "123 Test Street",
                    Phone = "+1234567890"
                };
            }
            else
            {
                newSupplier = new Supplier();
                
                Console.Write("Enter supplier name: ");
                newSupplier.Name = Console.ReadLine() ?? "Test Supplier";

                Console.Write("Enter supplier address: ");
                newSupplier.Address = Console.ReadLine() ?? "Test Address";

                Console.Write("Enter supplier phone: ");
                newSupplier.Phone = Console.ReadLine() ?? "+1234567890";
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, newSupplier);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateSupplier()
        {
            Console.Clear();
            Console.WriteLine("Update Supplier Demonstration");
            Console.WriteLine("1. Use predefined data");
            Console.WriteLine("2. Enter custom data");

            var choice = Console.ReadLine();
            Supplier updatedSupplier;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                updatedSupplier = new Supplier
                {
                    Id = 1,
                    Name = "Updated Test Supplier",
                    Address = "456 Updated Street",
                    Phone = "+9876543210"
                };
            }
            else
            {
                Console.Write("Enter supplier ID to update: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");
                
                updatedSupplier = new Supplier { Id = prevId };
                
                Console.Write("Enter updated name: ");
                updatedSupplier.Name = Console.ReadLine() ?? "Updated Test Supplier";

                Console.Write("Enter updated address: ");
                updatedSupplier.Address = Console.ReadLine() ?? "Updated Address";

                Console.Write("Enter updated phone: ");
                updatedSupplier.Phone = Console.ReadLine() ?? "+9876543210";
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", updatedSupplier);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteSupplier()
        {
            Console.Clear();
            Console.WriteLine("Delete Supplier Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int supplierId;

            if (choice == "1")
            {
                supplierId = 1;
            }
            else
            {
                Console.Write("Enter supplier ID to delete: ");
                supplierId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{supplierId}");
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
