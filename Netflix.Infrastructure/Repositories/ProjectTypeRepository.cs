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
    internal class ProjectTypeRepository(NetflixProjectContext dbContext) : IProjectTypeRepository
    {
        public async Task<List<ProjectType>> GetAllAsync(int skip, int take)
        {
            return await dbContext.ProjectTypes.Skip(skip).Take(take).ToListAsync();
        }

        public Task<ProjectType?> GetTypeById(Guid id)
        {
            return dbContext.ProjectTypes.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
