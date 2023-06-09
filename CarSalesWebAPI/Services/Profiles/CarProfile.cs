﻿using AutoMapper;
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
            CreateMap<UpdateCarDto, Car>().ForMember(dest => dest.Average, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
            CreateMap<CreateCarDto, Car>().ForMember(dest => dest.IsDeleted,
               opt => opt.MapFrom(src => false));
        }
    }
}
