using System.Net;
using CarSalesWebAPI.Services.Helpers;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface IService 
    {
        ResponseService GenerateErroResponse(string message, HttpStatusCode status);
        ResponseService GenerateSuccessResponse(HttpStatusCode status);
        ResponseService<T> GenerateErroResponse<T>(string message, HttpStatusCode status);
        ResponseService<T> GenerateSuccessResponse<T>(T value, HttpStatusCode status);
        ResponseService<T> GenerateErroValidationResponse<T>(T value, HttpStatusCode status);
    }
}
