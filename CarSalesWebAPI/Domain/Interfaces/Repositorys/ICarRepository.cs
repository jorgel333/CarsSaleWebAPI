using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Pagination;
using CarSalesWebAPI.Services;

namespace CarSalesWebAPI.Domain.Interfaces.Repositorys
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllOrder();
        Task<PagedList<Car>> GetPagination(CarsParameters carsParameters, CancellationToken cancellationToken);
        Task<Car> GetByIdToAssessments(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Car>> GetCarsFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken);
        void UpdateAverage(Car car);
    }
}
