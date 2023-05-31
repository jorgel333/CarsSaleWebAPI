using CarSalesWebAPI.Domain.Dtos.UserDtos;
using System.Collections.Specialized;

namespace CarSalesWebAPI.Services.SecurityServices.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserTokenDto userInfo);
    }
}
