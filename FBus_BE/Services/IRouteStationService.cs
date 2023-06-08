using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Services
{
    public interface IRouteStationService
    {
        Task<IEnumerable<RouteStation>> GetByRouteId(short routeId);
    }
}