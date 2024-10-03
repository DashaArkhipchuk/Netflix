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
    internal class AuditionRepository(NetflixProjectContext dbContext) : IAuditionRepository
    {
        public void Add(Audition audition)
        {
            dbContext.Auditions.Add(audition);
            dbContext.SaveChanges();
        }

        public async Task<Audition?> GetByIdAsync(Guid? id)
        {
            return await dbContext.Auditions.Include(a => a.CastingCall).Include(a => a.Location).SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> Remove(Guid auditionId)
        {
            var entity = await dbContext.Auditions.FindAsync(auditionId);

            if (entity == null)
            {
                return false;
            }

            dbContext.Auditions.Remove(entity);

            var result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

    }
}
