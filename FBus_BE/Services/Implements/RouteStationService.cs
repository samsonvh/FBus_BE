using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBus_BE.Services.Implements
{
    public class RouteStationService : IRouteStationService
    {
        private readonly Dictionary<string, Expression<Func<RouteStation, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public RouteStationService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<RouteStation, object?>>>
            {
                {"id", rs => rs.Id}
            };
        }

        public async Task<List<RouteStationDTO>> GetRouteStationList(int? routeId)
        {
            if(routeId != null)
            {
                List<RouteStationDTO> routes = await _context.RouteStations
                    .Where(rs => rs.RouteId == routeId)
                    .Select(rs => _mapper.Map<RouteStationDTO>(rs))
                    .ToListAsync();
                return routes;
            }
            return null;
        }
    }
}
