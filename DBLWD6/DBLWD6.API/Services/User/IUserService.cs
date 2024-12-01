namespace DBLWD6.API.Services
{
    public interface IUserService
    {
        Task<ResponseData<IEnumerable<User>>> GetUsersCollection(int? page, int? itemsPerPage);
        Task<ResponseData<User>> GetUserById(int id);
        Task<ResponseData<User>> GetUserByEmail(string email);
        Task<ResponseData<bool>> AddUser(User user);
        Task<ResponseData<bool>> UpdateUser(User user, int prevId);
        Task<ResponseData<bool>> DeleteUser(int id);
        Task<ResponseData<bool>> ValidateCredentials(string email, string password);
    }
}
