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
    public class BusService : IBusService
    {
        private readonly Dictionary<string, Expression<Func<Bus, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public BusService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            orderDict = new Dictionary<string, Expression<Func<Bus, object?>>>
            {
                {"id", bus => bus.Id }
            };
        }

        public async Task<DefaultPageResponse<BusListingDTO>> GetBusList(BusPageRequest pageRequest)
        {
            DefaultPageResponse<BusListingDTO> pageResponse = new DefaultPageResponse<BusListingDTO>();
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
            int totalCount = await _context.Buses
                .Where(bus => pageRequest.Code != null ? bus.Code.Contains(pageRequest.Code) : true)
                .Where(bus => pageRequest.LicensePlate != null ? bus.LicensePlate.Contains(pageRequest.LicensePlate) : true)
                .CountAsync();
            if (totalCount > 0)
            {
                List<BusListingDTO> buses = pageRequest.OrderBy == "desc"
                    ? await _context.Buses.Skip(skippedCount)
                                          .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                          .Where(bus => pageRequest.Code != null ? bus.Code.Contains(pageRequest.Code) : true)
                                          .Where(bus => pageRequest.LicensePlate != null ? bus.LicensePlate.Contains(pageRequest.LicensePlate) : true)
                                          .Select(bus => _mapper.Map<BusListingDTO>(bus))
                                          .ToListAsync()
                    : await _context.Buses.Skip(skippedCount)
                                          .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                          .Where(bus => pageRequest.Code != null ? bus.Code.Contains(pageRequest.Code) : true)
                                          .Where(bus => pageRequest.LicensePlate != null ? bus.LicensePlate.Contains(pageRequest.LicensePlate) : true)
                                          .Select(bus => _mapper.Map<BusListingDTO>(bus))
                                          .ToListAsync();
                pageResponse.Data = buses;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<BusDTO> GetBusDetails(int id)
        {
            Bus? bus = await _context.Buses.Include(bus => bus.CreatedBy).FirstOrDefaultAsync(bus => bus.Id == id);
            return _mapper.Map<BusDTO>(bus);
        }

        public async Task<BusDTO> Create(int createdById, BusInputDTO busInputDTO)
        {
            Bus? bus = _mapper.Map<Bus>(busInputDTO);
            if (bus != null)
            {
                bus.CreatedById = (short?)createdById;
                bus.Status = "ACTIVE";
                bus.CreatedDate = DateTime.Now;

                _context.Buses.Add(bus);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusDTO>(bus);
            }

            return null;
        }

        public async Task<BusDTO> Update(int createdById, BusInputDTO busInputDTO, int id)
        {
            Bus? bus = await _context.Buses.Include(bus => bus.CreatedBy).FirstOrDefaultAsync(bus => bus.Id == id);
            if (bus != null)
            {
                bus = _mapper.Map(busInputDTO, bus);
                bus.CreatedById = (short?)createdById;

                _context.Buses.Update(bus);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusDTO>(bus);
            }

            return null;
        }

        public async Task<bool> Deactivate(int id)
        {
            Bus? bus = await _context.Buses.Include(d => d.CreatedBy).FirstOrDefaultAsync(d => d.Id == id);
            if (bus != null)
            {
                bus.Status = "INACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<BusDTO> GetBusById(int? busId)
        {
            if (busId != null)
            {
                Bus? bus = await _context.Buses.FirstOrDefaultAsync(b => b.Id == busId);
                return _mapper.Map<BusDTO>(bus);
            }
            return null;
        }

    }
}
