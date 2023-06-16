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
    public class CoordinationService : ICoordinationService
    {
        private readonly Dictionary<string, Expression<Func<Coordination, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public CoordinationService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<Coordination, object?>>>()
            {
                {"id", coordination => coordination.Id}
            };
        }

        public async Task<bool> Activate(int id)
        {
            Coordination? coordination = await _context.Coordinations.FirstOrDefaultAsync(d => d.Id == id);
            if (coordination != null)
            {
                coordination.Status = "ACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CoordinationDTO> Create(CoordinationInputDTO coordinationInputDTO)
        {
            Coordination? existingCoordination = await _context.Coordinations.FirstOrDefaultAsync(Coordination => Coordination.CreatedById == coordinationInputDTO.CreatedById
                 || Coordination.BusId == coordinationInputDTO.BusId || Coordination.RouteId == coordinationInputDTO.RouteId);
            if (existingCoordination != null)
            {
                var bus = await _context.Buses.FirstOrDefaultAsync(b => b.Id == coordinationInputDTO.BusId && b.Status == "ACTIVE");
                var createdBy = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == coordinationInputDTO.CreatedById && a.Role == "ADMIN");
                var route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == coordinationInputDTO.RouteId && r.Status == "ACTIVE");

                if (bus != null && createdBy != null && route != null)
                {
                    var coordination = _mapper.Map<Coordination>(coordinationInputDTO);
                    _context.Coordinations.Add(coordination);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<CoordinationDTO>(coordination);
                }
            }
            return null;

        }

        public async Task<bool> Deactivate(int id)
        {
            Coordination? coordination = await _context.Coordinations.FirstOrDefaultAsync(d => d.Id == id);
            if (coordination != null)
            {
                coordination.Status = "INACTIVE";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CoordinationDTO> GetCoordinationById(int? coordinationId)
        {
            if (coordinationId != null)
            {
                Coordination? coordination = await _context.Coordinations.FirstOrDefaultAsync(c => c.Id == coordinationId);
                return _mapper.Map<CoordinationDTO>(coordination);
            }
            return null;
        }

        public async Task<CoordinationDTO> Update(int id, CoordinationInputDTO coordinationInputDTO)
        {
            Coordination? coordination = await _context.Coordinations.FirstOrDefaultAsync(c => c.Id == id);
            if (coordination != null)
            {
                coordination.DriverId = coordinationInputDTO.DriverId;
                coordination.BusId = coordinationInputDTO.BusId;
                coordination.RouteId = coordinationInputDTO.RouteId;
                coordination.Note = coordinationInputDTO.Note;
                coordination.DateLine = coordinationInputDTO.DateLine;
                coordination.DueDate = coordinationInputDTO.DueDate;
                coordination.Status = coordinationInputDTO.Status;
                await _context.SaveChangesAsync();
                return _mapper.Map<CoordinationDTO>(coordination);
            }
            return null;
        }
    }
}