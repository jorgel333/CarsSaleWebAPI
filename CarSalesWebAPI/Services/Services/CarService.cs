using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;

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
        public async Task<ResponseService> CreateCar(Car car, CancellationToken cancellationToken)
        {
            var getCar = await _uow.CarRepository.GetById(c => c.Model == car.Model 
                                            && c.YearOfManufacture == car.YearOfManufacture
                                            && c.Color == car.Color, cancellationToken);

            if (getCar != null)
            {
                return GenerateErrorResponse("Esse carro já foi cadastrado");
            }

            _uow.CarRepository.Add(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Carro cadastrado com sucesso");
        }

        public async Task<ResponseService> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await _uow.CarRepository.GetById(c => c.Id == id && c.IsDeleted == false, cancellationToken);
            
            if (car == null)
            {
                return GenerateErrorResponse("Carro não encontrado ou já deletado");
            }

            _uow.CarRepository.SoftDelete(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Carro Deletado com sucesso");
        }

        public async Task<ResponseService> UpdateCar(int id, Car car, CancellationToken cancellationToken)
        {
            var carUp = await _uow.CarRepository.GetById(c => c.Id == id && c.IsDeleted == false, cancellationToken);
            
            if (carUp is null)
            {
                return GenerateErrorResponse("Carro não encontrado");
            }
            if (id != car.Id)
            {
                return GenerateErrorResponse("Não pode alterar o id");
            }

            _uow.CarRepository.UpdateEntity(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Carro atualizado com sucesso");
        }

        public async Task<ResponseService<CarDetailsDto>> GetCarsDetails(int id, CancellationToken cancellationToken)
        {
            var car = await _uow.CarRepository.GetByIdToAssessments(id, cancellationToken);

            if(car is null)
            {
                return GenerateErroResponse<CarDetailsDto>("Carro não encontrado");
            }

            var carDetails = _mapper.Map<CarDetailsDto>(car);
            return GenerateSuccessfullResponse(carDetails);  
        }

        public async Task<ResponseService<IEnumerable<CarsAllDto>>> GetAllCars(CancellationToken cancellationToken)
        {
            var cars = await _uow.CarRepository.GetAllOrder();

            if (!cars.Any())
            {
                return GenerateErroResponse<IEnumerable<CarsAllDto>>("Nenhum carro encontrado");
            }

            var carMapper = _mapper.Map<IEnumerable<CarsAllDto>>(cars);
            return GenerateSuccessfullResponse(carMapper);
        }

        public async Task<ResponseService<IEnumerable<CarsAllDto>>> GetFilters(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var carsFilters = await _uow.CarRepository.GetCarsFilter(model, brand, type, year, cancellationToken);

            if (!carsFilters.Any())
            {
                return GenerateErroResponse<IEnumerable<CarsAllDto>>("Nenhum carro encontrado");
            }

            var carsFiltersDto = _mapper.Map<IEnumerable<CarsAllDto>>(carsFilters);
            return GenerateSuccessfullResponse(carsFiltersDto);
        }
    }
}
