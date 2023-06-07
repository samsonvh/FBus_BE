using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.Models;
using System.Collections.Generic;

namespace FBus_BE.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() { 
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
            CreateMap<Driver, DriverDTO>();
            CreateMap<Driver, DriverListingDTO>();
            CreateMap<DriverInputDTO, Driver>()
                .ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.AccountId, option => option.Ignore())
                .ForMember(dest => dest.Account, option => option.Ignore())
                .ForMember(dest => dest.CreatedDate, option => option.Ignore());
        }
    }
}
