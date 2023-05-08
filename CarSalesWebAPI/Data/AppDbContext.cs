using CarSalesWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AssessmentRecord> AssessmentRecords { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
