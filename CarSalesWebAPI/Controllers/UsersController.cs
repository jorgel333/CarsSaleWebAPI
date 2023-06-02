using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Dados de todos usuários
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("allusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            var search = await _userService.GetAllUsers(cancellation);

            if (search.Success)
            {
                return Ok(search.Value);
            }
            return NotFound(search.Message);
        }

        /// <summary>
        /// Dados de todos usuários desativados
        /// </summary>
        /// <returns>Coleção de usuários</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("allusersdesable")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDisable(CancellationToken cancellation)
        {
            var search = await _userService.GetAllCommonUsersDisable(cancellation);

            if(search.Success)
            {
                return Ok(search.Value);
            }
            return NotFound(search.Message);
        }

        /// <summary>
        /// Dados de um usuário
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(id, cancellationToken);

            if (user.Success)
            {
                return Ok(user.Value);
            }
            return NotFound(user.Message);
        }

        /// <summary>
        /// Cadastro de um usuário comum
        /// </summary>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad Request</response>
        [AllowAnonymous]
        [HttpPost("createcommonuser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateCommonUserDto userDto, CancellationToken cancellation)
        {
            var commonUser = await _userService.CreateCommonUser(userDto, cancellation);

            if (commonUser.Success)
            {
                return CreatedAtAction(nameof(GetUserById), new {id = commonUser.Value.Id}, commonUser.Value);
            }
            return BadRequest(commonUser.Message);
        }

        /// <summary>
        /// Cadastro de um usuário Administrador
        /// </summary>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad Request</response>
        [Authorize(Roles = "admin")]
        [HttpPost("createadm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdm(CreateUserAdmDto userDto, CancellationToken cancellation)
        {
            var adminUser = await _userService.CreateAdm(userDto, cancellation);

            if (adminUser.Success)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = adminUser.Value.Id }, adminUser.Value);
            }
            return BadRequest(adminUser.Message);
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <response code="204">Success</response>
        /// <response code="400">BadRequest</response>
        [Authorize]
        [HttpDelete("{id:int}/deleteuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellation)
        {
            var comonUser = await _userService.DeleteUser(id, cancellation);

            if (comonUser.Success)
            {
                return NoContent();
            }
            return BadRequest(comonUser.Message);
        }

        /// <summary>
        /// Atualização de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="userDto">Dados do usuário</param>
        /// <param name="cancellation"></param>
        /// <response code="204">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellation)
        {
            var result = await _userService.UpdateUser(id, userDto, cancellation);

            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Fazer Login do usuário
        /// </summary>
        /// <param name="loginUser">Dados de login</param>
        /// <param name="cancellation"></param>
        /// <response code="200">Success</response>
        /// <response code="400">BadRequest</response>
        /// <returns>Token de autorização</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginUserDto loginUser, CancellationToken cancellation)
        {
            var result = await _userService.Login(loginUser, cancellation);

            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Message);
        }
    }
}
