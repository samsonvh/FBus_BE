using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.Models;
using Route = FBus_BE.Models.Route;

namespace FBus_BE.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Account
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            // Driver
            CreateMap<Driver, DriverDTO>();
            CreateMap<Driver, DriverListingDTO>();
            CreateMap<DriverInputDTO, Driver>()
                .ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.AccountId, option => option.Ignore())
                .ForMember(dest => dest.Account, option => option.Ignore())
                .ForMember(dest => dest.CreatedDate, option => option.Ignore());

            // Station
            CreateMap<Station, StationListingDTO>();
            CreateMap<Station, StationDTO>();
            CreateMap<StationInputDTO, Station>()
                .ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.CreatedById, option => option.Ignore())
                .ForMember(dest => dest.CreatedBy, option => option.Ignore())
                .ForMember(dest => dest.CreatedDate, option => option.Ignore());

            // Route
            CreateMap<Route, RouteListingDTO>();
            CreateMap<Route, RouteDTO>();
            CreateMap<RouteInputDTO, Route>();

            // RouteStation
            CreateMap<RouteStation, RouteStationDTO>();

            //Coordination
            CreateMap<Coordination, CoordinationDTO>();
            CreateMap<CoordinationInputDTO, Coordination>();

            //Coordinationstatus
            CreateMap<CoordinationStatus, CoordinationStatusDTO>();
            CreateMap<CoordinationStatusInputDTO, CoordinationStatus>();

            //BusTrip
            CreateMap<BusTrip, BusTripDTO>();
            CreateMap<BusTripInputDTO, BusTrip>();
        }
    }
}
