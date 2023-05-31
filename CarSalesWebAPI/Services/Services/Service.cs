using CarSalesWebAPI.Services.Helpers;
using CarSalesWebAPI.Services.Interfaces.Services;
using System.Net;

namespace CarSalesWebAPI.Services.Services
{
    public abstract class Service : AbstractService, IService
    {
        public ResponseService GenerateErroResponse(string message, HttpStatusCode status) => new()
        {
            Status = status,
            Message = message,
            Success = false
        };

        public ResponseService GenerateSuccessResponse(HttpStatusCode status) => new()
        {
            Status = status,
            Message = string.Empty,
            Success = true
        };


        public ResponseService<T> GenerateSuccessResponse<T>(T value, HttpStatusCode status) => new()
        {
            Status = status,
            Message = string.Empty,
            Success = true,
            Value = value
        };
        public ResponseService<T> GenerateErroResponse<T>(string message, HttpStatusCode status) => new()
        {
            Status = status,
            Message = message,
            Success = false,
            Value = default
        };
        public ResponseService<T> GenerateErroValidationResponse<T>(T value, HttpStatusCode status) => new()
        {
            Status = status,
            Message = string.Empty,
            Success = false,
            Value = value
        };
    }
}
