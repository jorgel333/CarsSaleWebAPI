using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarsAllDto>>> GetAll(CancellationToken cancellationToken)
        {
            var cars = await _carService.GetAllCars(cancellationToken);

            if (cars.Success)
            {
                return Ok(cars);
            }
            return BadRequest(cars.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDetailsDto>> GetDetails(int id, CancellationToken cancellationToken)
        {
            var newCar = await _carService.GetCarsDetails(id, cancellationToken);

            if (newCar.Success)
            {
                return Ok(newCar);
            }
            return BadRequest(newCar.Message);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCar(Car car, CancellationToken cancellationToken)
        {
            var newCar = await _carService.CreateCar(car, cancellationToken);
            
            if (newCar.Success)
            {
                return Ok(newCar);
            }
            return BadRequest(newCar.Message);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Car>>> GetFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var carsFilter = await _carService.GetFilters(model, brand, type, year, cancellationToken);
            
            if (carsFilter.Success)
            {
                return Ok(carsFilter);
            }
            return BadRequest(carsFilter.Message);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await _carService.DeleteCar(id, cancellationToken);
            
            if (car.Success)
            {
                return Ok(car);
            }
            return BadRequest(car.Message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCar(int id, Car car, CancellationToken cancellationToken)
        {
            var carFind = await _carService.UpdateCar(id, car, cancellationToken);
            
            if (carFind.Success)
            {
                return Ok(carFind);
            }
            return BadRequest(carFind.Message);
        }

    }
}
