

using Microsoft.EntityFrameworkCore;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;

namespace Netflix.Infrastructure.Repositories
{
    internal class LocationRepository(NetflixProjectContext dbContext) : ILocationRepository
    {
        public void Add(Location location)
        {
            dbContext.Locations.Add(location);
            dbContext.SaveChanges();
        }

        public Task<Location?> GetLocationByLocationAndRegionNamesAsync(string locationName, string regionName)
        {
            return dbContext.Locations.FirstOrDefaultAsync(l => l.LocationName.Contains(locationName) && l.RegionName.Contains(regionName));
        }

        public Task<List<Location>> GetLocationsByRegionName(string regionName, int skip, int take)
        {
            return dbContext.Locations.Where(l => l.RegionName.ToLower() == regionName.ToLower()).Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<string>> GetRegionsNamesAsync(int skip, int take)
        {
            return dbContext.Locations.Select(l => l.RegionName).Distinct().Skip(skip).Take(take).ToListAsync();
        }

        public Task<Location?> GetLocationById(Guid id)
        {
            return dbContext.Locations.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
