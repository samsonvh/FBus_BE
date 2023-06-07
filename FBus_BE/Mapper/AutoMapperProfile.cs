using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.Dto;
using FBus_BE.Models;

namespace FBus_BE.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Driver, DriverDto>();
            CreateMap<DriverDto, Driver>();

            CreateMap<Models.Route, RouteDto>();
            CreateMap<RouteDto, Models.Route>();
        }
    }
}