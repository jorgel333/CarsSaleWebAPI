namespace CarSalesWebAPI.Domain.Dtos.CarDtos
{
    public class CarsAllDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int YearOfManufacture { get; set; }
        public double Average { get; set; }
    }
}
