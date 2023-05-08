using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebAPI.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetUserOrderName(CancellationToken cancellation)
        {
            return await GetAllActive().OrderBy(x => x.Name).ToListAsync(cancellation);
        }

        public async Task<IEnumerable<User>> GetUsersDisable(CancellationToken cancellation)
        {
            return await GetAllFilter(user => user.IsDeleted == true 
                    && user.IsAdmin == false).ToListAsync(cancellation);
        }
    }
}
