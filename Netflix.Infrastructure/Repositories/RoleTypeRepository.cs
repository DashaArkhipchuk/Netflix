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
    internal class RoleTypeRepository(NetflixProjectContext dbContext) : IRoleTypeRepository
    {
        public async Task<List<RoleType>> GetAllAsync(int skip, int take)
        {
            return await dbContext.RoleTypes.Skip(skip).Take(take).ToListAsync();
        }

        public Task<RoleType?> GetTypeById(Guid id)
        {
            return dbContext.RoleTypes.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
