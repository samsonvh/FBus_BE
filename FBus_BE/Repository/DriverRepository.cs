using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Data;
using FBus_BE.Dto;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly FbusMainContext _context;
        public DriverRepository(FbusMainContext context)
        {
            _context = context;
        }

        public async Task<bool> active(short id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
                return false;

            driver.Status = "ACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Driver> Create(Driver driver)
        {
            _context.Set<Driver>().Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> deactive(short id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
                return false;

            driver.Status = "INACTIVE";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Driver>> GetAllDriver()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetDriverById(short id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Driver driver)
        {
            _context.Drivers.Update(driver);
        }
    }
}