using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Domain.Dtos.UserDtos
{
    public class LoginOutUserDto
    {
        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
