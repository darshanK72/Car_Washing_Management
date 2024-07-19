using CarWashAPI.DTO;
using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IWasherRepository
    {
        Task<IEnumerable<Washer>> GetAllWashersAsync();
        Task<Washer> GetWasherByIdAsync(int washerId);
        Task<Washer> AddWasherAsync(Washer washer);
        Task<Washer> UpdateWasherAsync(Washer washer);
        Task<bool> DeleteWasherAsync(int washerId);
        Task<bool> AcceptOrderAsync(int orderId);
        Task<bool> RejectOrderAsync(int orderId);
        Task<IEnumerable<WashRequest>> GetWashingRequests(int washerId);
        Task<IEnumerable<Order>> GetAllWasherOrders(int washerId);
    }
}
