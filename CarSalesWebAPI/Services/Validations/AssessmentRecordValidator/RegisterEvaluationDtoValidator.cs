using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using FluentValidation;

namespace CarSalesWebAPI.Services.Validations.AssessmentRecordValidator
{
    public class RegisterEvaluationDtoValidator : AbstractValidator<RegisterEvaluationDTO>
    {
        public RegisterEvaluationDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage("Id precisa ser maior que 0");
            
            RuleFor(x => x.CarId).NotEmpty().GreaterThan(0).WithMessage("Id precisa ser maior que 0");

            RuleFor(x => x.Note).NotEmpty().InclusiveBetween(0, 5).WithMessage("A avaliação tem que ser entre 0 e 5");
        }
    }
}
