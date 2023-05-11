using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Interfaces.Services
{
    public interface IAssessmentRecordService : IService
    {
        Task<ResponseService> RegisterEvaluation(RegisterEvaluationDTO assessmentRcd, CancellationToken cancellationToken);
    }
}
