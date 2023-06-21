using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IDriverService
    {
        Task<DefaultPageResponse<DriverListingDTO>> GetDriverList(DriverPageRequest pageRequest);
        Task<DriverDTO> GetDriverDetails(int id);
        Task<DriverDTO> Create(DriverInputDTO driverInputDTO, int? createdById);
        Task<DriverDTO> Update(int id, DriverInputDTO driverInputDTO, int? createdById);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Deactivate(int id);
    }
}
