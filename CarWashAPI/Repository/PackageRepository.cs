using CarWashAPI.Interface;
using CarWashAPI.Repository;
using CarWashAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Models
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Package>> GetAllPackagesAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetPackageByIdAsync(int packageId)
        {
            return await _context.Packages.FindAsync(packageId);
        }

        public async Task<Package> AddPackageAsync(Package package)
        {
            _context.Packages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<Package> UpdatePackageAsync(Package package)
        {
            _context.Entry(package).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<bool> DeletePackageAsync(int packageId)
        {
            var package = await _context.Packages.FindAsync(packageId);
            if (package == null)
            {
                return false;
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

