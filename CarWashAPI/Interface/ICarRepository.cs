using CarWashAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Interface
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int carId);
        Task<IEnumerable<Car>> GetCarByUserIdAsync(int UserId);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int carId);
    }
}
