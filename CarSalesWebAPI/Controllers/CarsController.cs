using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Pagination;
using CarSalesWebAPI.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
                return Ok(cars.Value);
            }
            return BadRequest(cars.Message);
        }

        [HttpGet("carspaged")]
        public async Task<ActionResult<IEnumerable<CarsAllDto>>> GetPagination([FromQuery] CarsParameters carsParameters, CancellationToken cancellationToken)
        {
            var result = await _carService.GetAllPagination(carsParameters, cancellationToken);

            if (result.Success)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Message);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CarDetailsDto>> GetDetails(int id, CancellationToken cancellationToken)
        {
            var newCar = await _carService.GetCarsDetails(id, cancellationToken);

            if (newCar.Success)
            {
                return Ok(newCar.Value);
            }
            return BadRequest(newCar.Message);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateCar(CreateCarDto carDto, CancellationToken cancellationToken)
        {
            var newCar = await _carService.CreateCar(carDto, cancellationToken);
            
            if (newCar.Success)
            {
                return CreatedAtAction(nameof(GetDetails), new {id = newCar.Value.Id}, newCar.Value);
            }
            return BadRequest(newCar.Message);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<CarsAllDto>>> GetFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var carsFilter = await _carService.GetFilters(model, brand, type, year, cancellationToken);
            
            if (carsFilter.Success)
            {
                return Ok(carsFilter);
            }
            return BadRequest(carsFilter.Message);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await _carService.DeleteCar(id, cancellationToken);
            
            if (car.Success)
            {
                return NoContent();
            }
            return BadRequest(car.Message);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateCar(int id, UpdateCarDto carDto, CancellationToken cancellationToken)
        {
            var carFind = await _carService.UpdateCar(id, carDto, cancellationToken);
            
            if (carFind.Success)
            {
                return NoContent();
            }
            return BadRequest(carFind.Message);
        }
    }
}
