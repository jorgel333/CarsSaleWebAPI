using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Services.Helpers;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task<ResponseService> CreateCommonUser(CreateCommonUserDto userDto, CancellationToken cancellationToken);
        Task<ResponseService> CreateAdm(CreateUserAdmDto userDto, CancellationToken cancellationToken);
        Task<ResponseService> DeleteUser(int id, CancellationToken cancellationToken);
        Task<ResponseService> UpdateUser(int id, UpdateUserDto userDto, CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellation);
        Task<ResponseService<IEnumerable<UserDto>>> GetAllCommonUsersDisable(CancellationToken cancellation);
        Task<ResponseService<LoginOutUserDto>> Login(LoginUserDto loginUser, CancellationToken cancellationToken);
        Task<ResponseService<UserDto>> GetUserById(int id, CancellationToken cancellationToken);
    }
}
