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
    }
}
