using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBus_BE.Models;
using FBus_BE.Repository;

namespace FBus_BE.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IAccountRepository _accountRepository;
        public DriverService(IDriverRepository driverRepository, IAccountRepository accountRepository)
        {
            _driverRepository = driverRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> Active(short id)
        {
            bool activeDriver = await _driverRepository.active(id);
            bool activeAccount = await _accountRepository.active(id);
            return activeDriver && activeAccount;
        }

        public async Task<Driver> Create(Driver driver)
        {
            var createdAccount = await _driverRepository.Create(driver);
            return createdAccount;
        }

        public async Task<bool> Deactive(short id)
        {
            bool deactiveDriver = await _driverRepository.deactive(id);
            bool deactiveAccount = await _accountRepository.deactive(id);
            return deactiveAccount && deactiveDriver;
        }

        public async Task<IEnumerable<Driver>> GetAllDriver()
        {
            return await _driverRepository.GetAllDriver();
        }

        public async Task<Driver> GetDriverById(short id)
        {
            var driverId = await _driverRepository.GetDriverById(id);
            return driverId;
        }
    }
}