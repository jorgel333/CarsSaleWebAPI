using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Domain.Interfaces.Repositorys
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersDisable(CancellationToken cancellation);
        Task<IEnumerable<User>> GetUserOrderName(CancellationToken cancellation);
    }
}
