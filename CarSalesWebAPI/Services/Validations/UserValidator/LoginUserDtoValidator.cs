using CarSalesWebAPI.Domain.Dtos.UserDtos;
using FluentValidation;

namespace CarSalesWebAPI.Services.Validations.UserValidator
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Formato de {PropertyName} inválido")
                .MaximumLength(130).WithMessage("O {PropertyName} não pode ter mais de {PropertyValue} caracteres");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(5, 15).WithMessage("A senha deve conter de {MinLength} a {MaxLength} caracteres.");
        }
    }
}
