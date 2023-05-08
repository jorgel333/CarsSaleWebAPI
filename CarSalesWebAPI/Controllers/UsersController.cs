using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.Interfaces.Services;
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
        public async Task<ActionResult<IEnumerable<User>>> GetAll(CancellationToken cancellation)
        {
            var search = await _userService.GetAllUsers(cancellation);

            if (search.Success)
            {
                return Ok(search);
            }
            return BadRequest(search.Message);
        }

        [HttpGet("allusersdesable")]
        public async Task<ActionResult<IEnumerable<User>>> GetDisable(CancellationToken cancellation)
        {
            var search = await _userService.GetAllCommonUsersDisable(cancellation);

            if(search.Success)
            {
                return Ok(search);
            }
            return BadRequest(search.Message);
        }

        [HttpPost("createcommonuser")]
        public async Task<ActionResult> CreateUser(CreateUserDto userDto, CancellationToken cancellation)
        {
            var commonUser = await _userService.CreateCommonUser(userDto, cancellation);

            if (commonUser.Success)
            {
                return Ok(); //Retornar um newroute quando implementar o dto para transferencia de dados
            }
            return BadRequest(commonUser.Message);
        }

        [HttpPost("createadm")]
        public async Task<ActionResult> CreateAdm(CreateUserDto userDto, CancellationToken cancellation)
        {
            var commonUser = await _userService.CreateAdm(userDto, cancellation);

            if (commonUser.Success)
            {
                return Ok();
            }
            return BadRequest(commonUser.Message);
        }

        [HttpDelete("{id:int}/deleteanyuser")]
        public async Task<ActionResult> DeleteAnyUser(int id, CancellationToken cancellation)
        {
            var comonUser = await _userService.DeleteUser(id, cancellation);

            if (comonUser.Success)
            {
                return Ok(comonUser);
            }
            return BadRequest(comonUser.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellation)
        {
            var result = await _userService.UpdateUser(id, userDto, cancellation);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
