using Microsoft.EntityFrameworkCore;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    internal class CastingDirectorTypeRepository(NetflixProjectContext dbContext) : ICastingDirectorTypeRepository
    {
        public async Task<List<CastingDirectorType>> GetAllAsync(int skip, int take)
        {
            return await dbContext.CastingDirectorTypes.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<CastingDirectorType?> GetTypeById(Guid id)
        {
            return await dbContext.CastingDirectorTypes.SingleOrDefaultAsync(x=>x.Id == id);
        }
    }
}
