using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FBus_BE.DTOs;
using FBus_BE.DTOs.InputDTOs;
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

        public async Task<bool> ChangeStatus(int id, string status)
        {
            BusTrip? busTrip = await _context.BusTrips.Include(b => b.Coordination).FirstOrDefaultAsync(b => b.Id == id);
            if (busTrip != null)
            {
                busTrip.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<BusTripDTO> Create(BusTripInputDTO busTripInputDTO)
        {
            BusTrip? busTrip = _mapper.Map<BusTrip>(busTripInputDTO);
            if (busTrip != null)
            {
                busTrip.CoordinationId = busTripInputDTO.CoordinationId;
                busTrip.CreatedDate = DateTime.Now;

                _context.BusTrips.Add(busTrip);
                await _context.SaveChangesAsync();
                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
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
                busTrip = _mapper.Map(busTripInputDTO, busTrip);
                _context.Update(busTrip);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusTripDTO>(busTrip);

            }
            return null;

        }
    }
}