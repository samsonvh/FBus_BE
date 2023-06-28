using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.Models;
using Route = FBus_BE.Models.Route;

namespace FBus_BE
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            // Account
            CreateMap<Account, AccountDTO>();

            // Driver
            CreateMap<Driver, DriverListingDTO>()
                .ForMember(driver => driver.Code, option => option.MapFrom(driver => driver.Account.Code))
                .ForMember(driver => driver.Email, option => option.MapFrom(driver => driver.Account.Email));
            CreateMap<Driver, DriverDTO>()
                .ForMember(driver => driver.Code, option => option.MapFrom(driver => driver.Account.Code))
                .ForMember(driver => driver.Email, option => option.MapFrom(driver => driver.Account.Email))
                .ForMember(driver => driver.CreatedByCode, option => option.MapFrom(driver => driver.CreatedBy.Code));
            CreateMap<DriverInputDTO, Driver>()
                .ForMember(driver => driver.Id, option => option.Ignore())
                .ForMember(driver => driver.AccountId, option => option.Ignore())
                .ForMember(driver => driver.CreatedById, option => option.Ignore())
                .ForMember(driver => driver.CreatedDate, option => option.Ignore())
                .ForMember(driver => driver.Status, option => option.Ignore());

            // Bus
            CreateMap<Bus, BusListingDTO>();
            CreateMap<Bus, BusDTO>()
                .ForMember(bus => bus.CreatedByCode, option => option.MapFrom(bus => bus.CreatedBy.Code));
            CreateMap<BusInputDTO, Bus>();

            // Station
            CreateMap<Station, StationListingDTO>();
            CreateMap<Station, StationDTO>()
                .ForMember(station => station.CreatedByCode, option => option.MapFrom(station => station.CreatedBy.Code));
            CreateMap<StationInputDTO, Station>();

            // Route
            CreateMap<Route, RouteListingDTO>();
            CreateMap<RouteInputDTO, Route>()
                .ForMember(route => route.RouteStations, option => option.Ignore());
            CreateMap<Route, RouteDTO>()
                .ForMember(route => route.CreatedByCode, option => option.MapFrom(route => route.CreatedBy.Code));

            // RouteStation
            CreateMap<RouteStation, RouteStationDTO>()
                .ForMember(routeStation => routeStation.Station, option => option.MapFrom(routeStation => routeStation.Station));

            // Coordination
            CreateMap<Coordination, CoordinationDTO>();
            CreateMap<Coordination, CoordinationListingDTO>()
                .ForMember(coordination => coordination.BusCode, option => option.MapFrom(coordination => coordination.Bus.Code))
                .ForMember(coordination => coordination.Beginning, option => option.MapFrom(coordination => coordination.Route.Beginning))
                .ForMember(coordination => coordination.Destination, option => option.MapFrom(coordination => coordination.Route.Destination))
                .ForMember(coordination => coordination.DriverCode, option => option.MapFrom(coordination => coordination.Driver.Account.Code))
                .ForMember(coordination => coordination.CreatedByCode, option => option.MapFrom(coordination => coordination.CreatedBy.Code));
        }
    }
}
