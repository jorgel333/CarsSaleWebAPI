using CarSalesWebAPI.Services.Interfaces.Services;
using System.Net;

namespace CarSalesWebAPI.Services.Services
{
    public class Service : IService
    {
        public ResponseService<T> GenerateErroResponse<T>(string message, HttpStatusCode status = HttpStatusCode.BadRequest) => new()
        {
            Status = status,
            Message = message,
            Success = false,
            Value = default
        };

        public ResponseService GenerateErrorResponse(string message, HttpStatusCode status = HttpStatusCode.BadRequest) => new()
        {
            Status = status,
            Message = message,
            Success = false
        };

        public ResponseService GenerateSuccessfullResponse(HttpStatusCode status = HttpStatusCode.OK) => new()
        {
            Status = status,
            Message = string.Empty, 
            Success = true
        };


        public ResponseService<T> GenerateSuccessfullResponse<T>(T value, HttpStatusCode status = HttpStatusCode.OK) => new()
        {
            Status = status,
            Message = string.Empty,
            Success = true,
            Value = value
        };
    }
}
