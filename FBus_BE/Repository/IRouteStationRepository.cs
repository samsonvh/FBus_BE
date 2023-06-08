using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Repository
{
    public interface IRouteStationRepository
    {
        // Task<RouteStation> findByRoute(short routeId);
        Task<IEnumerable<RouteStation>> FindByRouteId(short routeId);

    }
}