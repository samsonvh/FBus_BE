using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface ICoordinationService
    {
        Task<DefaultPageResponse<CoordinationListingDTO>> GetCoordinationList(CoordinationPageRequest pageRequest);
        Task<CoordinationDTO> Create(int createdById, CoordinationInputDTO coordinationInputDTO);
        Task<CoordinationDTO> Update(int createdById, CoordinationInputDTO coordinationInputDTO, int id);
    }
}
