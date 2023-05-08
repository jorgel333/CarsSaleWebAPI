using CarSalesWebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace CarSalesWebAPI.Domain.Interfaces.Repositorys
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllOrder();
        Task<IEnumerable<Car>> GetByBrand(string brand);
        Task<IEnumerable<Car>> GetByModel(string model);
        Task<IEnumerable<Car>> GetByType(string type);
        Task<IEnumerable<Car>> GetByYearOfManufacture(int year);
        IEnumerable<Car> GetAllToAssessments();
        Task<Car> GetByIdToAssessments(int id, CancellationToken cancellationToken);
    }
}
