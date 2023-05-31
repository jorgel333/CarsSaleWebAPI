using System.Collections.Specialized;

namespace CarSalesWebAPI.Services.Helpers
{
    public class MessageResultValidator
    {
        public string Property { get; set; }
        public string Description { get; set; }

        public MessageResultValidator(string property, string description)
        {
            Property = property;
            Description = description;
        }
    }
}
