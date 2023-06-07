using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public Task<bool> Active(short id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Route> Create(Models.Route route)
        {
            return await _routeRepository.Create(route);
        }

        public Task<bool> Deactive(short id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Route> GetRouteById(short id)
        {
            return _routeRepository.GetRouteById(id);
        }

        public async Task<IEnumerable<Models.Route>> GetRouteList()
        {
            return await _routeRepository.GetRouteList();
        }
    }
}