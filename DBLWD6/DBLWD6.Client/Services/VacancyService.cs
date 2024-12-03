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
    public class VacancyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VacancyService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Vacancy";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Vacancy Service Demonstration");
                Console.WriteLine("1. Get Vacancies Collection");
                Console.WriteLine("2. Get Vacancy by ID");
                Console.WriteLine("3. Add Vacancy");
                Console.WriteLine("4. Update Vacancy");
                Console.WriteLine("5. Delete Vacancy");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetVacancies();
                        break;
                    case "2":
                        await DemonstrateGetVacancyById();
                        break;
                    case "3":
                        await DemonstrateAddVacancy();
                        break;
                    case "4":
                        await DemonstrateUpdateVacancy();
                        break;
                    case "5":
                        await DemonstrateDeleteVacancy();
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

        private async Task DemonstrateGetVacancies()
        {
            Console.Clear();
            Console.WriteLine("Get Vacancies Collection Demonstration");
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
                var vacancies = JsonSerializer.Deserialize<IEnumerable<Vacancy>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (vacancies != null)
                {
                    Console.WriteLine("\nVacancies found:");
                    foreach (var vacancy in vacancies)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"ID: {vacancy.Id}");
                        Console.WriteLine($"Title: {vacancy.Title}");
                        Console.WriteLine($"Description: {vacancy.Description}");
                        Console.WriteLine($"Requirements: {vacancy.Requirements}");
                        Console.WriteLine($"Responsibilities: {vacancy.Responsibilities}");
                        Console.WriteLine($"Location: {vacancy.Location}");
                        Console.WriteLine($"Salary: {vacancy.Salary:C}");
                        Console.WriteLine($"Created At: {vacancy.CreatedAt}");
                        if (!string.IsNullOrEmpty(vacancy.Image))
                            Console.WriteLine($"Image: {vacancy.Image}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo vacancies found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetVacancyById()
        {
            Console.Clear();
            Console.WriteLine("Get Vacancy by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int vacancyId;

            if (choice == "1")
            {
                vacancyId = 1;
            }
            else
            {
                Console.Write("Enter vacancy ID: ");
                vacancyId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{vacancyId}");
                var content = await response.Content.ReadAsStringAsync();
                var vacancy = JsonSerializer.Deserialize<Vacancy>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (vacancy != null)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {vacancy.Id}");
                    Console.WriteLine($"Title: {vacancy.Title}");
                    Console.WriteLine($"Description: {vacancy.Description}");
                    Console.WriteLine($"Requirements: {vacancy.Requirements}");
                    Console.WriteLine($"Responsibilities: {vacancy.Responsibilities}");
                    Console.WriteLine($"Location: {vacancy.Location}");
                    Console.WriteLine($"Salary: {vacancy.Salary:C}");
                    Console.WriteLine($"Created At: {vacancy.CreatedAt}");
                    if (!string.IsNullOrEmpty(vacancy.Image))
                        Console.WriteLine($"Image: {vacancy.Image}");
                    Console.WriteLine("----------------------------------------");
                }
                else
                {
                    Console.WriteLine("\nVacancy not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddVacancy()
        {
            Console.Clear();
            Console.WriteLine("Add Vacancy Demonstration");
            Console.WriteLine("1. Use predefined vacancy");
            Console.WriteLine("2. Enter custom vacancy");

            var choice = Console.ReadLine();
            Vacancy newVacancy;

            if (choice == "1")
            {
                newVacancy = new Vacancy
                {
                    Title = "Software Developer",
                    Description = "Join our dynamic team of developers",
                    Requirements = "C#, .NET Core, SQL",
                    Responsibilities = "Develop and maintain web applications",
                    Location = "Remote",
                    Salary = 80000,
                    Image = "developer.jpg",
                    CreatedAt = DateTime.UtcNow
                };
            }
            else
            {
                newVacancy = new Vacancy();
                
                Console.Write("Enter vacancy title: ");
                newVacancy.Title = Console.ReadLine() ?? "Software Developer";

                Console.Write("Enter description: ");
                newVacancy.Description = Console.ReadLine() ?? "";

                Console.Write("Enter requirements: ");
                newVacancy.Requirements = Console.ReadLine() ?? "Not specified";

                Console.Write("Enter responsibilities: ");
                newVacancy.Responsibilities = Console.ReadLine() ?? "";

                Console.Write("Enter location: ");
                newVacancy.Location = Console.ReadLine() ?? "";

                Console.Write("Enter salary: ");
                newVacancy.Salary = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter image URL (optional): ");
                newVacancy.Image = Console.ReadLine();

                newVacancy.CreatedAt = DateTime.UtcNow;
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, newVacancy);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateVacancy()
        {
            Console.Clear();
            Console.WriteLine("Update Vacancy Demonstration");
            Console.WriteLine("1. Use predefined data");
            Console.WriteLine("2. Enter custom data");

            var choice = Console.ReadLine();
            Vacancy updatedVacancy;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                updatedVacancy = new Vacancy
                {
                    Id = 1,
                    Title = "Senior Software Developer",
                    Description = "Lead our dynamic team of developers",
                    Requirements = "C#, .NET Core, SQL, Leadership experience",
                    Responsibilities = "Lead development of web applications",
                    Location = "Hybrid",
                    Salary = 120000,
                    Image = "senior-developer.jpg",
                    CreatedAt = DateTime.UtcNow
                };
            }
            else
            {
                Console.Write("Enter vacancy ID to update: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");
                
                updatedVacancy = new Vacancy { Id = prevId };
                
                Console.Write("Enter updated title: ");
                updatedVacancy.Title = Console.ReadLine() ?? "Senior Software Developer";

                Console.Write("Enter updated description: ");
                updatedVacancy.Description = Console.ReadLine() ?? "";

                Console.Write("Enter updated requirements: ");
                updatedVacancy.Requirements = Console.ReadLine() ?? "Not specified";

                Console.Write("Enter updated responsibilities: ");
                updatedVacancy.Responsibilities = Console.ReadLine() ?? "";

                Console.Write("Enter updated location: ");
                updatedVacancy.Location = Console.ReadLine() ?? "";

                Console.Write("Enter updated salary: ");
                updatedVacancy.Salary = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter updated image URL (optional): ");
                updatedVacancy.Image = Console.ReadLine();

                updatedVacancy.CreatedAt = DateTime.UtcNow;
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", updatedVacancy);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteVacancy()
        {
            Console.Clear();
            Console.WriteLine("Delete Vacancy Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int vacancyId;

            if (choice == "1")
            {
                vacancyId = 1;
            }
            else
            {
                Console.Write("Enter vacancy ID to delete: ");
                vacancyId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{vacancyId}");
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
