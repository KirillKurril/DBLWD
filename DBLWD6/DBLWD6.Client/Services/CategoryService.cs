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
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CategoryService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Category";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Category Service Demonstration");
                Console.WriteLine("1. Get Categories Collection");
                Console.WriteLine("2. Get Category by ID");
                Console.WriteLine("3. Add Category");
                Console.WriteLine("4. Update Category");
                Console.WriteLine("5. Delete Category");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetCategories();
                        break;
                    case "2":
                        await DemonstrateGetCategoryById();
                        break;
                    case "3":
                        await DemonstrateAddCategory();
                        break;
                    case "4":
                        await DemonstrateUpdateCategory();
                        break;
                    case "5":
                        await DemonstrateDeleteCategory();
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

        private async Task DemonstrateGetCategories()
        {
            Console.Clear();
            Console.WriteLine("Get Categories Collection Demonstration");
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
                var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (categories != null)
                {
                    Console.WriteLine("\nCategories found:");
                    foreach (var category in categories)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {category.Id}");
                        Console.WriteLine($"Name: {category.Name}");
                        Console.WriteLine($"Description: {category.Description}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo categories found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetCategoryById()
        {
            Console.Clear();
            Console.WriteLine("Get Category by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int categoryId;

            if (choice == "1")
            {
                categoryId = 1;
            }
            else
            {
                Console.Write("Enter category ID: ");
                categoryId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{categoryId}");
                var content = await response.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<Category>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (category != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {category.Id}");
                    Console.WriteLine($"Name: {category.Name}");
                    Console.WriteLine($"Description: {category.Description}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nCategory not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddCategory()
        {
            Console.Clear();
            Console.WriteLine("Add Category Demonstration");
            Console.WriteLine("1. Use predefined category");
            Console.WriteLine("2. Enter custom category details");

            var choice = Console.ReadLine();
            Category category;

            if (choice == "1")
            {
                category = new Category
                {
                    Name = "Demo Category",
                    Description = "This is a demo category description"
                };
            }
            else
            {
                category = new Category();
                Console.Write("Enter category name: ");
                category.Name = Console.ReadLine() ?? "";

                Console.Write("Enter category description: ");
                category.Description = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, category);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateCategory()
        {
            Console.Clear();
            Console.WriteLine("Update Category Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Category category;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                category = new Category
                {
                    Id = 1,
                    Name = "Updated Demo Category",
                    Description = "This is an updated demo category description"
                };
            }
            else
            {
                Console.Write("Enter previous category ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                category = new Category();
                Console.Write("Enter new category name: ");
                category.Name = Console.ReadLine() ?? "";

                Console.Write("Enter new category description: ");
                category.Description = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", category);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteCategory()
        {
            Console.Clear();
            Console.WriteLine("Delete Category Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int categoryId;

            if (choice == "1")
            {
                categoryId = 1;
            }
            else
            {
                Console.Write("Enter category ID to delete: ");
                categoryId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{categoryId}");
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
