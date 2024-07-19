using CarWashAPI.DTO;
using CarWashAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Interface
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetAllUserOrders(int userId);
        Task<IEnumerable<Order>> GetAllWasherOrders(int washerId);
        //Task<string> GenerateReportAsync(string orderNumber, string washerName, string type, string service, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> UpdateUserStatusAsync(int userId, bool isActive);
        Task<IEnumerable<Washer>> GetWashersAsync();
        Task<Washer> AddWasherAsync(Washer washer);
        Task<Washer> UpdateWasherAsync(Washer washer);
        Task<bool> UpdateWasherStatusAsync(int washerId, bool isActive);
        Task<IEnumerable<Review>> GetWasherReviewsAsync(int washerId);
        Task<IEnumerable<Review>> GetUserReviewsAsync(int userId);
        Task<string> ExportWasherReportAsync(int washerId);
        Task<IEnumerable<Order>> GetFilteredOrdersAsync(string status);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
        Task<Receipt> GetReceiptByOrderIdAsync(int orderId);
        Task<Review> GetReviewByOrderIdAsync(int orderId);
        Task<IEnumerable<Washer>> GetLeaderboardAsync();
        Task<Report> GenerateReportsAsync(DateTime startDate, DateTime endDate);
        Task<bool> AssignWasherToOrder(int orderId, int washerId);
    }
}
