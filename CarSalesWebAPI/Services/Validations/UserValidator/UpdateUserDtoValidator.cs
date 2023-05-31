using CarSalesWebAPI.Domain.Dtos.UserDtos;
using FluentValidation;

namespace CarSalesWebAPI.Services.Validations.UserValidator
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} inválido")
               .Length(10, 100).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");

            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Formato de {PropertyName} inválido")
                .MaximumLength(130).WithMessage("O {PropertyName} não pode ter mais de {PropertyValue} caracteres");
            
            RuleFor(x => x.Birthday).Must(UserValidatorRules.BeOver18).WithMessage("Usuário menor de 18 anos");
        }
    }
}
