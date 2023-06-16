using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.Models;

namespace FBus_BE.Services
{
    public interface IBusService
    {
        Task<BusDTO> GetBusById(int? busId);
        Task<BusDTO> Create(BusInputDTO busInputDTO);
        Task<BusDTO> Update(int id, BusInputDTO busInputDTO);
        Task<bool> Deactivate(int id);
        Task<bool> Activate(int id);
    }
}