using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Data;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Repository
{
    public class StationRepository : IStationRepository
    {
        private readonly FbusMainContext _context;
        public StationRepository(FbusMainContext context)
        {
            _context = context;
        }
        public async Task<bool> active(short id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
                return false;

            station.Status = "ACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Station> Create(Station station)
        {
            _context.Set<Station>().Add(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<bool> deactive(short id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
                return false;

            station.Status = "INACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Station>> GetAllStation()
        {
            return await _context.Stations.ToListAsync();
        }

        public async Task<Station> GetStationById(short id)
        {
            return await _context.Stations.FindAsync(id);
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