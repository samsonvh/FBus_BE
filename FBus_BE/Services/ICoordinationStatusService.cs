using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;

namespace FBus_BE.Services
{
    public interface ICoordinationStatusService
    {
        Task<CoordinationStatusDTO> GetCoordinationById(int? coordinationStatusId);
        Task<CoordinationStatusDTO> Create(CoordinationStatusDTO coordinationStatusInputDTO);
        Task<CoordinationStatusDTO> Update(int id, CoordinationStatusDTO coordinationStatusInputDTO);
        Task<bool> Deactivate(int id);
        Task<bool> Activate(int id);
    }
}