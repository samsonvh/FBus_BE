using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;
using FBus_BE.Data;
namespace FBus_BE.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly FbusMainContext _context;
        public RouteRepository(FbusMainContext context)
        {
            _context = context;
        }

        public Task<bool> Active(short id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Route> Create(Models.Route route)
        {
            _context.Set<Models.Route>().Add(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public Task<bool> Deactive(short id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Route> GetRouteById(short id)
        {
            return await _context.Routes.FindAsync(id);
        }

        public async Task<IEnumerable<FBus_BE.Models.Route>> GetRouteList()
        {
            return await _context.Routes.ToListAsync();
        }
    }
}