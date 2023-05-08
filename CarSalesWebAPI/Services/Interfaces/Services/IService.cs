using System.Net;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface IService 
    {
        ResponseService GenerateErrorResponse(string message, HttpStatusCode status = HttpStatusCode.BadRequest);
        ResponseService<T> GenerateErroResponse<T>(string message, HttpStatusCode status = HttpStatusCode.BadRequest);
        ResponseService GenerateSuccessfullResponse(HttpStatusCode status = HttpStatusCode.OK);
        ResponseService<T> GenerateSuccessfullResponse<T>(T value, HttpStatusCode status = HttpStatusCode.OK);
    }
}
