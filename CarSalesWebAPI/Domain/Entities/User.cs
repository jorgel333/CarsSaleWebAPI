using System.Collections.ObjectModel;

namespace CarSalesWebAPI.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<AssessmentRecord> Assessment { get; set; }

        public User()
        {
            Assessment = new Collection<AssessmentRecord>();
        }
    }
}
