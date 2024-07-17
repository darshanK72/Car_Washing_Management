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

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<string> GenerateReportAsync()
        {
            var orders = await GetAllOrdersAsync();
            var report = new StringBuilder();
            report.AppendLine("OrderId,UserId,WasherId,CarId,PackageId,Status,ScheduledDate,ActualWashDate,TotalPrice,ReceiptUrl,Notes");

            foreach (var order in orders)
            {
                report.AppendLine($"{order.OrderId},{order.UserId},{order.WasherId},{order.CarId},{order.PackageId},{order.Status},{order.ScheduledDate},{order.ActualWashDate},{order.TotalPrice},{order.ReceiptId},{order.Notes}");
            }

            return report.ToString();
        }
    }
}
