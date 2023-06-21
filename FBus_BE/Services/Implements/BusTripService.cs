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

        public Task<bool> Activate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BusTripDTO> Create(BusTripInputDTO busTripInputDTO)
        {
            BusTrip? busTrip = await _context.BusTrips.FirstOrDefaultAsync(busTrip => busTrip.CoordinationId == busTripInputDTO.CoordinationId);
            if (busTrip == null)
            {
                busTrip = _mapper.Map<BusTrip>(busTripInputDTO);
                _context.BusTrips.Add(busTrip);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
        }

        public Task<bool> Deactivate(int id)
        {
            return null;
        }

        public async Task<BusTripDTO> GetBusById(int? busTripId)
        {
            if (busTripId != null)
            {
                BusTrip? busTrip = await _context.BusTrips.FirstOrDefaultAsync(b => b.Id == busTripId);
                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
        }

        public async Task<List<BusTripDTO>> GetBusByCoordinationId(int? coordinationId)
        {
            if (coordinationId != null)
            {
                List<BusTrip> busTrips = await _context.BusTrips
                            .Where(bt => bt.CoordinationId == coordinationId)
                            .ToListAsync();
                return _mapper.Map<List<BusTripDTO>>(busTrips);
            }

            return null;
        }

        public async Task<BusTripDTO> Update(int id, BusTripInputDTO busTripInputDTO)
        {
            BusTrip? busTrip = await _context.BusTrips.FirstOrDefaultAsync(b => b.Id == id);
            if (busTrip != null)
            {
                busTrip.CoordinationId = busTripInputDTO.CoordinationId;
                busTrip.StartingDate = busTripInputDTO.StartingDate;
                busTrip.EndingDate = busTripInputDTO.EndingDate;
                busTrip.Status = busTripInputDTO.Status;
                await _context.SaveChangesAsync();
                return _mapper.Map<BusTripDTO>(busTrip);
            }
            return null;
        }
    }
}