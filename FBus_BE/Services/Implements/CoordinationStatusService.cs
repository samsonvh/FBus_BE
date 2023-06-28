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
    public class CoordinationStatusService : ICoordinationStatusService
    {
        private readonly Dictionary<string, Expression<Func<CoordinationStatus, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public CoordinationStatusService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.orderDict = new Dictionary<string, Expression<Func<CoordinationStatus, object?>>>()
            {
                {"id", coordinationStatus => coordinationStatus.Id}
            };
        }

        public Task<bool> Activate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CoordinationStatusDTO> Create(CoordinationStatusDTO coordinationStatusInputDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CoordinationStatusDTO> GetCoordinationById(int? coordinationStatusId)
        {
            if (coordinationStatusId == null)
            {
                return null;
            }

            CoordinationStatus? coordinationStatus = await _context.CoordinationStatuses
                .Include(cs => cs.Coordination)
                // .Include(cs => cs.Driver)
                .Include(cs => cs.CreatedBy)
                .FirstOrDefaultAsync(cs => cs.Id == coordinationStatusId);

            if (coordinationStatus == null)
            {
                return null;
            }

            CoordinationStatusDTO coordinationStatusDTO = _mapper.Map<CoordinationStatusDTO>(coordinationStatus);

            return coordinationStatusDTO;
        }

        public Task<CoordinationStatusDTO> Update(int id, CoordinationStatusDTO coordinationStatusInputDTO)
        {
            throw new NotImplementedException();
        }
    }
}