using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("allusers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellation)
        {
            var search = await _userService.GetAllUsers(cancellation);

            if (search.Success)
            {
                return Ok(search);
            }
            return NotFound(search.Message);
        }

        [HttpGet("allusersdesable")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetDisable(CancellationToken cancellation)
        {
            var search = await _userService.GetAllCommonUsersDisable(cancellation);

            if(search.Success)
            {
                return Ok(search);
            }
            return NotFound(search.Message);
        }

        [HttpGet("{id}/userid")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(id, cancellationToken);

            if (user.Success)
            {
                return Ok(user);
            }
            return NotFound(user.Message);
        }

        [HttpPost("createcommonuser")]
        public async Task<ActionResult> CreateUser(CreateCommonUserDto userDto, CancellationToken cancellation)
        {
            var commonUser = await _userService.CreateCommonUser(userDto, cancellation);

            if (commonUser.Success)
            {
                return Ok();
            }
            return BadRequest(commonUser.Message);
        }

        [HttpPost("createadm")]
        public async Task<ActionResult> CreateAdm(CreateUserAdmDto userDto, CancellationToken cancellation)
        {
            var commonUser = await _userService.CreateAdm(userDto, cancellation);

            if (commonUser.Success)
            {
                return Ok();
            }
            return BadRequest(commonUser.Message);
        }

        [HttpDelete("{id:int}/deleteuser")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(int id, CancellationToken cancellation)
        {
            var comonUser = await _userService.DeleteUser(id, cancellation);

            if (comonUser.Success)
            {
                return NoContent();
            }
            return BadRequest(comonUser.Message);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellation)
        {
            var result = await _userService.UpdateUser(id, userDto, cancellation);

            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUser, CancellationToken cancellation)
        {
            var result = await _userService.Login(loginUser, cancellation);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
