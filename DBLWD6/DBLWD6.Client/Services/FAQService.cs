using System.Text;
using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class FAQService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FAQService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "FAQ";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("FAQ Service Demonstration");
                Console.WriteLine("1. Get FAQs Collection");
                Console.WriteLine("2. Get FAQ by ID");
                Console.WriteLine("3. Add FAQ");
                Console.WriteLine("4. Update FAQ");
                Console.WriteLine("5. Delete FAQ");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetFAQs();
                        break;
                    case "2":
                        await DemonstrateGetFAQById();
                        break;
                    case "3":
                        await DemonstrateAddFAQ();
                        break;
                    case "4":
                        await DemonstrateUpdateFAQ();
                        break;
                    case "5":
                        await DemonstrateDeleteFAQ();
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

        private async Task DemonstrateGetFAQs()
        {
            Console.Clear();
            Console.WriteLine("Get FAQs Collection Demonstration");
            Console.WriteLine("1. Use predefined parameters");
            Console.WriteLine("2. Enter custom parameters");
            
            var choice = Console.ReadLine();
            
            int? page = null;
            int? itemsPerPage = null;
            bool includeArticle;

            if (choice == "1")
            {
                page = 1;
                itemsPerPage = 10;
                includeArticle = true;
            }
            else
            {
                Console.Write("Enter page number (press Enter to skip): ");
                var pageInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(pageInput)) page = int.Parse(pageInput);

                Console.Write("Enter items per page (press Enter to skip): ");
                var itemsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(itemsInput)) itemsPerPage = int.Parse(itemsInput);

                Console.Write("Include related Article? (y/n): ");
                includeArticle = Console.ReadLine()?.ToLower() == "y";
            }

            StringBuilder url = new($"{_baseUrl}?");
            url.Append(page == null ? "" : $"page={page}&");
            url.Append(itemsPerPage == null ? "" : $"itemsPerPage={itemsPerPage}&");
            url.Append($"includeArticle={includeArticle}");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var faqs = JsonSerializer.Deserialize<IEnumerable<FAQ>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (faqs != null)
                {
                    Console.WriteLine("\nFAQs found:");
                    foreach (var faq in faqs)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {faq.Id}");
                        Console.WriteLine($"Question: {faq.Question}");
                        Console.WriteLine($"Article ID: {faq.ArticleId}");
                        if (includeArticle && faq.Article != null)
                        {
                            Console.WriteLine("\nLinked Article:");
                            Console.WriteLine($"  Title: {faq.Article.Title}");
                            Console.WriteLine($"  Text: {faq.Article.Text}");
                            Console.WriteLine($"  Photo: {faq.Article.Photo}");
                            Console.WriteLine($"  Created At: {faq.Article.CreatedAt}");
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo FAQs found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetFAQById()
        {
            Console.Clear();
            Console.WriteLine("Get FAQ by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int faqId;
            bool includeArticle;

            if (choice == "1")
            {
                faqId = 1;
                includeArticle = true;
            }
            else
            {
                Console.Write("Enter FAQ ID: ");
                faqId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Include related Article? (y/n): ");
                includeArticle = Console.ReadLine()?.ToLower() == "y";
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{faqId}?includeArticle={includeArticle}");
                var content = await response.Content.ReadAsStringAsync();
                var faq = JsonSerializer.Deserialize<FAQ>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (faq != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {faq.Id}");
                    Console.WriteLine($"Question: {faq.Question}");
                    Console.WriteLine($"Article ID: {faq.ArticleId}");
                    if (includeArticle && faq.Article != null)
                    {
                        Console.WriteLine("\nLinked Article:");
                        Console.WriteLine($"  Title: {faq.Article.Title}");
                        Console.WriteLine($"  Text: {faq.Article.Text}");
                        Console.WriteLine($"  Photo: {faq.Article.Photo}");
                        Console.WriteLine($"  Created At: {faq.Article.CreatedAt}");
                    }
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nFAQ not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddFAQ()
        {
            Console.Clear();
            Console.WriteLine("Add FAQ Demonstration");
            Console.WriteLine("1. Use predefined FAQ");
            Console.WriteLine("2. Enter custom FAQ details");

            var choice = Console.ReadLine();
            FAQ faq;

            if (choice == "1")
            {
                faq = new FAQ
                {
                    Question = "Demo FAQ Question",
                    ArticleId = 1
                };
            }
            else
            {
                faq = new FAQ();
                Console.Write("Enter FAQ question: ");
                faq.Question = Console.ReadLine() ?? "";

                Console.Write("Enter Article ID: ");
                faq.ArticleId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, faq);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateFAQ()
        {
            Console.Clear();
            Console.WriteLine("Update FAQ Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            FAQ faq;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                faq = new FAQ
                {
                    Id = 1,
                    Question = "Updated Demo FAQ Question",
                    ArticleId = 1
                };
            }
            else
            {
                Console.Write("Enter previous FAQ ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                faq = new FAQ();
                Console.Write("Enter new FAQ question: ");
                faq.Question = Console.ReadLine() ?? "";

                Console.Write("Enter new Article ID: ");
                faq.ArticleId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", faq);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteFAQ()
        {
            Console.Clear();
            Console.WriteLine("Delete FAQ Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int faqId;

            if (choice == "1")
            {
                faqId = 1;
            }
            else
            {
                Console.Write("Enter FAQ ID to delete: ");
                faqId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{faqId}");
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
