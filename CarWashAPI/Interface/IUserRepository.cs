using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int UserId);
    }
}
