using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
using FBus_BE.DTOs.ListingDTOs;
using FBus_BE.DTOs.PageRequests;
using FBus_BE.DTOs.PageResponses;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Services.Implements
{
    public class BusTripService : IBusTripService
    {
        private readonly Dictionary<string, Expression<Func<BusTrip, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public BusTripService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<BusTrip, object?>>>()
            {
                {"id", busTrip => busTrip.Id}
            };
        }

        public async Task<DefaultPageResponse<BusTripListingDTO>> GetBusTripList(BusTripPageRequest pageRequest)
        {
            DefaultPageResponse<BusTripListingDTO> pageResponse = new DefaultPageResponse<BusTripListingDTO>();
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
            int totalCount = await _context.BusTrips
                .Where(busTrip => pageRequest.CoordinationId != null ? busTrip.CoordinationId == pageRequest.CoordinationId : true)
                .CountAsync();
            if (totalCount > 0)
            {
                List<BusTripListingDTO> busTrips = pageRequest.OrderBy == "desc"
                    ? await _context.BusTrips.Skip(skippedCount)
                                        .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                        .Where(busTrip => pageRequest.CoordinationId != null ? busTrip.CoordinationId == pageRequest.CoordinationId : true)
                                        .Include(busTrips => busTrips.Coordination.Bus)
                                        .Include(busTrips => busTrips.Coordination.Driver)
                                        .Include(busTrips => busTrips.Coordination.Route)
                                        .Select(busTrips => _mapper.Map<BusTripListingDTO>(busTrips))
                                        .ToListAsync()
                    : await _context.BusTrips.Skip(skippedCount)
                                        .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                        .Where(busTrips => pageRequest.CoordinationId != null ? busTrips.CoordinationId == pageRequest.CoordinationId : true)
                                        .Include(busTrips => busTrips.Coordination.Bus)
                                        .Include(busTrips => busTrips.Coordination.Driver)
                                        .Include(busTrips => busTrips.Coordination.Route)
                                        .Select(busTrips => _mapper.Map<BusTripListingDTO>(busTrips))
                                        .ToListAsync();
                pageResponse.Data = busTrips;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<BusTripDTO> GetBusTripDetails(int? busTripId)
        {
            BusTrip? busTrip = await _context.BusTrips
            .Include(busTrip => busTrip.Coordination)
            .Include(busTrip => busTrip.Coordination.Bus)
            .Include(busTrip => busTrip.Coordination.Driver.CreatedBy)
            .Include(busTrip => busTrip.Coordination.Route)
            .Include(busTrip => busTrip.Coordination.CreatedBy)
            .FirstOrDefaultAsync();
            return _mapper.Map<BusTripDTO>(busTrip);
        }

        public async Task<BusTripDTO> Create(BusTripInputDTO busTripInputDTO)
        {
            Coordination? coordination = await _context.Coordinations
            .Include(c => c.CreatedBy)
            .Include(c => c.Driver.CreatedBy)
            .Include(c => c.Bus.CreatedBy)
            .Include(c => c.Route)
            .FirstOrDefaultAsync(c => c.Id == busTripInputDTO.CoordinationId);
            BusTrip? busTrip = _mapper.Map<BusTrip>(busTripInputDTO);

            if (busTrip != null)
            {
                busTrip.CoordinationId = busTripInputDTO.CoordinationId;
                busTrip.Status = "ACTIVE";
                _context.BusTrips.Add(busTrip);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
        }

        public async Task<BusTripDTO> Update(int id, BusTripInputDTO busTripInputDTO)
        {
            BusTrip? busTrip = await _context.BusTrips
            .Include(busTrip => busTrip.Coordination)
            .Include(busTrip => busTrip.Coordination.Bus)
            .Include(busTrip => busTrip.Coordination.Driver.CreatedBy)
            .Include(busTrip => busTrip.Coordination.Route)
            .Include(busTrip => busTrip.Coordination.CreatedBy)
            .FirstOrDefaultAsync(busTrip => busTrip.Id == id);
            if (busTrip != null)
            {
                busTrip.Coordination = await _context.Coordinations.FindAsync(busTripInputDTO.CoordinationId);
                string status = busTripInputDTO.Status.ToUpper();
                if (status != "ACTIVE" && status != "INACTIVE")
                    return null;
                busTrip.Status = status;
                _context.Update(busTrip);
                await _context.SaveChangesAsync();
                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
        }

        public async Task<bool> ChangeStatus(int id)
        {
            BusTrip? busTrip = await _context.BusTrips.Include(b => b.Coordination).FirstOrDefaultAsync(b => b.Id == id);
            if (busTrip != null)
            {
                busTrip.Status = "ACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Deactivate(int id)
        {
            BusTrip? busTrip = await _context.BusTrips.Include(b => b.Coordination).FirstOrDefaultAsync(b => b.Id == id);
            if (busTrip != null)
            {
                busTrip.Status = "INACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}