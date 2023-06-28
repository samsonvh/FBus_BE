using FBus_BE.DTOs;

namespace FBus_BE.Services
{
    public interface IRouteStationService
    {
        Task<List<RouteStationDTO>> GetRouteStationList(int? routeId);
    }
}
