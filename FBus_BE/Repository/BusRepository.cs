using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Data;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Repository
{
    public class BusRepository : IBusRepository
    {
        private readonly FbusMainContext _context;
        public BusRepository(FbusMainContext context)
        {
            _context = context;
        }
        public async Task<bool> Active(short id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
                return false;
            bus.Status = "ACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Bus> Create(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return bus;
        }

        public async Task<bool> Delete(short id)
        {
            var bus = _context.Buses.FirstOrDefault(b => b.Id == id);
            if (bus == null)
                return false;
            bus.Status = "INACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Bus>> GetAllBus()
        {
            return await _context.Buses.Include(b => b.CreatedBy).ToListAsync();
        }

        public async Task<Bus> GetBusById(short id)
        {
            var buses = await _context.Buses.Include(p => p.CreatedBy).FirstAsync(b => b.Id == id);
            return buses;
        }

        public Task<Bus> Update(Bus bus)
        {
            throw new NotImplementedException();
        }
    }
}