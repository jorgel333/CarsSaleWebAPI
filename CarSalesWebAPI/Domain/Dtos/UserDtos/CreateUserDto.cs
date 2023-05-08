namespace CarSalesWebAPI.Domain.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
