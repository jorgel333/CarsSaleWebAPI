using CarSalesWebAPI.Services.Helpers;
using FluentValidation.Results;
using FluentValidation;

namespace CarSalesWebAPI.Services.Services
{
    public abstract class AbstractService 
    {
        private readonly List<MessageResultValidator> _message;

        public AbstractService()
        {
            _message = new List<MessageResultValidator>();
        }
        public void GetErrors(ValidationResult result)
        {
            foreach(var erros in result.Errors)
            {
                _message.Add(new MessageResultValidator(erros.PropertyName, erros.ErrorMessage));
            }
        }

        public List<MessageResultValidator> Notify()
        {
            return _message;
        }

        public bool PerformValidation<V, E>(V validation, E entity) where V : AbstractValidator<E>
        {
            var result = validation.Validate(entity);

            if (result.IsValid)
            {
                return true;
            }
            GetErrors(result);
            return false;
        }
    }
}
