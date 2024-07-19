using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailRepository _emailRepository;
        private readonly IReviewRepository _reviewRepository;
        public OrderRepository(ApplicationDbContext context,IReviewRepository  reviewRepository,IEmailRepository emailRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
            _emailRepository = emailRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Washer)
                .Include(o => o.Car)
                .Include(o => o.Package)
                .Include(o => o.Receipt)
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

        public async Task<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId).ToList();

            var orderDTOs = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    WasherId = order.WasherId,
                    CarId = order.CarId,
                    PackageId = order.PackageId,
                    Status = order.Status,
                    ScheduledDate = order.ScheduledDate,
                    ActualWashDate = order.ActualWashDate,
                    TotalPrice = order.TotalPrice,
                    ReceiptId = order.ReceiptId,
                    Notes = order.Notes
                };

                var review = await _reviewRepository.GetReviewsByOrderIdAsync(order.OrderId);
                if (review != null)
                {
                    orderDto.ReviewId = review.ReviewId;
                }
                orderDTOs.Add(orderDto);
            }

                
            return orderDTOs;
        }


        public async Task<List<Receipt>> GetReciptsByIdAsync(int userId)
        {

            var orders = _context.Orders.Where(o => o.UserId == userId).ToList();
            List<Receipt> recipts = new List<Receipt>();
            foreach(var order in orders)
            {
                var r = await _context.Receipts.FindAsync(order.ReceiptId);
                if (r != null)
                {
                    recipts.Add(r);
                }
            }
            return recipts;
        }


        public async Task<Order> CompletePaymentAsync(PaymentDTO paymentDTO)
        {
            var order = await _context.Orders.Include(o => o.Washer)
                .FirstOrDefaultAsync(r => r.OrderId == paymentDTO.OrderId);

            if (order == null)
                throw new ArgumentException("Order not found");

            var recepit = await _context.Receipts.FindAsync(order.ReceiptId);

            if (recepit.Status != "Not Paid")
                throw new InvalidOperationException("Receipt is not in the correct state for payment");

            var user = await _context.Users.FindAsync(order.UserId);

            if (user == null)
                throw new ArgumentException("User not found");

            var payment = new Payment
            {
                TotalAmount = order.TotalPrice,
                PaymentTime = DateTime.UtcNow,
                PaymentType = paymentDTO.PaymentType,
                User = user,
                UserId = user.UserId
            };


            payment.Receipt = recepit ;
            payment.ReceiptId = order.ReceiptId;
            order.Payment = payment;
            order.Receipt = recepit;
            order.Status = "Completed";
            recepit.Status = "Paid";
            recepit.PaymentMethod = paymentDTO.PaymentType;

            await _context.SaveChangesAsync();

            await _emailRepository.NotifyWasherPaymentSuccess(order.Washer.Email, order.OrderId);
            await _emailRepository.NotifyUserOnServiceUpdate(user.Email, order.Washer.Name, "Completed");
            await _emailRepository.NotifyUserOnReceipt(user.Email, recepit.ReceiptId);

            return order;
        }

        public async Task<List<User>> GetUsersSortedByOrdersAsync()
        {
            return await _context.Users
                .Include(u => u.Orders)
                .OrderByDescending(u => u.Orders.Count)
                .ToListAsync();
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

        public async Task<Receipt> CreateReceiptAsync(decimal amount)
        {
            var receipt = new Receipt
            {
                WashingDate = DateTime.Now,
                Amount = amount,
                TransactionId = Guid.NewGuid().ToString(),
                Status = "Created"
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

            var order = new Order
            {
                UserId = orderDto.UserId,
                CarId = orderDto.CarId,
                PackageId = orderDto.PackageId,
                Status = "Created",
                ScheduledDate = orderDto.ScheduleNow ? DateTime.Now : orderDto.ScheduledDate,
                TotalPrice = package.Price,
                Notes = orderDto.Notes,
                ReceiptId = receiptId
            };

            order.ActualWashDate = order.ScheduledDate;

            _context.Orders.Add(order);

            receipt.Status = "Not Paid";

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

