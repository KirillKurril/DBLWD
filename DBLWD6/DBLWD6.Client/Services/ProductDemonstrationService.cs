using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class ProductDemonstrationService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/api/product";

        public ProductDemonstrationService()
        {
            _httpClient = new HttpClient();
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Product Service Demonstration");
                Console.WriteLine("1. Get Products Collection");
                Console.WriteLine("2. Get Product by ID");
                Console.WriteLine("3. Add Product");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetProducts();
                        break;
                    case "2":
                        await DemonstrateGetProductById();
                        break;
                    case "3":
                        await DemonstrateAddProduct();
                        break;
                    case "4":
                        await DemonstrateUpdateProduct();
                        break;
                    case "5":
                        await DemonstrateDeleteProduct();
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

        private async Task DemonstrateGetProducts()
        {
            Console.Clear();
            Console.WriteLine("Get Products Collection Demonstration");
            Console.WriteLine("1. Use predefined parameters");
            Console.WriteLine("2. Enter custom parameters");
            
            var choice = Console.ReadLine();
            
            int? page = null;
            int? itemsPerPage = null;
            int? categoryId = null;
            bool? includeCategory = false;
            bool? includeManufacturers = false;
            bool? includeSuppliers = false;
            bool? includePickupPoints = false;

            if (choice == "1")
            {
                page = 1;
                itemsPerPage = 10;
                includeCategory = true;
            }
            else
            {
                Console.Write("Enter page number (press Enter to skip): ");
                var pageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(pageInput)) page = int.Parse(pageInput);

                Console.Write("Enter items per page (press Enter to skip): ");
                var itemsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(itemsInput)) itemsPerPage = int.Parse(itemsInput);

                Console.Write("Enter category ID (press Enter to skip): ");
                var categoryInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(categoryInput)) categoryId = int.Parse(categoryInput);

                Console.Write("Include category? (y/n): ");
                includeCategory = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include manufacturers? (y/n): ");
                includeManufacturers = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include suppliers? (y/n): ");
                includeSuppliers = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Include pickup points? (y/n): ");
                includePickupPoints = Console.ReadLine()?.ToLower() == "y";
            }

            var url = $"{BaseUrl}?page={page}&itemsPerPage={itemsPerPage}&categoryId={categoryId}" +
                     $"&includeCategory={includeCategory}&includeManufacturers={includeManufacturers}" +
                     $"&includeSuppliers={includeSuppliers}&includePickupPoints={includePickupPoints}";

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

        private async Task DemonstrateGetProductById()
        {
            Console.Clear();
            Console.WriteLine("Get Product by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int productId;

            if (choice == "1")
            {
                productId = 1;
            }
            else
            {
                Console.Write("Enter product ID: ");
                productId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{productId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddProduct()
        {
            Console.Clear();
            Console.WriteLine("Add Product Demonstration");
            Console.WriteLine("1. Use predefined product");
            Console.WriteLine("2. Enter custom product details");

            var choice = Console.ReadLine();
            Product product;

            if (choice == "1")
            {
                product = new Product
                {
                    Name = "Demo Product",
                    ArticleNumber = "Demo Article Number",
                    Description = "This is a demo product",
                    PricePerUnit = 99.99m,
                    Image = "Demo Image",
                    Count = 10,
                    CategoryId = 1
                };
            }
            else
            {
                product = new Product();
                Console.Write("Enter product name: ");
                product.Name = Console.ReadLine() ?? "";

                Console.Write("Enter product article number: ");
                product.ArticleNumber = Console.ReadLine() ?? "";

                Console.Write("Enter product description: ");
                product.Description = Console.ReadLine() ?? "";

                Console.Write("Enter product PricePerUnit: ");
                product.PricePerUnit = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter product image: ");
                product.Image = Console.ReadLine() ?? "";

                Console.Write("Enter product count: ");
                product.Count = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter category ID: ");
                product.CategoryId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseUrl, product);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("Update Product Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Product product;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                product = new Product
                {
                    Id = 1,
                    Name = "Updated Demo Product",
                    ArticleNumber = "Updated Demo Article Number",
                    Description = "This is an updated demo product",
                    PricePerUnit = 149.99m,
                    Image = "Updated Demo Image",
                    Count = 20,
                    CategoryId = 1
                };
            }
            else
            {
                Console.Write("Enter previous product ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                product = new Product();
                Console.Write("Enter new product ID: ");
                product.Id = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter new product name: ");
                product.Name = Console.ReadLine() ?? "";

                Console.Write("Enter new product article number: ");
                product.ArticleNumber = Console.ReadLine() ?? "";

                Console.Write("Enter new product description: ");
                product.Description = Console.ReadLine() ?? "";

                Console.Write("Enter new product PricePerUnit: ");
                product.PricePerUnit = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter new product image: ");
                product.Image = Console.ReadLine() ?? "";

                Console.Write("Enter new product count: ");
                product.Count = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter new category ID: ");
                product.CategoryId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}?prevId={prevId}", product);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("Delete Product Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int productId;

            if (choice == "1")
            {
                productId = 1;
            }
            else
            {
                Console.Write("Enter product ID to delete: ");
                productId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{productId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
    }
}
