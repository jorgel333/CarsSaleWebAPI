using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;
using System.Net;

namespace CarSalesWebAPI.Services.Services
{
    public class AssessmentRecordService : Service, IAssessmentRecordService
    {
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _uow;

        public AssessmentRecordService(IUnityOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ResponseService> RegisterEvaluation(RegisterEvaluationDTO assessmentDto, CancellationToken cancellationToken)
        {
            var assessmentContains = await _uow.AssessmentRecordRepository.GetById(ass => ass.UserId == assessmentDto.UserId
                             && ass.CarId == assessmentDto.CarId, cancellationToken);
            var car = await _uow.CarRepository.GetByIdToAssessments(assessmentDto.CarId, cancellationToken);
            var user = await _uow.UserRepository.GetById(user => user.Id == assessmentDto.UserId, cancellationToken);
            
            if (assessmentContains != null)
            {
                return GenerateErrorResponse("Avaliação já existe");
            }
            if (car is null)
            {
                return GenerateErrorResponse("Carro não encontrado");
            }
            if (user is null)
            {
                return GenerateErrorResponse("Usuário não encontrado");
            }

            var assessment = _mapper.Map<AssessmentRecord>(assessmentDto);
            _uow.AssessmentRecordRepository.Add(assessment);
            _uow.CarRepository.UpdateAverage(car);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse();
        }
    }
}
