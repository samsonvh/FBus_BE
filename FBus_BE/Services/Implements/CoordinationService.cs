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
    public class CoordinationService : ICoordinationService
    {
        private readonly Dictionary<string, Expression<Func<Coordination, object?>>> orderDict;
        private readonly FbusMainContext _context;
        private readonly IMapper _mapper;

        public CoordinationService(FbusMainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            orderDict = new Dictionary<string, Expression<Func<Coordination, object?>>>
            {
                {"id", coordination => coordination.Id }
            };
        }

        public async Task<DefaultPageResponse<CoordinationListingDTO>> GetCoordinationList(CoordinationPageRequest pageRequest)
        {
            DefaultPageResponse<CoordinationListingDTO> pageResponse = new DefaultPageResponse<CoordinationListingDTO>();
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
            int totalCount = await _context.Coordinations
                .Where(coordination => pageRequest.DriverId != null ? coordination.DriverId == pageRequest.DriverId : true)
                .Where(coordination => pageRequest.BusId != null ? coordination.BusId == pageRequest.BusId : true)
                .Where(coordination => pageRequest.RouteId != null ? coordination.RouteId == pageRequest.RouteId : true)
                .CountAsync();
            if (totalCount > 0)
            {
                List<CoordinationListingDTO> coordinations = pageRequest.OrderBy == "desc"
                    ? await _context.Coordinations.Skip(skippedCount)
                                          .OrderByDescending(orderDict[pageRequest.OrderBy.ToLower()])
                                          .Where(coordination => pageRequest.DriverId != null ? coordination.DriverId == pageRequest.DriverId : true)
                                          .Where(coordination => pageRequest.BusId != null ? coordination.BusId == pageRequest.BusId : true)
                                          .Where(coordination => pageRequest.RouteId != null ? coordination.RouteId == pageRequest.RouteId : true)
                                          .Include(coordination => coordination.Bus)
                                          .Include(coordination => coordination.Driver)
                                          .Include(coordination => coordination.Route)
                                          .Select(coordination => _mapper.Map<CoordinationListingDTO>(coordination))
                                          .ToListAsync()
                    : await _context.Coordinations.Skip(skippedCount)
                                          .OrderBy(orderDict[pageRequest.OrderBy.ToLower()])
                                          .Where(coordination => pageRequest.DriverId != null ? coordination.DriverId == pageRequest.DriverId : true)
                                          .Where(coordination => pageRequest.BusId != null ? coordination.BusId == pageRequest.BusId : true)
                                          .Where(coordination => pageRequest.RouteId != null ? coordination.RouteId == pageRequest.RouteId : true)
                                          .Include(coordination => coordination.Bus)
                                          .Include(coordination => coordination.Driver)
                                          .Include(coordination => coordination.Route)
                                          .Select(coordination => _mapper.Map<CoordinationListingDTO>(coordination))
                                          .ToListAsync();
                pageResponse.Data = coordinations;
            }
            pageResponse.PageIndex = (int)pageRequest.PageIndex;
            pageResponse.PageCount = (int)(totalCount / pageRequest.PageSize) + 1;
            pageResponse.PageSize = (int)pageRequest.PageSize;
            return pageResponse;
        }

        public async Task<CoordinationDTO> Create(int createdById, CoordinationInputDTO coordinationInputDTO)
        {
            Coordination? coordination = _mapper.Map<Coordination>(coordinationInputDTO);
            if (coordination != null)
            {
                coordination.CreatedById = (short?)createdById;
                _context.Coordinations.Add(coordination);
                await _context.SaveChangesAsync();

                CoordinationStatus coordinationStatus = new CoordinationStatus
                {
                    CoordinationId = coordination.Id,
                    CreatedById = (short?)createdById,
                    Note = coordination.Note,
                    StatusOrder = 1,
                    UpdatedStatus = coordination.Status
                };
                _context.CoordinationStatuses.Add(coordinationStatus);
                await _context.SaveChangesAsync();

                return _mapper.Map<CoordinationDTO>(coordination);
            }
            return null;
        }

        public async Task<CoordinationDTO> Update(int createdById, CoordinationInputDTO coordinationInputDTO, int id)
        {
            Coordination? coordination = await _context.Coordinations
                .Include(coordination => coordination.CreatedBy)
                .Include(coordination => coordination.Driver)
                .Include(coordination => coordination.Bus)
                .Include(coordination => coordination.Route)
                .FirstOrDefaultAsync(coordination => coordination.Id == id);
            if (coordination != null)
            {
                coordination = _mapper.Map(coordinationInputDTO, coordination);
                coordination.CreatedById = (short?)createdById;
                _context.Coordinations.Update(coordination);
                await _context.SaveChangesAsync();

                CoordinationStatus? latestStatus = await _context.CoordinationStatuses.LastOrDefaultAsync(coordinationStatus => coordinationStatus.CoordinationId == id);

                CoordinationStatus coordinationStatus = new CoordinationStatus
                {
                    CoordinationId = coordination.Id,
                    CreatedById = (short?)createdById,
                    Note = coordination.Note,
                    StatusOrder = (byte)(latestStatus.StatusOrder + 1),
                    OriginalStatus = latestStatus.UpdatedStatus,
                    UpdatedStatus = coordination.Status
                };
                _context.CoordinationStatuses.Add(coordinationStatus);
                await _context.SaveChangesAsync();

                return _mapper.Map<CoordinationDTO>(coordination);
            }
            return null;
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
    }
}
