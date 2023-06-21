using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IRouteService
    {
        Task<DefaultPageResponse<RouteListingDTO>> GetRouteList(RoutePageRequest pageRequest);
        Task<RouteDTO> GetRouteDetails(int id);
        Task<RouteDTO> Create(int createdById, RouteInputDTO routeInputDTO);
        Task<RouteDTO> Update(int createdById, RouteInputDTO routeInputDTO, int id);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Deactivate(int id);
    }
}
