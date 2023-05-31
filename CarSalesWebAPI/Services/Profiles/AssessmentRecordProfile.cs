using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.AssessmentRecordDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Profiles
{
    public class AssessmentRecordProfile : Profile
    {
        public AssessmentRecordProfile()
        {
            CreateMap<AssessmentRecord,  RegisterEvaluationDTO>().ReverseMap()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => 
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
        }
    }
}
