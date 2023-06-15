using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Models;
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

        public RouteService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Route, object?>>>()
            {
                {"id", route => route.Id}
            };
        }

        public async Task<PageResponse<RouteListingDTO>> GetRoutesWithPaging(RoutePageRequest pageRequest)
        {
            if (pageRequest != null)
            {
                int skippedCount = (pageRequest.PageNumber - 1) * pageRequest.PageSize;
                int totalCount = await _context.Routes.CountAsync();

                if (pageRequest.OrderBy == null)
                {
                    pageRequest.OrderBy = "id";
                }

                List<RouteListingDTO> routes = pageRequest.Direction == "desc"
                    ? await _context.Routes.Skip(skippedCount).Take(pageRequest.PageSize)
                                    .OrderByDescending(orderDict[pageRequest.OrderBy])
                                    .Select(route => _mapper.Map<RouteListingDTO>(route))
                                    .ToListAsync()
                    : await _context.Routes.Skip(skippedCount)
                                    .Take(pageRequest.PageSize)
                                    .OrderBy(orderDict[pageRequest.OrderBy])
                                    .Select(route => _mapper.Map<RouteListingDTO>(route))
                                    .ToListAsync();
                return new PageResponse<RouteListingDTO>
                {
                    Items = routes,
                    PageIndex = pageRequest.PageNumber,
                    PageCount = (pageRequest.PageSize / totalCount) + 1,
                    PageSize = pageRequest.PageSize
                };
            }
            return null;
        }

        public async Task<RouteDTO> GetRouteDetails(int? routeId)
        {
            if (routeId != null)
            {
                Route? route = await _context.Routes
                    .Include(r => r.RouteStations)
                    .FirstOrDefaultAsync(r => r.Id == routeId);
                return _mapper.Map<RouteDTO>(route);
            }
            return null;
        }
        
        public async Task<RouteDTO> Create(RouteInputDTO routeInputDTO)
        {
            if (routeInputDTO != null)
            {
                Route route = _mapper.Map<Route>(routeInputDTO);
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();

                foreach (RouteStationInputDTO routeStationInput in routeInputDTO.routeStationInputs)
                {
                    short? stationId = null;
                    if (routeStationInput.StationId != null && routeStationInput.StationInputDTO == null)
                    {
                        stationId = routeStationInput.StationId;
                    }
                    else if (routeStationInput.StationId == null && routeStationInput.StationInputDTO != null)
                    {
                        Station newStation = _mapper.Map<Station>(routeStationInput.StationInputDTO);
                        _context.Stations.Add(newStation);
                        await _context.SaveChangesAsync();
                        stationId = newStation.Id;
                    }

                    if (stationId != null)
                    {
                        RouteStation routeStation = new RouteStation
                        {
                            RouteId = route.Id,
                            StationId = stationId,
                            StationOrder = routeStationInput.StationOrder
                        };
                        _context.RouteStations.Add(routeStation);
                    }
                }
                await _context.SaveChangesAsync();

                //foreach (RouteStationInputDTO routeStationInputDTO in routeInputDTO.routeStationInputs)
                //{
                //    RouteStation routeStation;
                //    if (routeStationInputDTO.StationId == null && routeStationInputDTO.StationInputDTO != null)
                //    {
                //        Station? station = await _context.Stations
                //            .FirstOrDefaultAsync(station => station.Code == routeStationInputDTO.StationInputDTO.Code);
                //        if (station == null)
                //        {
                //            station = _mapper.Map<Station>(routeStationInputDTO.StationInputDTO);
                //            _context.Stations.Add(station);
                //        }
                //        routeStation = new RouteStation()
                //        {
                //            RouteId = route.Id,
                //            StationId = station.Id,
                //            StationOrder = routeStationInputDTO.StationOrder
                //        };
                //        _context.RouteStations.Add(routeStation);
                //    }
                //    else if (routeStationInputDTO.StationId != null && routeStationInputDTO.StationInputDTO == null)
                //    {
                //        routeStation = new RouteStation()
                //        {
                //            RouteId = route.Id,
                //            StationId = routeStationInputDTO.StationId,
                //            StationOrder = routeStationInputDTO.StationOrder
                //        };
                //        _context.RouteStations.Add(routeStation);
                //    }
                //}
                //await _context.SaveChangesAsync();
                return _mapper.Map<RouteDTO>(route);
            }
            return null;
        }

        public async Task<RouteDTO> Update(int id, RouteInputDTO routeInputDTO)
        {
            if (routeInputDTO != null)
            {
                Route? route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == id);
                if (route != null)
                {
                    List<RouteStation> routeStations = await _context.RouteStations.Where(rs => rs.RouteId == route.Id).ToListAsync();
                    _context.RouteStations.RemoveRange(routeStations);

                    foreach (RouteStationInputDTO routeStationInput in routeInputDTO.routeStationInputs)
                    {
                        short? stationId = null;
                        if (routeStationInput.StationId != null && routeStationInput.StationInputDTO == null)
                        {
                            stationId = routeStationInput.StationId;
                        }
                        else if (routeStationInput.StationId == null && routeStationInput.StationInputDTO != null)
                        {
                            Station newStation = _mapper.Map<Station>(routeStationInput.StationInputDTO);
                            _context.Stations.Add(newStation);
                            await _context.SaveChangesAsync();
                            stationId = newStation.Id;
                        }

                        if (stationId != null)
                        {
                            RouteStation routeStation = new RouteStation
                            {
                                RouteId = route.Id,
                                StationId = stationId,
                                StationOrder = routeStationInput.StationOrder
                            };
                            _context.RouteStations.Add(routeStation);
                        }
                    }

                    route = _mapper.Map<Route>(routeInputDTO);
                    _context.Routes.Update(route);
                    await _context.SaveChangesAsync();
                }
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int routeId, string status)
        {
            Route? route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == routeId);
            if (route != null)
            {
                route.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Deactivate(int routeId)
        {
            Route? route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == routeId);
            if(route != null)
            {
                route.Status = "Inactive";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
