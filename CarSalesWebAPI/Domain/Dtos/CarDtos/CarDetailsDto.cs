using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;

namespace CarSalesWebAPI.Domain.Dtos.CarDtos
{
    public class CarDetailsDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int YearOfManufacture { get; set; }
        public int Stock { get; set; }
        public double Average { get; set; }
    }
}
