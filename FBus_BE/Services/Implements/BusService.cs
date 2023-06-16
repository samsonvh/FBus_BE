using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public class BusService : IBusService
    {
        private readonly Dictionary<string, Expression<Func<Bus, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public BusService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Bus, object?>>>()
            {
                {"id", bus => bus.Id}
            };
        }

        public async Task<bool> Activate(int id)
        {
            Bus? bus = await _context.Buses.FirstOrDefaultAsync(d => d.Id == id);
            if (bus != null)
            {
                bus.Status = "ACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<BusDTO> Create(BusInputDTO busInputDTO)
        {
            // Bus existingBus = await _context.Buses.FirstOrDefaultAsync(bus => bus.Code == busInputDTO.Code);

            // if (existingBus == null)
            // {
            //     var bus = _mapper.Map<Bus>(busInputDTO);
            //     _context.Buses.Add(bus);
            //     await _context.SaveChangesAsync();

            //     return _mapper.Map<BusDTO>(bus);
            // }

            // return null;

            if (busInputDTO.DateOfRegistration < SqlDateTime.MinValue.Value || busInputDTO.DateOfRegistration > SqlDateTime.MaxValue.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(busInputDTO.DateOfRegistration), "DateOfRegistration is outside the valid range.");
            }

            Bus? existingBus = await _context.Buses.FirstOrDefaultAsync(bus => bus.Code == busInputDTO.Code || bus.LicensePlate == busInputDTO.LicensePlate);

            if (existingBus == null)
            {
                var bus = _mapper.Map<Bus>(busInputDTO);
                bus.CreatedDate = DateTime.Now; // Set the current date and time
                _context.Buses.Add(bus);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusDTO>(bus);
            }

            return null;
        }

        public async Task<BusDTO> Update(int id, BusInputDTO busInputDTO)
        {
            Bus? bus = await _context.Buses.FirstOrDefaultAsync(b => b.Id == id);
            if (bus != null)
            {
                bus.Code = busInputDTO.Code;
                bus.LicensePlate = busInputDTO.LicensePlate;
                bus.Brand = busInputDTO.Brand;
                bus.Model = busInputDTO.Model;
                bus.Color = busInputDTO.Color;
                bus.Seat = busInputDTO.Seat;
                bus.Status = busInputDTO.Status;
                bus.DateOfRegistration = busInputDTO.DateOfRegistration;
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