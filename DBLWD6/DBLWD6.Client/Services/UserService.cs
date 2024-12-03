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

        public UserService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl + "User";
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
            bool includeProfile = false;

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

                Console.Write("Include profile? (y/n): ");
                includeProfile = Console.ReadLine()?.ToLower() == "y";
            }

            StringBuilder url = new($"{_baseUrl}?");
            url.Append(page == null ? "" : $"page={page}&");
            url.Append(itemsPerPage == null ? "" : $"itemsPerPage={itemsPerPage}&");
            url.Append($"includeProfile={includeProfile}");
            Console.WriteLine('\n' + url.ToString() + '\n');

            try
            {
                var response = await _httpClient.GetAsync(url.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"First Name: {user.FirstName}");
                    Console.WriteLine($"Last Name: {user.LastName}");
                    Console.WriteLine($"Is Staff: {user.IsStaff}");
                    Console.WriteLine($"Is Active: {user.IsActive}");
                    Console.WriteLine($"Date Joined: {user.DateJoined}");
                    if (includeProfile && user.Profile != null)
                    {
                        Console.WriteLine($"Profile Photo: {user.Profile.Photo}");
                        Console.WriteLine($"Profile Is Non-Secretive: {user.Profile.NonSecretive}");
                    }
                    Console.WriteLine();
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

            Console.Write("Include profile? (y/n): ");
            bool includeProfile = Console.ReadLine()?.ToLower() == "y";

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}?includeProfile={includeProfile}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"\nID: {user.Id}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Is Staff: {user.IsStaff}");
                Console.WriteLine($"Is Active: {user.IsActive}");
                Console.WriteLine($"Date Joined: {user.DateJoined}");
                if (includeProfile && user.Profile != null)
                {
                    Console.WriteLine($"Profile Photo: {user.Profile.Photo}");
                    Console.WriteLine($"Profile Is Non-Secretive: {user.Profile.NonSecretive}");
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

            Console.Write("Include profile? (y/n): ");
            bool includeProfile = Console.ReadLine()?.ToLower() == "y";

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/byEmail/{email}?includeProfile={includeProfile}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("User not found");
                    return;
                }

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"\nID: {user.Id}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Is Staff: {user.IsStaff}");
                Console.WriteLine($"Is Active: {user.IsActive}");
                Console.WriteLine($"Date Joined: {user.DateJoined}");
                if (includeProfile && user.Profile != null)
                {
                    Console.WriteLine($"Profile Photo: {user.Profile.Photo}");
                    Console.WriteLine($"Profile Is Non-Secretive: {user.Profile.NonSecretive}");
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
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, user);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User created successfully");
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
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}?prevId={prevId}", user);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User updated successfully");
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
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User deleted successfully");
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
        }

        private async Task DemonstrateValidateCredentials()
        {
            Console.Clear();
            Console.WriteLine("Validate Credentials Demonstration");

            var loginModel = new LoginModel();
            
            Console.Write("Enter email: ");
            loginModel.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            loginModel.Password = Console.ReadLine();

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/validate", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Credentials are valid");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Invalid credentials");
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
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
