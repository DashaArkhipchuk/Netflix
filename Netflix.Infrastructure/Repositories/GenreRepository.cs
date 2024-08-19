using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    internal class GenreRepository(NetflixProjectContext dbContext) : IGenreRepository
    {
        public async Task<List<GenreModel>> GetAllAsync(int skip, int take)
        {
            return await dbContext.GenreModels.Skip(skip).Take(take).ToListAsync() ?? new List<GenreModel>();
        }
    }
}
