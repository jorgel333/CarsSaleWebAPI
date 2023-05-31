using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Pagination;
using CarSalesWebAPI.Services.Helpers;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface ICarService : IService
    {
        Task<ResponseService<CarsAllDto>> CreateCar(CreateCarDto carDto, CancellationToken cancellationToken);
        Task<ResponseService> UpdateCar(int id, UpdateCarDto carDto, CancellationToken cancellationToken);
        Task<ResponseService> DeleteCar(int id, CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllPagination(CarsParameters carsParameters, CancellationToken cancellationToken);
        Task<ResponseService<CarDetailsDto>> GetCarsDetails(int id, CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllCars(CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<CarsAllDto>>> GetFilters(string model, string brand, string type, int? year, CancellationToken cancellationToken);
    }
}
