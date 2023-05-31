using CarSalesWebAPI.Domain.Dtos.UserDtos;
using FluentValidation;

namespace CarSalesWebAPI.Services.Validations.UserValidator
{
    public class CreateCommonUserDtoValidator : AbstractValidator<CreateCommonUserDto>
    {
        public CreateCommonUserDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} inválido")
                .Length(10, 100).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");

            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Formato de {PropertyName} inválido")
                .MaximumLength(130).WithMessage("O {PropertyName} não pode ter mais de 130 caracteres");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Matches(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9])$").WithMessage("A senha deve conter pelo menos 1 número, 1 letra e 1 caractere especial.")
                .Length(5, 15).WithMessage("A senha deve conter de {MinLength} a {MaxLength} caracteres."); ;

            RuleFor(p => p.ComfirmPassword).Equal(p => p.Password);

            RuleFor(x => x.Birthday).Must(UserValidatorRules.BeOver18).WithMessage("Usuário menor de 18 anos");
        }
    }
}
