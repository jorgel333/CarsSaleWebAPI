using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface ICarService : IService
    {
        Task<ResponseService> CreateCar(Car car, CancellationToken cancellationToken);
        Task<ResponseService> UpdateCar(int id, Car car, CancellationToken cancellationToken);
        Task<ResponseService> DeleteCar(int id, CancellationToken cancellationToken);
        Task<ResponseService<CarDetailsDto>> GetCarsDetails(int id, CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllCars(CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<CarsAllDto>>> GetFilters(string model, string brand, string type, int? year, CancellationToken cancellationToken);
    }
}
