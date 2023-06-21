using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Models;
using Google.Apis.Storage.v1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Route = FBus_BE.Models.Route;

namespace FBus_BE.Services.Implements
{
    public class RouteService : IRouteService
    {
        private readonly Dictionary<string, Expression<Func<Route, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;
        private readonly IFirebaseStorageService _storageService;
        private const string cloudStoragePrefix = @"https://firebasestorage.googleapis.com/v0/b/fbus-388009.appspot.com/o/";

        public RouteService(FbusMainContext context, IMapper mapper, IFirebaseStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            orderDict = new Dictionary<string, Expression<Func<Route, object?>>>
            {
                {"id", route => route.Id }
            };
            _storageService = storageService;
        }

        public async Task<DefaultPageResponse<RouteListingDTO>> GetRouteList(RoutePageRequest pageRequest)
        {
            DefaultPageResponse<RouteListingDTO> pageResponse = new DefaultPageResponse<RouteListingDTO>();
            if (pageRequest.PageIndex == null)
            {
                pageRequest.PageIndex = 1;
            }
            if (pageRequest.PageSize == null)
            {
                pageRequest.PageSize = 10;
            }
            if (pageRequest.OrderBy == null)
            {
                pageRequest.OrderBy = "id";
            }
            int skippedCount = (int)((pageRequest.PageIndex - 1) * pageRequest.PageSize);
            int totalCount = await _context.Routes
                .Where(route => pageRequest.Beginning != null ? route.Beginning.Contains(pageRequest.Beginning) : true)
                .Where(route => pageRequest.Destination != null ? route.Destination.Contains(pageRequest.Destination) : true)
                .CountAsync();
            if (totalCount > 0)
            {
                List<RouteListingDTO> routes = pageRequest.Direction == "desc"
                    ? await _context.Routes.Skip(skippedCount)
                                           .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                           .Where(route => pageRequest.Beginning != null ? route.Beginning.Contains(pageRequest.Beginning) : true)
                                           .Where(route => pageRequest.Destination != null ? route.Destination.Contains(pageRequest.Destination) : true)
                                           .Select(route => _mapper.Map<RouteListingDTO>(route))
                                           .ToListAsync()
                    : await _context.Routes.Skip(skippedCount)
                                           .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                           .Where(route => pageRequest.Beginning != null ? route.Beginning.Contains(pageRequest.Beginning) : true)
                                           .Where(route => pageRequest.Destination != null ? route.Destination.Contains(pageRequest.Destination) : true)
                                           .Select(route => _mapper.Map<RouteListingDTO>(route))
                                           .ToListAsync();
                pageResponse.Data = routes;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<RouteDTO> GetRouteDetails(int id)
        {
            Route? route = await _context.Routes
                .Include(route => route.CreatedBy)
                .Include(route => route.RouteStations).ThenInclude(routeStation => routeStation.Station)
                .FirstOrDefaultAsync(route => route.Id == id);
            return _mapper.Map<RouteDTO>(route);
        }

        public async Task<RouteDTO> Create(int createdById, RouteInputDTO routeInputDTO)
        {
            Route? route = _mapper.Map<Route>(routeInputDTO);
            if (route != null)
            {
                route.CreatedById = (short?)createdById;
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();

                foreach (RouteStationInputDTO routeStationInputDTO in routeInputDTO.RouteStations)
                {
                    RouteStation routeStation = new RouteStation();
                    routeStation.RouteId = route.Id;
                    routeStation.StationOrder = routeStationInputDTO.StationOrder;
                    if (routeStationInputDTO.stationInputDTO != null && routeStationInputDTO.StationId == null)
                    {
                        Station? station = _mapper.Map<Station>(routeStationInputDTO.stationInputDTO);
                        if(station != null)
                        {
                            if (routeStationInputDTO.stationInputDTO.ImageFile != null)
                            {
                                Uri uri = await _storageService.UploadFile(routeStationInputDTO.stationInputDTO.Code, routeStationInputDTO. stationInputDTO.ImageFile, "stations");
                                station.Image = cloudStoragePrefix + uri.AbsolutePath.Substring(uri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                            }
                            station.CreatedById = (short?)createdById;
                            _context.Stations.Add(station);
                            await _context.SaveChangesAsync();
                            routeStation.StationId = station.Id;
                        }
                    }
                    else if (routeStationInputDTO.stationInputDTO == null && routeStationInputDTO.StationId != null)
                    {
                        routeStation.StationId = routeStationInputDTO.StationId;
                    }
                    _context.RouteStations.Add(routeStation);
                    await _context.SaveChangesAsync();
                }

                short routeId = route.Id;
                route = await _context.Routes
                    .Include(route => route.CreatedBy)
                    .Include(route => route.RouteStations).ThenInclude(routeStation => routeStation.Station)
                    .FirstOrDefaultAsync(route => route.Id == routeId);
                return _mapper.Map<RouteDTO>(route);
            }
            return null;
        }

        public async Task<RouteDTO> Update(int createdById, RouteInputDTO routeInputDTO, int id)
        {
            Route? route = await _context.Routes.Include(route => route.RouteStations).FirstOrDefaultAsync(route => route.Id == id);
            if (route != null)
            {
                _context.RouteStations.RemoveRange(route.RouteStations);

                route = _mapper.Map(routeInputDTO, route);
                route.CreatedById = (short?)createdById;
                _context.Routes.Update(route);
                await _context.SaveChangesAsync();


                foreach (RouteStationInputDTO routeStationInputDTO in routeInputDTO.RouteStations)
                {
                    RouteStation routeStation = new RouteStation();
                    routeStation.RouteId = route.Id;
                    routeStation.StationOrder = routeStationInputDTO.StationOrder;
                    if (routeStationInputDTO.stationInputDTO != null && routeStationInputDTO.StationId == null)
                    {
                        Station? station = _mapper.Map<Station>(routeStationInputDTO.stationInputDTO);
                        if(station != null)
                        {
                            if (routeStationInputDTO.stationInputDTO.ImageFile != null)
                            {
                                Uri uri = await _storageService.UploadFile(routeStationInputDTO.stationInputDTO.Code, routeStationInputDTO. stationInputDTO.ImageFile, "stations");
                                station.Image = cloudStoragePrefix + uri.AbsolutePath.Substring(uri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                            }
                            station.CreatedById = (short?)createdById;
                            _context.Stations.Add(station);
                            await _context.SaveChangesAsync();
                            routeStation.StationId = station.Id;
                        }
                    }
                    else if (routeStationInputDTO.stationInputDTO == null && routeStationInputDTO.StationId != null)
                    {
                        routeStation.StationId = routeStationInputDTO.StationId;
                    }
                    _context.RouteStations.Add(routeStation);
                    await _context.SaveChangesAsync();
                }

                short routeId = route.Id;
                route = await _context.Routes
                    .Include(route => route.CreatedBy)
                    .Include(route => route.RouteStations).ThenInclude(routeStation => routeStation.Station)
                    .FirstOrDefaultAsync(route => route.Id == routeId);
                return _mapper.Map<RouteDTO>(route);
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            Route? route = await _context.Routes.FirstOrDefaultAsync(route => route.Id == id);
            if(route != null)
            {
                route.Status = status;
                _context.Routes.Update(route);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Deactivate(int id)
        {
            Route? route = await _context.Routes.FirstOrDefaultAsync(route => route.Id == id);
            if(route != null)
            {
                route.Status = "INACTIVE";
                _context.Routes.Update(route);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
