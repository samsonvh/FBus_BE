using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDriver();
        Task<Driver> Create(Driver driver);
        Task<Driver> GetDriverById(short id);
        Task<bool> Active(short id);
        Task<bool> Deactive(short id);
    }
}