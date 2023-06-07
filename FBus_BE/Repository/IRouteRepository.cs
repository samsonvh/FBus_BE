using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Repository
{
    public interface IRouteRepository
    {
        Task<IEnumerable<FBus_BE.Models.Route>> GetRouteList();
        Task<Models.Route> Create(Models.Route route);
        Task<Models.Route> GetRouteById(short id);
        Task<bool> Active(short id);
        Task<bool> Deactive(short id);
    }
}