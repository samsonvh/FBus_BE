using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IBusService
    {
        Task<DefaultPageResponse<BusListingDTO>> GetBusList(BusPageRequest pageRequest);
        Task<BusDTO> GetBusDetails(int id);
        Task<BusDTO> Create(int createById, BusInputDTO busInputDTO);
        Task<BusDTO> Update(int createById, BusInputDTO busInputDTO, int id);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Deactivate(int id);
    }
}
