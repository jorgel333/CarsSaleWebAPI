using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
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
        private readonly IAssessmentRecordService _assessmentRecord;
        public CarsController(ICarService carService, IAssessmentRecordService assessmentRecord)
        {
            _carService = carService;
            _assessmentRecord = assessmentRecord;
        }

        /// <summary>
        /// Informações de todos os carros ordenados por média de avaliação
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <returns>Retornar uma lista com todos os carros</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var cars = await _carService.GetAllCars(cancellationToken);

            if (cars.Success)
            {
                return Ok(cars.Value);
            }
            return NotFound(cars.Message);
        }

        /// <summary>
        /// Iformações de todos os carros paginados
        /// </summary>
        /// <param name="carsParameters">Dados para paginação</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <returns>Retorna uma lista com todos os carros paginados</returns>
        [AllowAnonymous]
        [HttpGet("carspaged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPagination([FromQuery] CarsParameters carsParameters, CancellationToken cancellationToken)
        {
            var result = await _carService.GetAllPagination(carsParameters, cancellationToken);

            if (result.Success)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Message);
        }

        /// <summary>
        /// Detalhes de um carro
        /// </summary>
        /// <param name="id">Identificador do carro</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        /// <responde code="404">NotFound</responde>
        /// <returns>Retorna todos os detalhes de um carro</returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetails(int id, CancellationToken cancellationToken)
        {
            var newCar = await _carService.GetCarsDetails(id, cancellationToken);

            if (newCar.Success)
            {
                return Ok(newCar.Value);
            }
            return NotFound(newCar.Message);
        }

        /// <summary>
        /// Filtro de carros
        /// </summary>
        /// <param name="model">Modelo do carro</param>
        /// <param name="brand">Marca do carro</param>
        /// <param name="type">Tipo do carro</param>
        /// <param name="year">Ano de fabricação do carro</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        /// <responde code="404">NotFound</responde>
        /// <returns>Lista de carros filtrados</returns>
        [AllowAnonymous]
        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var carsFilter = await _carService.GetFilters(model, brand, type, year, cancellationToken);

            if (carsFilter.Success)
            {
                return Ok(carsFilter);
            }
            return NotFound(carsFilter.Message);
        }

        /// <summary>
        /// Cadastro de um carro
        /// </summary>
        /// <param name="carDto">Dados do carro</param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCar(CreateCarDto carDto, CancellationToken cancellationToken)
        {
            var newCar = await _carService.CreateCar(carDto, cancellationToken);
            
            if (newCar.Success)
            {
                return CreatedAtAction(nameof(GetDetails), new {id = newCar.Value.Id}, newCar.Value);
            }
            return BadRequest(newCar.Message);
        }

        /// <summary>
        /// Deleta um carro
        /// </summary>
        /// <param name="id">Identificador do carro</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCar(int id, CancellationToken cancellationToken)
        {
            var car = await _carService.DeleteCar(id, cancellationToken);
            
            if (car.Success)
            {
                return NoContent();
            }
            return BadRequest(car.Message);
        }

        /// <summary>
        /// Atualização de dados de um carro
        /// </summary>
        /// <param name="id">Identificador do carro</param>
        /// <param name="carDto">Dados para atualização</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCar(int id, UpdateCarDto carDto, CancellationToken cancellationToken)
        {
            var carFind = await _carService.UpdateCar(id, carDto, cancellationToken);
            
            if (carFind.Success)
            {
                return NoContent();
            }
            return BadRequest(carFind.Message);
        }

        /// <summary>
        /// Cadastro de avaliação de um carro
        /// </summary>
        /// <param name="rgEvDto">Dados para avaliação do carro</param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns></returns>
        [Authorize]
        [HttpPost("registerevaluation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAssessment(RegisterEvaluationDTO rgEvDto, CancellationToken cancellationToken)
        {
            var assessment = await _assessmentRecord.RegisterEvaluation(rgEvDto, cancellationToken);

            if (assessment.Success)
            {
                return NoContent();
            }
            return BadRequest(assessment.Message);
        }
    }
}
