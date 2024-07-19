using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailRepository _emailRepository;

        public AdminRepository(ApplicationDbContext context, IEmailRepository emailRepository)
        {
            _context = context;
            _emailRepository = emailRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Washer)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .ToListAsync();
        }

        //public async Task<string> GenerateReportAsync(string orderNumber, string washerName, string type, string service, DateTime? startDate, DateTime? endDate)
        //{
        //    var query = _context.Orders.AsQueryable();

        //    if (!string.IsNullOrEmpty(orderNumber))
        //        query = query.Where(o => o.OrderId.ToString().Contains(orderNumber));

        //    if (!string.IsNullOrEmpty(washerName))
        //        query = query.Where(o => o.Washer.Name.Contains(washerName));

        //    if (!string.IsNullOrEmpty(type))
        //        query = query.Where(o => o.Car.Make.Contains(type) || o.Car.Model.Contains(type));

        //    if (!string.IsNullOrEmpty(service))
        //        query = query.Where(o => o.Package.Name.Contains(service));

        //    if (startDate.HasValue)
        //        query = query.Where(o => o.ScheduledDate >= startDate.Value);

        //    if (endDate.HasValue)
        //        query = query.Where(o => o.ScheduledDate <= endDate.Value);

        //    var orders = await query
        //        .Include(o => o.User)
        //        .Include(o => o.Washer)
        //        .Include(o => o.Car)
        //        .Include(o => o.Package)
        //        .ToListAsync();

        //    var report = new StringBuilder();
        //    report.AppendLine("OrderId,UserId,WasherName,CarMake,CarModel,PackageName,Status,ScheduledDate,ActualWashDate,TotalPrice");
        //    foreach (var order in orders)
        //    {
        //        report.AppendLine($"{order.OrderId},{order.UserId},{order.Washer.Name},{order.Car.Make},{order.Car.Model},{order.Package.Name},{order.Status},{order.ScheduledDate},{order.ActualWashDate},{order.TotalPrice}");
        //    }
        //    return report.ToString();
        //}

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Where(u => u.Role == "User").ToListAsync();
        }

        public async Task<bool> UpdateUserStatusAsync(int userId, bool isActive)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = isActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Washer>> GetWashersAsync()
        {
            return await _context.Washers.ToListAsync();
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

        public async Task<bool> UpdateWasherStatusAsync(int washerId, bool isActive)
        {
            var washer = await _context.Washers.FindAsync(washerId);
            if (washer == null) return false;

            washer.IsActive = isActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetWasherReviewsAsync(int washerId)
        {
            return await _context.Reviews.Where(r => r.WasherId == washerId).Include(r => r.User).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetUserReviewsAsync(int userId)
        {
            return await _context.Reviews.Where(r => r.UserId == userId).Include(r => r.User).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllUserOrders(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllWasherOrders(int washerId)
        {
            return await _context.Orders.Where(o => o.WasherId == washerId).Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .ToListAsync();
        }
        public async Task<string> ExportWasherReportAsync(int washerId)
        {
            var washer = await _context.Washers.FindAsync(washerId);
            if (washer == null) return "Washer not found";

            var orders = await _context.Orders
                .Where(o => o.WasherId == washerId)
                .Include(o => o.User)
                .Include(o => o.Package)
                .ToListAsync();

            var report = new StringBuilder();
            report.AppendLine($"Report for Washer: {washer.Name}");
            report.AppendLine("OrderId,UserName,CarMake,CarModel,PackageName,Status,ScheduledDate,ActualWashDate,TotalPrice");
            foreach (var order in orders)
            {
                report.AppendLine($"{order.OrderId},{order.User.Name},{order.Car.Make},{order.Car.Model},{order.Package.Name},{order.Status},{order.ScheduledDate},{order.ActualWashDate},{order.TotalPrice}");
            }
            return report.ToString();
        }

        public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(string status)
        {
            return await _context.Orders
                .Where(o => o.Status == status)
                .Include(o => o.User)
                .Include(o => o.Washer)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return await _context.Payments.Where(p => p.PaymentId == order.PaymentId).
                FirstOrDefaultAsync();
        }

        public async Task<Receipt> GetReceiptByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return await _context.Receipts.Where(r => r.ReceiptId == order.ReceiptId).
                FirstOrDefaultAsync();
        }

        public async Task<Review> GetReviewByOrderIdAsync(int orderId)
        {
            return await _context.Reviews
                .Where(r => r.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Washer>> GetLeaderboardAsync()
        {
            return await _context.Washers
                .OrderByDescending(w => w.Orders.Count)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Report> GenerateReportsAsync(DateTime startDate, DateTime endDate)
        {
            var report = new Report();

            var orders = await _context.Orders
                .Where(o => o.ScheduledDate >= startDate && o.ScheduledDate <= endDate)
                .ToListAsync();

            report.OrderReport = new OrderReport
            {
                TotalOrders = orders.Count.ToString(),
                TotalRevenue = orders.Sum(o => (double)o.TotalPrice),
                Orders = orders,
                GeneratedDate = DateTime.Now
            };

            var washerIds = orders.Select(o => o.WasherId).Distinct();
            var washerReports = new List<IndividualWasherReport>();

            foreach (var washerId in washerIds)
            {
                var washerOrders = orders.Where(o => o.WasherId == washerId).ToList();
                washerReports.Add(new IndividualWasherReport
                {
                    WasherId = washerId,
                    TotalOrders = washerOrders.Count,
                    TotalRevenue = washerOrders.Sum(o => (double)o.TotalPrice)
                });
            }

            report.WashersReport = new WashersReport
            {
                TotalWashersRevenue = washerReports.Sum(wr => wr.TotalRevenue),
                WasherReports = washerReports,
                GeneratedDate = DateTime.Now
            };

            return report;
        }

        public async Task<bool> AssignWasherToOrder(int orderId, int washerId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            var washer = await _context.Washers.FindAsync(washerId);
            if (washer == null)
                return false;

            order.WasherId = washerId;
            order.Status = "Assigned";

            await _context.SaveChangesAsync();

            await _emailRepository.NotifyWasherNewOrder(washer.Email, order.OrderId);
            return true;
        }

    }
}
