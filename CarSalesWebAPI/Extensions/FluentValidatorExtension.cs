using CarSalesWebAPI.Services.Validations.AssessmentRecordValidator;
using CarSalesWebAPI.Services.Validations.CarValidator;
using CarSalesWebAPI.Services.Validations.UserValidator;
using FluentValidation;

namespace CarSalesWebAPI.Extensions
{
    public static class FluentValidatorExtension
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(CreateUserAdminDtoValidator));;
            return services;
        }
    }
}
