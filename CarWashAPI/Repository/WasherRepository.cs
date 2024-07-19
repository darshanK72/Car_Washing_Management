using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Repository
{
    public class WasherRepository : IWasherRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailRepository _emailRepository;

        public WasherRepository(ApplicationDbContext context, IEmailRepository emailRepository)
        {
            _context = context;
            _emailRepository = emailRepository;
        }

        public async Task<IEnumerable<Washer>> GetAllWashersAsync()
        {
            return await _context.Washers.ToListAsync();
        }

        public async Task<Washer> GetWasherByIdAsync(int washerId)
        {
            return await _context.Washers.FindAsync(washerId);
        }

        public async Task<Washer> AddWasherAsync(Washer washer)
        {
            _context.Washers.Add(washer);
            await _context.SaveChangesAsync();
            return washer;
        }

        public async Task<Washer> UpdateWasherAsync(Washer washer)
        {
            _context.Entry(washer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return washer;
        }

        public async Task<bool> DeleteWasherAsync(int washerId)
        {
            var washer = await _context.Washers.FindAsync(washerId);
            if (washer == null)
            {
                return false;
            }

            _context.Washers.Remove(washer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AcceptOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            var user = await _context.Users.FindAsync(order.UserId);
            var washer = await _context.Washers.FindAsync(order.WasherId);
            if (order == null || order.Status != "Assigned")
            {
                return false;
            }

            order.Status = "Accepted";
            await _context.SaveChangesAsync();

            await _emailRepository.NotifyUserOnWashRequestResponse(user.Email, washer.Name, true);
            await _emailRepository.NotifyWasherScheduledWash(washer.Email, $"<h3>Address {user.Address}</h3><h3>Note By User : {order.Notes}</h3>");
            return true;
        }

        public async Task<bool> RejectOrderAsync(int orderId)
        {
            var order = await _context.Orders.Include(o => o.User).Include(o => o.Washer).SingleOrDefaultAsync();
            if (order == null || order.Status != "Assigned")
            {
                return false;
            }

            order.Status = "Rejected";
            order.Washer = null;
            order.Washer = null;
            await _context.SaveChangesAsync();
            await _emailRepository.NotifyUserOnWashRequestResponse(order.User.Email, order.Washer.Name, false);
            return true;
        }

        public async Task<IEnumerable<WashRequest>> GetWashingRequests(int washerId)
        {
            var washRequests = await _context.Orders.Include(o => o.User)
                .Where(o => o.WasherId == washerId && o.Status == "Assigned")
                .Select(o => new WashRequest()
                {
                    OrderId = o.OrderId,
                    Status = o.Status,
                    ScheduledDate = o.ScheduledDate,
                    ActualWashDate = o.ActualWashDate,
                    TotalPrice = o.TotalPrice,
                    Notes = o.Notes,
                    UserId = o.UserId,
                    Address = o.User.Address,
                })
                .ToListAsync();
            return washRequests;
        }

        public async Task<IEnumerable<Order>> GetAllWasherOrders(int washerId)
        {
            return await _context.Orders.Where(o => o.WasherId == washerId).Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .ToListAsync();
        }
    }
}
