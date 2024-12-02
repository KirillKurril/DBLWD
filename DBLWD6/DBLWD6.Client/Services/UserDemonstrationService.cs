using System.Net.Http.Json;
using System.Text.Json;

namespace DBLWD6.Client.Services
{
    public class UserDemonstrationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserDemonstrationService(string baseurl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseurl + "User";
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User Service Demonstration");
                Console.WriteLine("1. Get Users Collection");
                Console.WriteLine("2. Get User by ID");
                Console.WriteLine("3. Add User");
                Console.WriteLine("4. Update User");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DemonstrateGetUsers();
                        break;
                    case "2":
                        await DemonstrateGetUserById();
                        break;
                    case "3":
                        await DemonstrateAddUser();
                        break;
                    case "4":
                        await DemonstrateUpdateUser();
                        break;
                    case "5":
                        await DemonstrateDeleteUser();
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

        private async Task DemonstrateGetUsers()
        {
            Console.Clear();
            Console.WriteLine("Get Users Collection Demonstration");
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

            var url = $"{_baseUrl}?page={page}&itemsPerPage={itemsPerPage}";

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

        private async Task DemonstrateGetUserById()
        {
            Console.Clear();
            Console.WriteLine("Get User by ID Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int userId;

            if (choice == "1")
            {
                userId = 1;
            }
            else
            {
                Console.Write("Enter user ID: ");
                userId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{userId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateAddUser()
        {
            Console.Clear();
            Console.WriteLine("Add User Demonstration");
            Console.WriteLine("1. Use predefined user");
            Console.WriteLine("2. Enter custom user details");

            var choice = Console.ReadLine();
            User user;

            if (choice == "1")
            {
                user = new User
                {
                    Username = "demo_user",
                    Password = "demo_password",
                    Email = "demo@example.com",
                    FirstName = "Demo",
                    LastName = "User",
                    IsActive = true,
                    IsStaff = false,
                    IsSuperuser = false,
                    DateJoined = DateTime.Now
                };
            }
            else
            {
                user = new User();
                Console.Write("Enter username: ");
                user.Username = Console.ReadLine() ?? "";

                Console.Write("Enter password: ");
                user.Password = Console.ReadLine() ?? "";

                Console.Write("Enter email: ");
                user.Email = Console.ReadLine() ?? "";

                Console.Write("Enter first name (press Enter to skip): ");
                user.FirstName = Console.ReadLine();

                Console.Write("Enter last name (press Enter to skip): ");
                user.LastName = Console.ReadLine();

                Console.Write("Is active? (y/n): ");
                user.IsActive = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Is staff? (y/n): ");
                user.IsStaff = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Is superuser? (y/n): ");
                user.IsSuperuser = Console.ReadLine()?.ToLower() == "y";

                user.DateJoined = DateTime.Now;
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, user);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateUpdateUser()
        {
            Console.Clear();
            Console.WriteLine("Update User Demonstration");
            Console.WriteLine("1. Use predefined update");
            Console.WriteLine("2. Enter custom update details");

            var choice = Console.ReadLine();
            User user;
            int prevId;

            if (choice == "1")
            {
                prevId = 1;
                user = new User
                {
                    Id = 1,
                    Username = "updated_user",
                    Password = "updated_password",
                    Email = "updated@example.com",
                    FirstName = "Updated",
                    LastName = "User",
                    IsActive = true,
                    IsStaff = true,
                    IsSuperuser = false,
                    DateJoined = DateTime.Now
                };
            }
            else
            {
                Console.Write("Enter previous user ID: ");
                prevId = int.Parse(Console.ReadLine() ?? "1");

                user = new User();
                Console.Write("Enter new user ID: ");
                user.Id = int.Parse(Console.ReadLine() ?? "1");

                Console.Write("Enter username: ");
                user.Username = Console.ReadLine() ?? "";

                Console.Write("Enter password: ");
                user.Password = Console.ReadLine() ?? "";

                Console.Write("Enter email: ");
                user.Email = Console.ReadLine() ?? "";

                Console.Write("Enter first name (press Enter to skip): ");
                user.FirstName = Console.ReadLine();

                Console.Write("Enter last name (press Enter to skip): ");
                user.LastName = Console.ReadLine();

                Console.Write("Is active? (y/n): ");
                user.IsActive = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Is staff? (y/n): ");
                user.IsStaff = Console.ReadLine()?.ToLower() == "y";

                Console.Write("Is superuser? (y/n): ");
                user.IsSuperuser = Console.ReadLine()?.ToLower() == "y";

                user.DateJoined = DateTime.Now;
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", user);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateDeleteUser()
        {
            Console.Clear();
            Console.WriteLine("Delete User Demonstration");
            Console.WriteLine("1. Use predefined ID");
            Console.WriteLine("2. Enter custom ID");

            var choice = Console.ReadLine();
            int userId;

            if (choice == "1")
            {
                userId = 1;
            }
            else
            {
                Console.Write("Enter user ID to delete: ");
                userId = int.Parse(Console.ReadLine() ?? "1");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{userId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\nResponse:\n{JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true })}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsSuperuser { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
