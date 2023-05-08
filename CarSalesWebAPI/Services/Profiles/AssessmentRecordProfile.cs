using AutoMapper;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Profiles
{
    public class AssessmentRecordProfile : Profile
    {
        public AssessmentRecordProfile()
        {
            CreateMap<AssessmentRecord,  AssessmentRecordProfile>().ReverseMap();
        }
    }
}
