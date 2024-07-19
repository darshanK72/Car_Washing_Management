using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByOrderIdAsync(int OrderId);
        Task<User> GetUserByIdAsync(int UserId);
    }
}
