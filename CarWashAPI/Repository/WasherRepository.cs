using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Repository
{
    public class WasherRepository : IWasherRepository
    {
        private readonly ApplicationDbContext _context;

        public WasherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Washer>> GetAllWashersAsync()
        {
            return await _context.Washers.ToListAsync();
        }

        public async Task<Washer> GetWasherByIdAsync(int washerId)
        {
            return await _context.Washers.FindAsync(washerId);
        }

        public async Task<Washer> AddWasherAsync(Washer washer)
        {
            _context.Washers.Add(washer);
            await _context.SaveChangesAsync();
            return washer;
        }

        public async Task<Washer> UpdateWasherAsync(Washer washer)
        {
            _context.Entry(washer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return washer;
        }

        public async Task<bool> DeleteWasherAsync(int washerId)
        {
            var washer = await _context.Washers.FindAsync(washerId);
            if (washer == null)
            {
                return false;
            }

            _context.Washers.Remove(washer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
