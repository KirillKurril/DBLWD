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
    public class ReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ReviewService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Review";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Review Service Demonstration");
                Console.WriteLine("1. Get Reviews Collection");
                Console.WriteLine("2. Get Review by ID");
                Console.WriteLine("3. Add Review");
                Console.WriteLine("4. Update Review");
                Console.WriteLine("5. Delete Review");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetReviews();
                        break;
                    case "2":
                        await DemonstrateGetReviewById();
                        break;
                    case "3":
                        await DemonstrateAddReview();
                        break;
                    case "4":
                        await DemonstrateUpdateReview();
                        break;
                    case "5":
                        await DemonstrateDeleteReview();
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

        private async Task DemonstrateGetReviews()
        {
            Console.Clear();
            Console.WriteLine("Get Reviews Collection Demonstration");
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
                var reviews = JsonSerializer.Deserialize<IEnumerable<Review>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (reviews != null && reviews.Any())
                {
                    Console.WriteLine("\nReviews found:");
                    foreach (var review in reviews)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {review.Id}");
                        Console.WriteLine($"Rating: {review.Rating}/5");
                        Console.WriteLine($"Comment: {review.Comment}");
                        Console.WriteLine($"User ID: {review.UserId}");
                        Console.WriteLine($"Product ID: {review.ProductId}");
                        Console.WriteLine($"Created: {review.Created:g}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo Reviews found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetReviewById()
        {
            Console.Clear();
            Console.WriteLine("Get Review by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int reviewId;

            if (choice == "1")
            {
                reviewId = 1;
            }
            else
            {
                Console.Write("Enter Review ID: ");
                reviewId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{reviewId}");
                var content = await response.Content.ReadAsStringAsync();
                var review = JsonSerializer.Deserialize<Review>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (review != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {review.Id}");
                    Console.WriteLine($"Rating: {review.Rating}/5");
                    Console.WriteLine($"Comment: {review.Comment}");
                    Console.WriteLine($"User ID: {review.UserId}");
                    Console.WriteLine($"Product ID: {review.ProductId}");
                    Console.WriteLine($"Created: {review.Created:g}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nReview not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddReview()
        {
            Console.Clear();
            Console.WriteLine("Add Review Demonstration");
            Console.WriteLine("1. Use predefined Review");
            Console.WriteLine("2. Enter custom Review details");

            var choice = Console.ReadLine();
            Review review;

            if (choice == "1")
            {
                review = new Review
                {
                    Rating = 4,
                    Comment = "Great product, would recommend!",
                    UserId = 1,
                    ProductId = 1,
                    Created = DateTime.Now
                };
            }
            else
            {
                review = new Review();
                Console.Write("Enter Rating (1-5): ");
                review.Rating = int.Parse(Console.ReadLine() ?? "4");

                Console.Write("Enter Comment: ");
                review.Comment = Console.ReadLine() ?? "Great product!";

                Console.Write("Enter User ID: ");
                review.UserId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter Product ID: ");
                review.ProductId = int.Parse(Console.ReadLine() ?? "1");

                review.Created = DateTime.Now;
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, review);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateReview()
        {
            Console.Clear();
            Console.WriteLine("Update Review Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            Review review;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                review = new Review
                {
                    Id = 1,
                    Rating = 5,
                    Comment = "Updated: Even better than initially thought!",
                    UserId = 1,
                    ProductId = 1,
                    Created = DateTime.Now
                };
            }
            else
            {
                Console.Write("Enter previous Review ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                review = new Review();
                Console.Write("Enter new Rating (1-5): ");
                review.Rating = int.Parse(Console.ReadLine() ?? "5");

                Console.Write("Enter new Comment: ");
                review.Comment = Console.ReadLine() ?? "Updated review";

                Console.Write("Enter User ID: ");
                review.UserId = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter Product ID: ");
                review.ProductId = int.Parse(Console.ReadLine() ?? "1");

                review.Created = DateTime.Now;
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", review);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteReview()
        {
            Console.Clear();
            Console.WriteLine("Delete Review Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int reviewId;

            if (choice == "1")
            {
                reviewId = 1;
            }
            else
            {
                Console.Write("Enter Review ID to delete: ");
                reviewId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{reviewId}");
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
