using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;
        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }
        public async Task<bool> active(short id)
        {
            var activeStation = await _stationRepository.active(id);
            return activeStation;
        }

        public Task<Station> Create(Station station)
        {
            return _stationRepository.Create(station);
        }

        public async Task<bool> deactive(short id)
        {
            var deactiveStation = await _stationRepository.deactive(id);
            return deactiveStation;
        }

        public async Task<IEnumerable<Station>> GetAllStation()
        {
            return await _stationRepository.GetAllStation();
        }

        public async Task<Station> GetStationById(short id)
        {
            return await _stationRepository.GetStationById(id);
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Station station)
        {
            throw new NotImplementedException();
        }
    }
}