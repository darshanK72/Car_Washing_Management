using Microsoft.EntityFrameworkCore;

namespace CarWashAPI.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Washer> Washers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}

