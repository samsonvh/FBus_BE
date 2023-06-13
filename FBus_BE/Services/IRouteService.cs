using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;

namespace FBus_BE.Services
{
    public interface IRouteService
    {
        Task<PageResponse<RouteListingDTO>> GetRoutesWithPaging(RoutePageRequest pageRequest);
        Task<RouteDTO> GetRouteDetails(int? routeId);
        Task<RouteDTO> Create(RouteInputDTO routeInputDTO);
        Task<RouteDTO> Update(int id, RouteInputDTO routeInputDTO);
        Task<bool> ChangeStatus(int routeId, string status);
        Task<bool> Deactivate(int routeId);
    }
}
