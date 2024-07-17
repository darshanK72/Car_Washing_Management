using CarWashAPI.DTO;
using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> AddOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<Package> GetPackageByIdAsync(int id);
        Task<Receipt> CreateReceiptAsync(decimal amount, string paymentMethod);
        Task<Order> PlaceOrderAsync(PlaceOrderDTO orderDto, int receiptId);
        Task<Washer> AssignRandomWasherAsync();
    }
}