using CarSalesWebAPI.Data;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;
using CarSalesWebAPI.Services.SecurityServices.CryptographyService;
using CarSalesWebAPI.Services.SecurityServices.TokenService;
using CarSalesWebAPI.Services.Services;

namespace CarSalesWebAPI.Extensions
{
    public static class DependencyInjecttionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAssessmentRecordService, AssessmentRecordService>();
            services.AddScoped<ICryptography, Cryptography>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
