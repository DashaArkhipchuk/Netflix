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
    internal class GenderRepository(NetflixProjectContext dbContext) : IGenderRepository
    {
        public async Task<List<Gender>> GetAllAsync(int skip, int take)
        {
            return await dbContext.Genders.Skip(skip).Take(take).ToListAsync();
        }

        public Task<Gender?> GetTypeById(Guid id)
        {
            return dbContext.Genders.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
