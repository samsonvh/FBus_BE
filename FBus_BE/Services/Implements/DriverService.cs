using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FBus_BE.Services.Implements
{
    public class DriverService : IDriverService
    {
        private readonly Dictionary<string, Expression<Func<Driver, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;
        public DriverService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Driver, object?>>>()
            {
                {"id", driver => driver.Id}
            };
        }
        public async Task<PageResponse<DriverListingDTO>> GetDriversWithPaging(DriverPageRequest pageRequest)
        {
            if (pageRequest != null)
            {
                int skippedCount = (pageRequest.PageNumber - 1) * pageRequest.PageSize;
                int driverCount = await _context.Drivers
                                       .Where(driver => pageRequest.Name != null ? driver.FullName.Contains(pageRequest.Name) : true)
                                       .Where(driver => pageRequest.IdCardNumber != null ? driver.IdCardNumber.Contains(pageRequest.IdCardNumber) : true)
                                       .Where(driver => pageRequest.Status != null ? driver.Status == pageRequest.Status : true)
                                       .Select(driver => _mapper.Map<DriverDTO>(driver))
                                       .CountAsync();
                if(pageRequest.OrderBy == null)
                {
                    pageRequest.OrderBy = "id";
                }
                List<DriverListingDTO> drivers = pageRequest.Direction == "desc"
                    ? await _context.Drivers.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderByDescending(orderDict[pageRequest.OrderBy])
                                       .Where(driver => pageRequest.Name != null ? driver.FullName.Contains(pageRequest.Name) : true)
                                       .Where(driver => pageRequest.IdCardNumber != null ? driver.IdCardNumber.Contains(pageRequest.IdCardNumber) : true)
                                       .Where(driver => pageRequest.Status != null ? driver.Status == pageRequest.Status : true)
                                       .Select(driver => _mapper.Map<DriverListingDTO>(driver))
                                       .ToListAsync()
                    : await _context.Drivers.Skip(skippedCount)
                                       .Take(pageRequest.PageSize)
                                       .OrderBy(orderDict[pageRequest.OrderBy])
                                       .Where(driver => pageRequest.Name != null ? driver.FullName.Contains(pageRequest.Name) : true)
                                       .Where(driver => pageRequest.IdCardNumber != null ? driver.IdCardNumber.Contains(pageRequest.IdCardNumber) : true)
                                       .Where(driver => pageRequest.Status != null ? driver.Status == pageRequest.Status : true)
                                       .Select(driver => _mapper.Map<DriverListingDTO>(driver))
                                       .ToListAsync();
                return new PageResponse<DriverListingDTO>
                {
                    Items = drivers,
                    PageIndex = pageRequest.PageNumber,
                    PageCount = (driverCount / pageRequest.PageSize) + 1,
                    PageSize = pageRequest.PageSize
                };
            }
            return null;
        }
        public async Task<DriverDTO> GetDriverDetails(int id)
        {
            Driver? driver = await _context.Drivers
                .Include(driver => driver.Account)
                .FirstOrDefaultAsync(driver => driver.Id == id);
            return _mapper.Map<DriverDTO>(driver);
        }

        public async Task<DriverDTO> Create(DriverInputDTO driverInputDTO)
        {
            Account? account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Email == driverInputDTO.Email);
            if (account == null)
            {
                account = new Account
                {
                    Email = driverInputDTO.Email,
                    Code = driverInputDTO.Code,
                    Role = "DRIVER",
                    Status = "ACTIVE",
                };
                _context.Accounts.Add(account);

                Driver driver = _mapper.Map<Driver>(driverInputDTO);
                driver.Account = account;
                _context.Drivers.Add(driver);

                await _context.SaveChangesAsync();

                return _mapper.Map<DriverDTO>(driver);
            }
            return null;
        }

        public async Task<DriverDTO> Update(int id, DriverInputDTO driverInputDTO)
        {
            Driver? driver = await _context.Drivers.Include(d => d.Account).FirstOrDefaultAsync(d => d.Id == id);
            if (driver != null)
            {
                driver = _mapper.Map(driverInputDTO, driver);
                _context.Update(driver);
                await _context.SaveChangesAsync();
                return _mapper.Map<DriverDTO>(driver);
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            Driver? driver = await _context.Drivers.Include(d => d.Account).FirstOrDefaultAsync(d => d.Id == id);
            if (driver != null)
            {
                if (driver.Account != null)
                {
                    driver.Account.Status = "ACTIVE";
                }
                driver.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            Driver? driver = await _context.Drivers.Include(d => d.Account).FirstOrDefaultAsync(d => d.Id == id);
            if (driver != null)
            {
                if (driver.Account != null)
                {
                    driver.Account.Status = "INACTIVE";
                }
                driver.Status = "INACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
