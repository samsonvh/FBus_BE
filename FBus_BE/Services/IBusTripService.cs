using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;

namespace FBus_BE.Services
{
    public interface IBusTripService
    {
        Task<DefaultPageResponse<BusTripListingDTO>> GetBusTripList(BusTripPageRequest pageRequest);
        Task<BusTripDTO> GetBusTripDetails(int? busTripId);
        Task<BusTripDTO> Create(BusTripInputDTO busTripInputDTO);
        Task<BusTripDTO> Update(int id, BusTripInputDTO busTripInputDTO);
        Task<bool> ChangeStatus(int id);
        Task<bool> Deactivate(int id);

    }
}