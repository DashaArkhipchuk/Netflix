using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetLocationsByRegionName(string regionName, int skip, int take);
        Task<List<string>> GetRegionsNamesAsync(int skip, int take);
        Task<Location?> GetLocationByLocationAndRegionNamesAsync(string locationName, string regionName);
        void Add(Location location);
        public Task<Location?> GetLocationById(Guid id);
    }
}
