using DBLWD6.Domain.Entities;

namespace DBLWD6.API.Services
{
    public interface IUserService
    {
        Task<ResponseData<IEnumerable<User>>> GetUsersCollection(int? page, int? itemsPerPage, bool includeProfile = false);
        Task<ResponseData<User>> GetUserById(int id, bool includeProfile = false);
        Task<ResponseData<User>> GetUserByEmail(string email, bool includeProfile = false);
        Task<ResponseData<bool>> AddUser(User user);
        Task<ResponseData<bool>> UpdateUser(User user, int prevId);
        Task<ResponseData<bool>> DeleteUser(int id);
        Task<ResponseData<bool>> ValidateCredentials(string email, string password);
    }
}
