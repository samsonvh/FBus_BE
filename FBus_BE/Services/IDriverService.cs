using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
namespace FBus_BE.Services
{
    public interface IDriverService
    {
        Task<PageResponse<DriverListingDTO>> GetDriversWithPaging(DriverPageRequest pageRequest);
        Task<DriverDTO> GetDriverDetails(int id);
        Task<DriverDTO> Create(DriverInputDTO driverInputDTO);
        Task<DriverDTO> Update(int id, DriverInputDTO driverInputDTO);
        Task<bool> ChangeStatus(int id, string status);
        Task<bool> Delete(int id);
    }
}
