using CarSalesWebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace CarSalesWebAPI.Domain.Interfaces.Repositorys
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllOrder();
        Task<Car> GetByIdToAssessments(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Car>> GetCarsFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken);
        void UpdateAverage(Car car);
    }
}
