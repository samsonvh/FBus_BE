using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;

namespace FBus_BE.Services
{
    public interface IBusTripService
    {
        Task<BusTripDTO> GetBusById(int? busTripId);
        Task<BusTripDTO> Create(BusTripInputDTO busTripInputDTO);
        Task<BusTripDTO> Update(int id, BusTripInputDTO busTripInputDTO);
        Task<bool> Deactivate(int id);
        Task<bool> Activate(int id);
        Task<List<BusTripDTO>> GetBusByCoordinationId(int? coordinationId);
    }
}