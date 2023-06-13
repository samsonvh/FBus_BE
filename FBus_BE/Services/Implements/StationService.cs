using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBus_BE.Services.Implements
{
    public class StationService : IStationService
    {
        private readonly Dictionary<string, Expression<Func<Station, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public StationService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Station, object?>>>()
            {
                {"id", station => station.Id}
            };
        }

        public async Task<PageResponse<StationListingDTO>> GetStationsWithPaging(StationPageRequest pageRequest)
        {
            if (pageRequest != null)
            {
                int skippedCount = (pageRequest.PageNumber - 1) * pageRequest.PageSize;
                int stationCount = await _context.Stations
                                       .Where(station => pageRequest.Name != null ? station.Name.Contains(pageRequest.Name) : true)
                                       .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                                       .Where(station => pageRequest.Street != null ? station.Street == pageRequest.Street : true)
                                       .Where(station => pageRequest.City != null ? station.City == pageRequest.City : true)
                                       .CountAsync();

                if (pageRequest.OrderBy == null)
                {
                    pageRequest.OrderBy = "id";
                }
                List<StationListingDTO> stations = pageRequest.Direction != "desc"
                    ? await _context.Stations.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderByDescending(orderDict[pageRequest.OrderBy])
                                       .Where(station => pageRequest.Name != null ? station.Name.Contains(pageRequest.Name) : true)
                                       .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                                       .Where(station => pageRequest.Street != null ? station.Street == pageRequest.Street : true)
                                       .Where(station => pageRequest.City != null ? station.City == pageRequest.City : true)
                                       .Select(station => _mapper.Map<StationListingDTO>(station))
                                       .ToListAsync()
                    : await _context.Stations.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderBy(orderDict[pageRequest.OrderBy])
                                       .Where(station => pageRequest.Name != null ? station.Name.Contains(pageRequest.Name) : true)
                                       .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                                       .Where(station => pageRequest.Street != null ? station.Street == pageRequest.Street : true)
                                       .Where(station => pageRequest.City != null ? station.City == pageRequest.City : true)
                                       .Select(station => _mapper.Map<StationListingDTO>(station))
                                       .ToListAsync();
                return new PageResponse<StationListingDTO>
                {
                    Items = stations,
                    PageIndex = pageRequest.PageNumber,
                    PageCount = (stationCount / pageRequest.PageSize) + 1,
                    PageSize = pageRequest.PageSize
                };
            }
            return null;
        }

        public async Task<StationDTO> GetStationDetails(int id)
        {
            Station? station = await _context.Stations
                .FirstOrDefaultAsync(station => station.Id == id);
            return _mapper.Map<StationDTO>(station);
        }

        public async Task<StationDTO> Create(StationInputDTO stationInputDTO)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Code == stationInputDTO.Code);
            if (station == null)
            {
                station = _mapper.Map<Station>(stationInputDTO);
                _context.Stations.Add(station);
                await _context.SaveChangesAsync();

                return _mapper.Map<StationDTO>(station);
            }
            return null;
        }

        public async Task<StationDTO> Update(int id, StationInputDTO stationInputDTO)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                station = _mapper.Map<Station>(stationInputDTO);
                _context.Stations.Update(station);
                await _context.SaveChangesAsync();

                return _mapper.Map<StationDTO>(station);
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                station.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Deactivate(int id)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                RouteStation? routeStation = await _context.RouteStations.FirstOrDefaultAsync(routeStation => routeStation.StationId == id);
                if(routeStation != null)
                {
                    _context.RouteStations.Remove(routeStation);
                }
                station.Status = "INACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
