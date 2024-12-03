using System.Net.Http.Json;

namespace DBLWD6.Client.Services
{
    public class PartnerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PartnerService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "Partner";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Partner Service Demonstration");
                Console.WriteLine("1. Get All Partners");
                Console.WriteLine("2. Get Partner by ID");
                Console.WriteLine("3. Create Partner");
                Console.WriteLine("4. Update Partner");
                Console.WriteLine("5. Delete Partner");
                Console.WriteLine("6. Get Partners with Pagination");
                Console.WriteLine("0. Return to Main Menu");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await DemonstrateGetAllPartners();
                            break;
                        case "2":
                            await DemonstrateGetPartnerById();
                            break;
                        case "3":
                            await DemonstrateCreatePartner();
                            break;
                        case "4":
                            await DemonstrateUpdatePartner();
                            break;
                        case "5":
                            await DemonstrateDeletePartner();
                            break;
                        case "6":
                            await DemonstrateGetPartnersWithPagination();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private async Task DemonstrateGetAllPartners()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var partners = await response.Content.ReadFromJsonAsync<IEnumerable<Partner>>();
                Console.WriteLine("\nAll Partners:");
                foreach (var partner in partners)
                {
                    Console.WriteLine($"ID: {partner.Id}, Name: {partner.Name}, Website: {partner.Website}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
            }
        }

        private async Task DemonstrateGetPartnerById()
        {
            Console.Write("Enter Partner ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var partner = await response.Content.ReadFromJsonAsync<Partner>();
                    Console.WriteLine($"\nPartner Details:");
                    Console.WriteLine($"ID: {partner.Id}");
                    Console.WriteLine($"Name: {partner.Name}");
                    Console.WriteLine($"Image: {partner.Image}");
                    Console.WriteLine($"Website: {partner.Website}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Partner not found");
                }
                else
                {
                    Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format");
            }
        }

        private async Task DemonstrateCreatePartner()
        {
            var partner = new Partner();

            Console.WriteLine("Enter Partner Details:");
            
            Console.Write("Name: ");
            partner.Name = Console.ReadLine();

            Console.Write("Image (press Enter for default): ");
            var image = Console.ReadLine();
            if (!string.IsNullOrEmpty(image))
            {
                partner.Image = image;
            }

            Console.Write("Website (press Enter for default): ");
            var website = Console.ReadLine();
            if (!string.IsNullOrEmpty(website))
            {
                partner.Website = website;
            }

            var response = await _httpClient.PostAsJsonAsync(_baseUrl, partner);
            if (response.IsSuccessStatusCode)
            {
                var createdPartner = await response.Content.ReadFromJsonAsync<Partner>();
                Console.WriteLine($"\nPartner created successfully with ID: {createdPartner.Id}");
            }
            else
            {
                Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
            }
        }

        private async Task DemonstrateUpdatePartner()
        {
            Console.Write("Enter Partner ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            // Получаем текущего партнера
            var getResponse = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!getResponse.IsSuccessStatusCode)
            {
                if (getResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Partner not found");
                }
                else
                {
                    Console.WriteLine($"Error: {await getResponse.Content.ReadAsStringAsync()}");
                }
                return;
            }

            var currentPartner = await getResponse.Content.ReadFromJsonAsync<Partner>();
            var partner = new Partner
            {
                Id = currentPartner.Id,
                Name = currentPartner.Name,
                Image = currentPartner.Image,
                Website = currentPartner.Website
            };

            Console.WriteLine("\nCurrent values (press Enter to keep current value):");
            
            Console.WriteLine($"Current Name: {currentPartner.Name}");
            Console.Write("New Name: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                partner.Name = input;
            }

            Console.WriteLine($"Current Image: {currentPartner.Image}");
            Console.Write("New Image: ");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                partner.Image = input;
            }

            Console.WriteLine($"Current Website: {currentPartner.Website}");
            Console.Write("New Website: ");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                partner.Website = input;
            }

            var updateResponse = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={id}", partner);
            if (updateResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Partner updated successfully");
            }
            else
            {
                Console.WriteLine($"Error: {await updateResponse.Content.ReadAsStringAsync()}");
            }
        }

        private async Task DemonstrateDeletePartner()
        {
            Console.Write("Enter Partner ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Partner deleted successfully");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Partner not found");
                }
                else
                {
                    Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format");
            }
        }

        private async Task DemonstrateGetPartnersWithPagination()
        {
            Console.Write("Enter page number: ");
            if (!int.TryParse(Console.ReadLine(), out int page))
            {
                Console.WriteLine("Invalid page number");
                return;
            }

            Console.Write("Enter items per page: ");
            if (!int.TryParse(Console.ReadLine(), out int itemsPerPage))
            {
                Console.WriteLine("Invalid items per page");
                return;
            }

            var response = await _httpClient.GetAsync($"{_baseUrl}/paginated?page={page}&itemsPerPage={itemsPerPage}");
            if (response.IsSuccessStatusCode)
            {
                var partners = await response.Content.ReadFromJsonAsync<IEnumerable<Partner>>();
                Console.WriteLine($"\nPartners (Page {page}, {itemsPerPage} items per page):");
                foreach (var partner in partners)
                {
                    Console.WriteLine($"ID: {partner.Id}, Name: {partner.Name}, Website: {partner.Website}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
