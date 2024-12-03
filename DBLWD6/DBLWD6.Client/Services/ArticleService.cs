using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DBLWD6.Domain.Entities;

namespace DBLWD6.Client.Services
{
    public class ArticleService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ArticleService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Article";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Article Service Demonstration");
                Console.WriteLine("1. Get Articles Collection");
                Console.WriteLine("2. Get Article by ID");
                Console.WriteLine("3. Add Article");
                Console.WriteLine("4. Update Article");
                Console.WriteLine("5. Delete Article");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetArticles();
                        break;
                    case "2":
                        await DemonstrateGetArticleById();
                        break;
                    case "3":
                        await DemonstrateAddArticle();
                        break;
                    case "4":
                        await DemonstrateUpdateArticle();
                        break;
                    case "5":
                        await DemonstrateDeleteArticle();
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

        private async Task DemonstrateGetArticles()
        {
            Console.Clear();
            Console.WriteLine("Get Articles Collection Demonstration");
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
                var articles = JsonSerializer.Deserialize<IEnumerable<Article>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (articles != null)
                {
                    Console.WriteLine("\nArticles found:");
                    foreach (var article in articles)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {article.Id}");
                        Console.WriteLine($"Title: {article.Title}");
                        Console.WriteLine($"Text: {article.Text}");
                        Console.WriteLine($"Photo: {article.Photo}");
                        Console.WriteLine($"Created At: {article.CreatedAt}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo articles found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetArticleById()
        {
            Console.Clear();
            Console.WriteLine("Get Article by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int articleId;

            if (choice == "1")
            {
                articleId = 1;
            }
            else
            {
                Console.Write("Enter article ID: ");
                articleId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{articleId}");
                var content = await response.Content.ReadAsStringAsync();
                var article = JsonSerializer.Deserialize<Article>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (article != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {article.Id}");
                    Console.WriteLine($"Title: {article.Title}");
                    Console.WriteLine($"Text: {article.Text}");
                    Console.WriteLine($"Photo: {article.Photo}");
                    Console.WriteLine($"Created At: {article.CreatedAt}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nArticle not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddArticle()
        {
            Console.Clear();
            Console.WriteLine("Add Article Demonstration");
            Console.WriteLine("1. Use predefined article");
            Console.WriteLine("2. Enter custom article details");

            var choice = Console.ReadLine();
            Article article;

            if (choice == "1")
            {
                article = new Article
                {
                    Title = "Demo Article",
                    Text = "This is a demo article text content",
                    Photo = "demo-photo.jpg"
                };
            }
            else
            {
                article = new Article();
                Console.Write("Enter article title: ");
                article.Title = Console.ReadLine() ?? "";

                Console.Write("Enter article text: ");
                article.Text = Console.ReadLine() ?? "";

                Console.Write("Enter article photo path: ");
                article.Photo = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, article);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateArticle()
        {
            Console.Clear();
            Console.WriteLine("Update Article Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Article article;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                article = new Article
                {
                    Id = 1,
                    Title = "Updated Demo Article",
                    Text = "This is an updated demo article text content",
                    Photo = "updated-demo-photo.jpg"
                };
            }
            else
            {
                Console.Write("Enter previous article ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                article = new Article();
                Console.Write("Enter new article title: ");
                article.Title = Console.ReadLine() ?? "";

                Console.Write("Enter new article text: ");
                article.Text = Console.ReadLine() ?? "";

                Console.Write("Enter new article photo path: ");
                article.Photo = Console.ReadLine() ?? "";
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", article);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteArticle()
        {
            Console.Clear();
            Console.WriteLine("Delete Article Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int articleId;

            if (choice == "1")
            {
                articleId = 1;
            }
            else
            {
                Console.Write("Enter article ID to delete: ");
                articleId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{articleId}");
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
