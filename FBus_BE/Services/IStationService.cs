using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;

namespace FBus_BE.Services
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetAllStation();
        Task<Station> Create(Station station);
        Task<Station> GetStationById(short id);
        public void Update(Station station);
        Task SaveChanges();
        Task<bool> deactive(short id);
        Task<bool> active(short id);
    }
}