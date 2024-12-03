using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using DBLWD6.Domain.Entities;

namespace DBLWD6.API.Services
{
    public class UserService : IUserService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public UserService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        private async Task<Profile?> GetProfileForUser(int userId)
        {
            Expression<Func<Profile, bool>> profilePredicate = p => p.UserId == userId;
            var profiles = await _dbService.ProfileTable.GetWithConditions(profilePredicate);
            return profiles.FirstOrDefault();
        }

        public async Task<ResponseData<IEnumerable<User>>> GetUsersCollection(int? page, int? itemsPerPage, bool includeProfile = false)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<User> users;
            Expression<Func<User, bool>> predicate = u => u.Id >= startIndex && u.Id < endIndex;

            try
            {
                users = await _dbService.UserTable.GetWithConditions(predicate);
                
                // Remove sensitive data before returning
                foreach (var user in users)
                {
                    user.Password = null;
                    if (includeProfile)
                    {
                        user.Profile = await GetProfileForUser(user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<User>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<User>>(users);
        }

        public async Task<ResponseData<User>> GetUserById(int id, bool includeProfile = false)
        {
            User user;
            try
            {
                user = await _dbService.UserTable.GetById(id);
                if (user != null)
                {
                    user.Password = null;
                    if (includeProfile)
                    {
                        user.Profile = await GetProfileForUser(user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<User>(false, ex.Message);
            }

            return new ResponseData<User>(user);
        }

        public async Task<ResponseData<User>> GetUserByEmail(string email, bool includeProfile = false)
        {
            IEnumerable<User> users;
            try
            {
                Expression<Func<User, bool>> predicate = u => u.Email == email;
                users = await _dbService.UserTable.GetWithConditions(predicate);
                var user = users.FirstOrDefault();
                if (user != null)
                {
                    user.Password = null;
                    if (includeProfile)
                    {
                        user.Profile = await GetProfileForUser(user.Id);
                    }
                }
                return new ResponseData<User>(user);
            }
            catch (Exception ex)
            {
                return new ResponseData<User>(false, ex.Message);
            }
        }

        public async Task<ResponseData<bool>> AddUser(User user)
        {
            try
            {
                // Check if email already exists
                var existingUser = await GetUserByEmail(user.Email);
                if (existingUser.Data != null)
                {
                    return new ResponseData<bool>(false, "Email already exists");
                }

                // Hash password before storing
                user.Password = HashPassword(user.Password);
                await _dbService.UserTable.Add(user);

                // Create empty profile for the user
                var profile = new Profile
                {
                    UserId = user.Id,
                    Photo = "images/employees/default_employee.png",
                    NonSecretive = false
                };
                await _dbService.ProfileTable.Add(profile);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateUser(User user, int prevId)
        {
            try
            {
                // If password is being updated, hash it
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = HashPassword(user.Password);
                }
                else
                {
                    // Keep existing password
                    var existingUser = await _dbService.UserTable.GetById(prevId);
                    user.Password = existingUser.Password;
                }

                await _dbService.UserTable.Update(user, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteUser(int id)
        {
            try
            {
                // Profile will be automatically deleted due to CASCADE constraint
                await _dbService.UserTable.Delete(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> ValidateCredentials(string email, string password)
        {
            try
            {
                Expression<Func<User, bool>> predicate = u => u.Email == email;
                var users = await _dbService.UserTable.GetWithConditions(predicate);
                var user = users.FirstOrDefault();

                if (user == null)
                {
                    return new ResponseData<bool>(false, "Invalid credentials");
                }

                string hashedPassword = HashPassword(password);
                if (user.Password != hashedPassword)
                {
                    return new ResponseData<bool>(false, "Invalid credentials");
                }

                return new ResponseData<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
