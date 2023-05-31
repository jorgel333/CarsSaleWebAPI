namespace CarSalesWebAPI.Domain.Entities
{
    public class AssessmentRecord : Entity
    {
        public int Note { get; set; }
        public DateTime Date { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
    }
}
