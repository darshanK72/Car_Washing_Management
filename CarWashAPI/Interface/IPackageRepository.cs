using CarWashAPI.Model;

namespace CarWashAPI.Interface
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package> GetPackageByIdAsync(int packageId);
        Task<Package> AddPackageAsync(Package package);
        Task<Package> UpdatePackageAsync(Package package);
        Task<bool> DeletePackageAsync(int packageId);
    }
}
