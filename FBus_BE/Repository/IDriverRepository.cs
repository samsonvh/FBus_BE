using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Repository
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllDriver();
        Task<Driver> Create(Driver driver);
        Task<Driver> GetDriverById(short id);
        public void Update(Driver driver);
        Task SaveChanges();
        Task<bool> deactive(short id);
        Task<bool> active(short id);
    }
}