using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Data;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Repository
{
    public class RouteStationRepository : IRouteStationRepository
    {
        private readonly FbusMainContext _context;
        public RouteStationRepository(FbusMainContext context)
        {
            _context = context;
        }
        public async Task<RouteStation> findByRoute(short routeId)
        {
            return await _context.RouteStations.FirstOrDefaultAsync(a => a.RouteId == routeId);
        }

        public async Task<IEnumerable<RouteStation>> FindByRouteId(short routeId)
        {
            return await _context.RouteStations
            .Include(rs => rs.Route)
            .Include(rs => rs.Station)
            .Where(a => a.RouteId == routeId).ToListAsync();
        }
    }
}