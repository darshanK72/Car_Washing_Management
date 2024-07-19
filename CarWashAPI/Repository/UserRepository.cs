
using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarWashAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int UserId)
        {
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<User> GetUserByOrderIdAsync(int OrderId)
        {
            var order = await _context.Orders.FindAsync(OrderId);
            var user = await _context.Users.FindAsync(order.UserId);
            return user;
        }
    }
}
