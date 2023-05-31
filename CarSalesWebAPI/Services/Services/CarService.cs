using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Domain.Pagination;
using CarSalesWebAPI.Services.Helpers;
using CarSalesWebAPI.Services.Interfaces.Services;
using System.Net;

namespace CarSalesWebAPI.Services.Services
{
    public class CarService : Service, ICarService
    {
        private readonly IUnityOfWork _uow;
        private readonly IMapper _mapper;
        public CarService(IUnityOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ResponseService<CarsAllDto>> CreateCar(CreateCarDto carDto, CancellationToken cancellationToken)
        {
            var getCar = await _uow.CarRepository.GetById(c => c.Model == carDto.Model 
                                            && c.YearOfManufacture == carDto.YearOfManufacture
                                            && c.Color == carDto.Color
                                            && c.Type == carDto.Type, cancellationToken);

            if (getCar != null)
            {
                return GenerateErroResponse<CarsAllDto>("Esse carro já foi cadastrado", HttpStatusCode.BadRequest);
            }

            var car = _mapper.Map(carDto, getCar);
            _uow.CarRepository.Add(car);
            await _uow.Commit(cancellationToken);
            var carViewDto = _mapper.Map<CarsAllDto>(car);
            return GenerateSuccessResponse(carViewDto, HttpStatusCode.Created);
        }

        public async Task<ResponseService> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await _uow.CarRepository.GetById(c => c.Id == id && c.IsDeleted == false, cancellationToken);
            
            if (car is null)
            {
                return GenerateErroResponse("Carro não encontrado ou já deletado", HttpStatusCode.NotFound);
            }

            _uow.CarRepository.SoftDelete(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessResponse("Carro deletado com sucesso", HttpStatusCode.NoContent);
        }

        public async Task<ResponseService> UpdateCar(int id, UpdateCarDto carDto, CancellationToken cancellationToken)
        {
            var carUp = await _uow.CarRepository.GetById(c => c.Id == id && c.IsDeleted == false, cancellationToken);
            
            if (carUp is null)
            {
                return GenerateErroResponse("Carro não encontrado", HttpStatusCode.NotFound);
            }
            if (id != carDto.Id)
            {
                return GenerateErroResponse("Não pode alterar o id", HttpStatusCode.BadRequest);
            }

            var car = _mapper.Map(carDto, carUp);
            _uow.CarRepository.UpdateEntity(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessResponse("Carro atualizado com sucesso", HttpStatusCode.NoContent);
        }

        public async Task<ResponseService<CarDetailsDto>> GetCarsDetails(int id, CancellationToken cancellationToken)
        {
            var car = await _uow.CarRepository.GetByIdToAssessments(id, cancellationToken);

            if(car is null)
            {
                return GenerateErroResponse<CarDetailsDto>("Carro não encontrado", HttpStatusCode.NotFound);
            }

            var carDetails = _mapper.Map<CarDetailsDto>(car);
            return GenerateSuccessResponse(carDetails, HttpStatusCode.OK);  
        }

        public async Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllCars(CancellationToken cancellationToken)
        {
            var cars = await _uow.CarRepository.GetAllOrder();

            if (!cars.Any())
            {
                return GenerateErroResponse<IEnumerable<CarsAllDto>>("Nenhum carro encontrado", HttpStatusCode.NotFound);
            }

            var carMapper = _mapper.Map<IEnumerable<CarsAllDto>>(cars);
            return GenerateSuccessResponse(carMapper, HttpStatusCode.OK);
        }

        public async Task<ResponseService<IEnumerable<CarsAllDto>>> GetFilters(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var carsFilters = await _uow.CarRepository.GetCarsFilter(model, brand, type, year, cancellationToken);

            if (!carsFilters.Any())
            {
                return GenerateErroResponse<IEnumerable<CarsAllDto>>("Nenhum carro encontrado", HttpStatusCode.NotFound);
            }

            var carsFiltersDto = _mapper.Map<IEnumerable<CarsAllDto>>(carsFilters);
            return GenerateSuccessResponse(carsFiltersDto, HttpStatusCode.OK);
        }
        public async Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllPagination(CarsParameters carsParameters, CancellationToken cancellationToken)
        {
            var pagedCars = await _uow.CarRepository.GetPagination(carsParameters, cancellationToken);
            var carsDto = _mapper.Map<IEnumerable<CarsAllDto>>(pagedCars);
            
            if (!carsDto.Any())
            {
                return GenerateErroResponse<IEnumerable<CarsAllDto>>("Nenhum carro encontrado", HttpStatusCode.NotFound);
            }
            return GenerateSuccessResponse(carsDto, HttpStatusCode.OK);
        }
    }
}
