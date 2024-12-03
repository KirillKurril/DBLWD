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
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public UserService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "User";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task DemonstrateAllMethods()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User Service Demonstration");
                Console.WriteLine("1. Get Users Collection");
                Console.WriteLine("2. Get User by ID");
                Console.WriteLine("3. Get User by Email");
                Console.WriteLine("4. Add User");
                Console.WriteLine("5. Update User");
                Console.WriteLine("6. Delete User");
                Console.WriteLine("7. Validate Credentials");
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
                        await DemonstrateGetUserByEmail();
                        break;
                    case "4":
                        await DemonstrateAddUser();
                        break;
                    case "5":
                        await DemonstrateUpdateUser();
                        break;
                    case "6":
                        await DemonstrateDeleteUser();
                        break;
                    case "7":
                        await DemonstrateValidateCredentials();
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

            try
            {
                var users = await GetUsersCollection(page, itemsPerPage);
                foreach (var user in users)
                {
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"First Name: {user.FirstName}");
                    Console.WriteLine($"Last Name: {user.LastName}");
                    Console.WriteLine($"Is Staff: {user.IsStaff}");
                    Console.WriteLine($"Is Active: {user.IsActive}");
                    Console.WriteLine($"Date Joined: {user.DateJoined}");
                    if (user.Profile != null)
                    {
                        Console.WriteLine("\nProfile Information:");
                        Console.WriteLine($"Photo: {user.Profile.Photo}");
                        Console.WriteLine($"Non-Secretive: {user.Profile.NonSecretive}");
                    }
                }
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
            
            Console.Write("Enter user ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            try
            {
                var user = await GetUserById(id);
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Is Staff: {user.IsStaff}");
                Console.WriteLine($"Is Active: {user.IsActive}");
                Console.WriteLine($"Date Joined: {user.DateJoined}");
                if (user.Profile != null)
                {
                    Console.WriteLine("\nProfile Information:");
                    Console.WriteLine($"Photo: {user.Profile.Photo}");
                    Console.WriteLine($"Non-Secretive: {user.Profile.NonSecretive}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateGetUserByEmail()
        {
            Console.Clear();
            Console.WriteLine("Get User by Email Demonstration");
            
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();

            try
            {
                var user = await GetUserByEmail(email);
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Is Staff: {user.IsStaff}");
                Console.WriteLine($"Is Active: {user.IsActive}");
                Console.WriteLine($"Date Joined: {user.DateJoined}");
                if (user.Profile != null)
                {
                    Console.WriteLine("\nProfile Information:");
                    Console.WriteLine($"Photo: {user.Profile.Photo}");
                    Console.WriteLine($"Non-Secretive: {user.Profile.NonSecretive}");
                }
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

            var user = new User();
            
            Console.Write("Enter email: ");
            user.Email = Console.ReadLine();

            Console.Write("Enter username: ");
            user.Username = Console.ReadLine();

            Console.Write("Enter password: ");
            user.Password = Console.ReadLine();

            Console.Write("Enter first name (optional, press Enter to skip): ");
            user.FirstName = Console.ReadLine();

            Console.Write("Enter last name (optional, press Enter to skip): ");
            user.LastName = Console.ReadLine();

            Console.Write("Is staff? (y/n): ");
            user.IsStaff = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Is superuser? (y/n): ");
            user.IsSuperuser = Console.ReadLine()?.ToLower() == "y";

            user.IsActive = true;
            user.DateJoined = DateTime.Now;

            try
            {
                var success = await AddUser(user);
                if (success)
                {
                    Console.WriteLine("User created successfully");
                }
                else
                {
                    Console.WriteLine("Failed to create user");
                }
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

            Console.Write("Enter user ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int prevId))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            var user = new User();
            
            Console.Write("Enter new email: ");
            user.Email = Console.ReadLine();

            Console.Write("Enter new username: ");
            user.Username = Console.ReadLine();

            Console.Write("Enter new password (press Enter to keep existing): ");
            var password = Console.ReadLine();
            if (!string.IsNullOrEmpty(password))
            {
                user.Password = password;
            }

            Console.Write("Enter new first name (optional, press Enter to skip): ");
            user.FirstName = Console.ReadLine();

            Console.Write("Enter new last name (optional, press Enter to skip): ");
            user.LastName = Console.ReadLine();

            Console.Write("Is staff? (y/n): ");
            user.IsStaff = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Is superuser? (y/n): ");
            user.IsSuperuser = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Is active? (y/n): ");
            user.IsActive = Console.ReadLine()?.ToLower() == "y";

            try
            {
                var success = await UpdateUser(user, prevId);
                if (success)
                {
                    Console.WriteLine("User updated successfully");
                }
                else
                {
                    Console.WriteLine("Failed to update user");
                }
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

            Console.Write("Enter user ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format");
                return;
            }

            try
            {
                var success = await DeleteUser(id);
                if (success)
                {
                    Console.WriteLine("User deleted successfully");
                }
                else
                {
                    Console.WriteLine("Failed to delete user");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DemonstrateValidateCredentials()
        {
            Console.Clear();
            Console.WriteLine("Validate Credentials Demonstration");

            Console.Write("Enter email: ");
            var email = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            try
            {
                var isValid = await ValidateCredentials(email, password);
                if (isValid)
                {
                    Console.WriteLine("Credentials are valid");
                }
                else
                {
                    Console.WriteLine("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetUsersCollection(int? page, int? itemsPerPage)
        {
            string url = $"{_baseUrl}?includeProfile=true";
            if (page.HasValue)
            {
                url += $"&page={page.Value}";
            }
            if (itemsPerPage.HasValue)
            {
                url += $"&itemsPerPage={itemsPerPage.Value}";
            }

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<User>>(content, _jsonSerializerOptions) ?? Array.Empty<User>();
            }
            return Array.Empty<User>();
        }

        public async Task<User?> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}?includeProfile=true");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
            }
            return null;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/byEmail/{email}?includeProfile=true");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
            }
            return null;
        }

        public async Task<bool> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(User user, int prevId)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var credentials = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/validate", credentials);
            return response.IsSuccessStatusCode;
        }
    }
}
