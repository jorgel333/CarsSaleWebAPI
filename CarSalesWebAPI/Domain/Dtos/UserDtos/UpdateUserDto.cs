namespace CarSalesWebAPI.Domain.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
