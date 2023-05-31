namespace CarSalesWebAPI.Domain.Dtos.CarDtos
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int YearOfManufacture { get; set; }
        public int Stock { get; set; }
    }
}
