using Microsoft.EntityFrameworkCore;
using COMP003B.FinalProject.CarMaintenance.Models;

namespace COMP003B.FinalProject.CarMaintenance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<CarMaintenanceEntry> CarMaintenanceEntries { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
    }
}
