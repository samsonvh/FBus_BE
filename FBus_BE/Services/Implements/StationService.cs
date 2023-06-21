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
    public class StationService : IStationService
    {
        private readonly Dictionary<string, Expression<Func<Station, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;
        private readonly IFirebaseStorageService _storageService;
        private const string cloudStoragePrefix = @"https://firebasestorage.googleapis.com/v0/b/fbus-388009.appspot.com/o/";

        public StationService(FbusMainContext context, IMapper mapper, IFirebaseStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            orderDict = new Dictionary<string, Expression<Func<Station, object?>>>
            {
                { "id", station => station.Id }
            };
            _storageService = storageService;
        }

        public async Task<DefaultPageResponse<StationListingDTO>> GetStationList(StationPageRequest pageRequest)
        {
            DefaultPageResponse<StationListingDTO> pageResponse = new DefaultPageResponse<StationListingDTO>();
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
            int totalCount = await _context.Stations
                .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                .CountAsync();
            if (totalCount > 0)
            {
                List<StationListingDTO> stations = pageRequest.Direction == "desc"
                    ? await _context.Stations.Skip(skippedCount)
                                             .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                                             .Select(station => _mapper.Map<StationListingDTO>(station))
                                             .ToListAsync()
                    : await _context.Stations.Skip(skippedCount)
                                             .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                             .Where(station => pageRequest.Code != null ? station.Code.Contains(pageRequest.Code) : true)
                                             .Select(station => _mapper.Map<StationListingDTO>(station))
                                             .ToListAsync();
                pageResponse.Data = stations;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<StationDTO> GetStationDetails(int id)
        {
            Station? station = await _context.Stations.Include(station => station.CreatedBy)
                .FirstOrDefaultAsync(station => station.Id == id);
            return _mapper.Map<StationDTO>(station);
        }

        public async Task<StationDTO> Create(int createdById, StationInputDTO stationInputDTO)
        {
            Station? station = _mapper.Map<Station>(stationInputDTO);
            if (station != null)
            {
                if (stationInputDTO.ImageFile != null)
                {
                    Uri uri = await _storageService.UploadFile(stationInputDTO.Code, stationInputDTO.ImageFile, "stations");
                    station.Image = cloudStoragePrefix + uri.AbsolutePath.Substring(uri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                }
                station.CreatedById = (short?)createdById;
                _context.Stations.Add(station);
                await _context.SaveChangesAsync();
                return _mapper.Map<StationDTO>(station);
            }
            return null;
        }

        public async Task<StationDTO> Update(int createdById, StationInputDTO stationInputDTO, int id)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                if (stationInputDTO.ImageFile != null)
                {
                    if(station.Image != null)
                    {
                        string fileName = station.Image.Substring(station.Image.LastIndexOf('/') + 1).Replace("?alt=media", "");
                        await _storageService.DeleteFile(fileName);
                    }
                    Uri uri = await _storageService.UploadFile(stationInputDTO.Code, stationInputDTO.ImageFile, "stations");
                    station.Image = cloudStoragePrefix + uri.AbsolutePath.Substring(uri.AbsolutePath.LastIndexOf('/') + 1) + "?alt=media";
                }
                station.CreatedById = (short?)createdById;
                _context.Stations.Add(station);
                await _context.SaveChangesAsync();
                return _mapper.Map<StationDTO>(station);
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                station.Status = status;
                _context.Stations.Update(station);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Deactivate(int id)
        {
            Station? station = await _context.Stations.FirstOrDefaultAsync(station => station.Id == id);
            if (station != null)
            {
                station.Status = "INACTIVE";
                _context.Stations.Update(station);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
