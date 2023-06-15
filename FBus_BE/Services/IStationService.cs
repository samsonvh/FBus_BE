using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;

namespace FBus_BE.Services
{
    public interface IStationService
    {
        Task<PageResponse<StationListingDTO>> GetStationsWithPaging(StationPageRequest pageRequest);
        Task<StationDTO> GetStationDetails(int id);
        Task<StationDTO> Create(StationInputDTO stationInputDTO);
        Task<StationDTO> Update(int id, StationInputDTO stationInputDTO);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Deactivate(int id);
    }
}
