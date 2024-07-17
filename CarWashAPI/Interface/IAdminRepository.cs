using CarWashAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Interface
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<string> GenerateReportAsync();
    }
}
