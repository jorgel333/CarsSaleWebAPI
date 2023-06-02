using CarSalesWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebAPI.Data.Seeds
{
    public class SendInitialUser
    {
        private readonly ModelBuilder _builder;

        public SendInitialUser(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void SeedUser(string password)
        {
            _builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Root",
                    Email = "admin@mail.com",
                    IsDeleted = false,
                    IsAdmin = true,
                    Password = password
                });
        }
    }
}
