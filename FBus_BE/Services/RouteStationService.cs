using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class RouteStationService : IRouteStationService
    {
        private readonly IRouteStationRepository _routeStationRepository;
        public RouteStationService(IRouteStationRepository routeStationRepository)
        {
            _routeStationRepository = routeStationRepository;
        }
        public async Task<IEnumerable<RouteStation>> GetByRouteId(short routeId)
        {
            return await _routeStationRepository.FindByRouteId(routeId);
        }
    }
}