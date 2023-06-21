using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBus_BE.Services.Implements
{
    public class DriverService : IDriverService
    {
        private readonly Dictionary<string, Expression<Func<Driver, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;
        private readonly IFirebaseStorageService _storageService;
        private const string cloudStoragePrefix = @"https://firebasestorage.googleapis.com/v0/b/fbus-388009.appspot.com/o/";

        public DriverService(FbusMainContext context, IMapper mapper, IFirebaseStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            orderDict = new Dictionary<string, Expression<Func<Driver, object?>>>
            {
                { "id", driver => driver.Id }
            };
            _storageService = storageService;
        }

        public async Task<DefaultPageResponse<DriverListingDTO>> GetDriverList(DriverPageRequest pageRequest)
        {
            DefaultPageResponse<DriverListingDTO> pageResponse = new DefaultPageResponse<DriverListingDTO>();
            if (pageRequest.PageIndex == null)
            {
                pageRequest.PageIndex = 1;
            }
            if (pageRequest.PageSize == null)
            {
                pageRequest.PageSize = 10;
            }
            if (pageRequest.OrderBy == null)
            {
                pageRequest.OrderBy = "id";
            }
            int skippedCount = (int)((pageRequest.PageIndex - 1) * pageRequest.PageSize);
            int totalCount = await _context.Drivers
                                         .Where(driver => pageRequest.Code != null ? driver.Account.Code.Contains(pageRequest.Code) : true)
                                         .Where(driver => pageRequest.Email != null ? driver.Account.Email.Contains(pageRequest.Email) : true)
                                         .CountAsync();
            if (totalCount > 0)
            {
                List<DriverListingDTO> drivers = pageRequest.Direction == "desc"
                    ? await _context.Drivers.Skip(skippedCount)
                                             .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(driver => pageRequest.Code != null ? driver.Account.Code.Contains(pageRequest.Code) : true)
                                             .Where(driver => pageRequest.Email != null ? driver.Account.Email.Contains(pageRequest.Email) : true)
                                             .Include(driver => driver.Account)
                                             .Select(driver => _mapper.Map<DriverListingDTO>(driver))
                                             .ToListAsync()
                    : await _context.Drivers.Skip(skippedCount)
                                             .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(driver => pageRequest.Code != null ? driver.Account.Code.Contains(pageRequest.Code) : true)
                                             .Where(driver => pageRequest.Email != null ? driver.Account.Email.Contains(pageRequest.Email) : true)
                                             .Include(driver => driver.Account)
                                             .Select(driver => _mapper.Map<DriverListingDTO>(driver))
                                             .ToListAsync();
                pageResponse.Data = drivers;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<DriverDTO> GetDriverDetails(int id)
        {
            Driver? driver = await _context.Drivers
                .Include(driver => driver.Account)
                .Include(driver => driver.CreatedBy)
                .FirstOrDefaultAsync(driver => driver.Id == id);
            return _mapper.Map<DriverDTO>(driver);
        }

        public async Task<DriverDTO> Create(DriverInputDTO driverInputDTO, int? createdById)
        {
            Account? account = await _context.Accounts.FirstOrDefaultAsync(account => account.Code == driverInputDTO.Code || account.Email == driverInputDTO.Email);
            if (account == null)
            {
                account = new Account
                {
                    Code = driverInputDTO.Code,
                    Email = driverInputDTO.Email,
                    Role = "DRIVER",
                    Status = "UNSIGNED"
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                if (account.Id != null)
                {
                    Driver driver = _mapper.Map<Driver>(driverInputDTO);
                    driver.AccountId = account.Id;
                    driver.CreatedById = (short?)createdById;
                    driver.Status = "ACTIVE";

                    Uri avatarUri = null;
                    if (driverInputDTO.AvatarFile != null)
                    {
                        avatarUri = await _storageService.UploadFile(driverInputDTO.Code, driverInputDTO.AvatarFile, "avatars");
                        driver.Avatar = cloudStoragePrefix + avatarUri.AbsolutePath.Substring(avatarUri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                    }

                    _context.Drivers.Add(driver);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<DriverDTO>(driver);
                }
            }
            return null;
        }

        public async Task<DriverDTO> Update(int id, DriverInputDTO driverInputDTO, int? createdById)
        {
            Driver? driver = await _context.Drivers.Include(driver => driver.Account).FirstOrDefaultAsync(driver => driver.Id  == id);
            if (driver != null)
            {
                Account account = driver.Account;
                account.Code = driverInputDTO.Code;
                account.Email = driverInputDTO.Email;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                driver = _mapper.Map(driverInputDTO, driver);

                if (driverInputDTO.AvatarFile != null)
                {
                    if(driver.Avatar != null)
                    {
                        string fileName = driver.Avatar.Substring(driver.Avatar.LastIndexOf('/') + 1).Replace("?alt=media", "");
                        await _storageService.DeleteFile(fileName);
                    }

                    Uri avatarUri = await _storageService.UploadFile(driverInputDTO.Code, driverInputDTO.AvatarFile, "avatars");
                    driver.Avatar = cloudStoragePrefix + avatarUri.AbsolutePath.Substring(avatarUri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                }

                if (createdById != null)
                {
                    driver.CreatedById = (short?)createdById;
                }
                _context.Drivers.Update(driver);
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

        public async Task<bool> Deactivate(int id)
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
