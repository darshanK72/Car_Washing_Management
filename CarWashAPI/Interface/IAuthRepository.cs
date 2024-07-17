using CarWashAPI.DTO;
using CarWashAPI.Model;
using CarWashAPI.Repository;

namespace CarWashAPI.Interface
{
    public interface IAuthRepository
    {
        Task<RegistrationResult> RegisterUserAsync(RegisterDto registerDto);
        Task<dynamic> LoginAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
        Task<Washer> GetWasherByEmailAsync(string email);
        Task<bool> UpdateUserAsync(dynamic user);
        Task<bool> DeleteUserAsync(string email);
    }
}
