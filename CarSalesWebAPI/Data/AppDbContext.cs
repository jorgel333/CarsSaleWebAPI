using CarSalesWebAPI.Data.Seeds;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.SecurityServices.CryptographyService;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            
            var crypto = new Cryptography();
            var senha = crypto.EncryptPassword("admin");
            new SendInitialUser(modelBuilder).SeedUser(senha);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AssessmentRecord> AssessmentRecords { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
