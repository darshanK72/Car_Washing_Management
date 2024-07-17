using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Washer)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Package> GetPackageByIdAsync(int id)
        {
            return await _context.Packages.FindAsync(id);
        }

        public async Task<Receipt> CreateReceiptAsync(decimal amount, string paymentMethod)
        {
            var receipt = new Receipt
            {
                WashingDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = paymentMethod,
                TransactionId = Guid.NewGuid().ToString(),
                Status = "Pending"
            };

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();

            return receipt;
        }

        public async Task<Order> PlaceOrderAsync(PlaceOrderDTO orderDto, int receiptId)
        {
            var package = await GetPackageByIdAsync(orderDto.PackageId);
            var receipt = await _context.Receipts.FindAsync(receiptId);
            if (package == null)
            {
                throw new ArgumentException("Invalid package ID");
            }

            if (receipt == null)
            {
                throw new ArgumentException("Invalid Receipt ID");
            }

            var washer = orderDto.ScheduleNow ? await AssignRandomWasherAsync() : null;

            var order = new Order
            {
                UserId = orderDto.UserId,
                CarId = orderDto.CarId,
                PackageId = orderDto.PackageId,
                WasherId = washer?.WasherId ?? 0,
                Status = orderDto.ScheduleNow ? "Assigned" : "Scheduled",
                ScheduledDate = orderDto.ScheduleNow ? DateTime.Now : orderDto.ScheduledDate,
                TotalPrice = package.Price,
                Notes = orderDto.Notes,
                ReceiptId = receiptId
            };

            order.ActualWashDate = order.ScheduledDate;

            _context.Orders.Add(order);

            receipt.Status = "Paid";

            await _context.SaveChangesAsync();

            return order;
    }
    
    public async Task<Washer> AssignRandomWasherAsync()
        {
            return await _context.Washers
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }
    }
}

