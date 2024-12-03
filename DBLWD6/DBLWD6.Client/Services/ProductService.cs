using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Product";
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

            StringBuilder url = new($"{_baseUrl}?");
            url.Append(page == null ? "" : $"page={page}&");
            url.Append(itemsPerPage == null ? "" : $"itemsPerPage={itemsPerPage}&");
            url.Append(categoryId == null ? "" : $"categoryId={categoryId}&");
            url.Append(includeCategory == null ? "" : $"includeCategory={includeCategory}&");
            url.Append(includeManufacturers == null ? "" : $"includeManufacturers={includeManufacturers}&");
            url.Append(includeSuppliers == null ? "" : $"includeSuppliers={includeSuppliers}&");
            url.Append(includePickupPoints == null ? "" : $"includePickupPoints={includePickupPoints}");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (products != null)
                {
                    Console.WriteLine("\nProducts found:");
                    foreach (var product in products)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {product.Id}");
                        Console.WriteLine($"Name: {product.Name}");
                        Console.WriteLine($"Article Number: {product.ArticleNumber}");
                        Console.WriteLine($"Description: {product.Description}");
                        Console.WriteLine($"Price: {product.PricePerUnit:C}");
                        Console.WriteLine($"In Stock: {product.Count} units");
                        Console.WriteLine($"Category ID: {product.CategoryId}");
                        if (product.Category != null)
                        {
                            Console.WriteLine("Category Information:");
                            Console.WriteLine($"  - Category ID: {product.Category.Id}");
                            Console.WriteLine($"  - Category Name: {product.Category.Name}");
                            Console.WriteLine($"  - Category Description: {product.Category.Description}");
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo products found.");
                }
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

            StringBuilder url = new($"{_baseUrl}/{productId}?includeCategory=true");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (product != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {product.Id}");
                    Console.WriteLine($"Name: {product.Name}");
                    Console.WriteLine($"Article Number: {product.ArticleNumber}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.PricePerUnit:C}");
                    Console.WriteLine($"In Stock: {product.Count} units");
                    Console.WriteLine($"Category ID: {product.CategoryId}");
                    if (product.Category != null)
                    {
                        Console.WriteLine("Category Information:");
                        Console.WriteLine($"  - Category ID: {product.Category.Id}");
                        Console.WriteLine($"  - Category Name: {product.Category.Name}");
                        Console.WriteLine($"  - Category Description: {product.Category.Description}");
                    }
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nProduct not found.");
                }
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
                    PricePerUnit = 99.99,
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
                product.PricePerUnit = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter product image: ");
                product.Image = Console.ReadLine() ?? "";

                Console.Write("Enter product count: ");
                product.Count = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter category ID: ");
                product.CategoryId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, product);
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
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                var product = new Product
                {
                    Id = 1,
                    Name = "Updated Demo Product",
                    ArticleNumber = "Updated Demo Article Number",
                    Description = "This is an updated demo product",
                    PricePerUnit = 149.99,
                    Image = "Updated Demo Image",
                    Count = 20,
                    CategoryId = 1
                };

                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", product);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Product updated successfully");
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
                return;
            }

            Console.Write("Enter product ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out prevId))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            try
            {
                var url = $"{_baseUrl}/{prevId}?includeCategory=true";
                var response = await _httpClient.GetAsync(url);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Product not found");
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                var currentProduct = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var product = new Product
                {
                    Id = currentProduct.Id,
                    Name = currentProduct.Name,
                    ArticleNumber = currentProduct.ArticleNumber,
                    Description = currentProduct.Description,
                    PricePerUnit = currentProduct.PricePerUnit,
                    Image = currentProduct.Image,
                    Count = currentProduct.Count,
                    CategoryId = currentProduct.CategoryId
                };

                Console.WriteLine("\nCurrent values:");
                Console.WriteLine($"Name: {currentProduct.Name}");
                Console.WriteLine($"Article Number: {currentProduct.ArticleNumber}");
                Console.WriteLine($"Description: {currentProduct.Description}");
                Console.WriteLine($"Price Per Unit: {currentProduct.PricePerUnit}");
                Console.WriteLine($"Image: {currentProduct.Image}");
                Console.WriteLine($"Count: {currentProduct.Count}");
                Console.WriteLine($"Category ID: {currentProduct.CategoryId} (Category: {currentProduct.Category?.Name})");
                Console.WriteLine("\nPress Enter to keep current value, or enter new value:");

                Console.Write("Enter new name: ");
                var nameInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(nameInput))
                {
                    product.Name = nameInput;
                }

                Console.Write("Enter new article number: ");
                var articleInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(articleInput))
                {
                    product.ArticleNumber = articleInput;
                }

                Console.Write("Enter new description: ");
                var descriptionInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(descriptionInput))
                {
                    product.Description = descriptionInput;
                }

                Console.Write("Enter new price per unit: ");
                var priceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(priceInput) && double.TryParse(priceInput, out double price))
                {
                    product.PricePerUnit = price;
                }

                Console.Write("Enter new image: ");
                var imageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(imageInput))
                {
                    product.Image = imageInput;
                }

                Console.Write("Enter new count: ");
                var countInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(countInput) && int.TryParse(countInput, out int count))
                {
                    product.Count = count;
                }

                Console.Write("Enter new category ID: ");
                var categoryInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(categoryInput) && int.TryParse(categoryInput, out int categoryId))
                {
                    product.CategoryId = categoryId;
                }

                var updateResponse = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", product);
                if (updateResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Product updated successfully");
                }
                else
                {
                    var error = await updateResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {error}");
                }
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
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{productId}");
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
