using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IStationService
    {
        Task<DefaultPageResponse<StationListingDTO>> GetStationList(StationPageRequest pageRequest);
        Task<StationDTO> GetStationDetails(int id);
        Task<StationDTO> Create(int createdById, StationInputDTO stationInputDTO);
        Task<StationDTO> Update(int createdById, StationInputDTO stationInputDTO, int id);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Deactivate(int id);
    }
}
