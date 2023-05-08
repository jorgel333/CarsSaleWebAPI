using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task<ResponseService> CreateCommonUser(CreateUserDto user, CancellationToken cancellationToken);
        Task<ResponseService> CreateAdm(CreateUserDto user, CancellationToken cancellationToken);
        Task<ResponseService> DeleteUser(int id, CancellationToken cancellationToken);
        Task<ResponseService> UpdateUser(int id, UpdateUserDto user, CancellationToken cancellationToken);
        Task<ResponseService<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellation);
        Task<ResponseService<IEnumerable<UserDto>>> GetAllCommonUsersDisable(CancellationToken cancellation);
    }
}
