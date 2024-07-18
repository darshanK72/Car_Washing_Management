using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashAPI.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            return await _context.Cars.FindAsync(carId);
        }

        public async Task<IEnumerable<Car>> GetCarByUserIdAsync(int UserId)
        {
            return await _context.Cars.Where(c => c.UserId == UserId).ToListAsync();
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
