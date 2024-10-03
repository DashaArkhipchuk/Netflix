using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    internal class EthnicAppearanceRepository(NetflixProjectContext dbContext) : IEthnicAppearanceRepository
    {
        public async Task<List<EthnicAppearance>> GetAllAsync(int skip, int take)
        {
            return await dbContext.EthnicAppearances.Skip(skip).Take(take).ToListAsync();
        }

        public Task<EthnicAppearance?> GetTypeById(Guid id)
        {
            return dbContext.EthnicAppearances.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
