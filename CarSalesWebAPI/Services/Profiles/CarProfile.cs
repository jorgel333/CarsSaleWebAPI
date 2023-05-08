using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.CarDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDetailsDto>().ReverseMap();
            CreateMap<Car, CarsAllDto>().ReverseMap();
        }
    }
}
