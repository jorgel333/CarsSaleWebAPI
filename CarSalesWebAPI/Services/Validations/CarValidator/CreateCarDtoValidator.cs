using CarSalesWebAPI.Domain.Dtos.CarDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CarSalesWebAPI.Services.Validations.CarValidator
{
    public class CreateCarDtoValidator : AbstractValidator<CreateCarDto>
    {
        public CreateCarDtoValidator()
        {

            RuleFor(x => x.Model).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .Length(2, 25).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");

            RuleFor(x => x.Brand).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .Length(2, 30).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");

            RuleFor(x => x.Color).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .Length(2, 30).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");
            
            RuleFor(x => x.Type).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .Length(2, 30).WithMessage("O {PropertyName} não pode ter menos de {MinLength} ou mais que {MaxLength} caracteres");

            RuleFor(x => x.Price).NotEmpty();

            RuleFor(x => x.YearOfManufacture).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .GreaterThanOrEqualTo(2000).WithMessage("O ano de fabricação não pode ser menor que 2000")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano de fabricação não pode ser maior que o ano atual");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("A {PropertyName} não pode ser vazia.")
                .GreaterThan(0).WithMessage("O {PropertyName} não pode ser menor que 0");
        }
    }
}
