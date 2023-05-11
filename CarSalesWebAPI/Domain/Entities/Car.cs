using System.Collections.ObjectModel;

namespace CarSalesWebAPI.Domain.Entities
{
    public class Car : Entity
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int YearOfManufacture { get; set; } 
        public int Stock { get; set; }
        public double Average { get; set; }
        public IEnumerable<AssessmentRecord> Assessments { get; set; }

        public Car()
        {
            Assessments = new Collection<AssessmentRecord>();
        }
    }
}
