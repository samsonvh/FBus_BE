using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Dto;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }
        public async Task<bool> Active(short id)
        {
            return await _busRepository.Active(id);
        }

        public async Task<Bus> Create(Bus bus)
        {
            return await _busRepository.Create(bus);
        }

        public async Task<bool> Delete(short id)
        {
            return await _busRepository.Delete(id);
        }

        public async Task<IEnumerable<Bus>> GetAllBus()
        {
            return await _busRepository.GetAllBus();
        }

        public async Task<Bus> GetBusById(short id)
        {
            return await _busRepository.GetBusById(id);
        }

        public async Task<Bus> Update(Bus bus)
        {
            return await _busRepository.Update(bus);
        }
    }
}