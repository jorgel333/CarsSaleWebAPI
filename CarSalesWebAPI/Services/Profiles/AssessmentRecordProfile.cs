using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Profiles
{
    public class AssessmentRecordProfile : Profile
    {
        public AssessmentRecordProfile()
        {
            CreateMap<AssessmentRecord,  RegisterEvaluationDTO>().ReverseMap();
            CreateMap<AssessmentRecord, AssessmentCarDetailsDto>().ReverseMap();
        }
    }
}
