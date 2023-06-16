using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;

namespace FBus_BE.Services
{
    public interface ICoordinationService
    {
        Task<CoordinationDTO> GetCoordinationById(int? coordinationId);
        Task<CoordinationDTO> Create(CoordinationInputDTO coordinationInputDTO);
        Task<CoordinationDTO> Update(int id, CoordinationInputDTO coordinationInputDTO);
        Task<bool> Deactivate(int id);
        Task<bool> Activate(int id);
    }
}