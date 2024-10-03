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
    public class CastingDirectorRepository : ICastingDirectorRepository
    {
        private readonly NetflixProjectContext _dbContext;

        public CastingDirectorRepository(NetflixProjectContext dbContext) => _dbContext = dbContext;

        public void Add(CastingDirector castingDirector)
        {
            _dbContext.CastingDirectors.Add(castingDirector);
            _dbContext.SaveChanges();
        }

        public async Task<CastingDirector?> GetProfileByClientId(Guid clientId)
        {
            return await _dbContext.CastingDirectors.Include(x=>x.Client).Include(x=>x.CastingDirectorType).Where(x => x.ClientId == clientId).SingleOrDefaultAsync();
        }
    }
}
