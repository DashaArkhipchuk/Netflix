using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.DTOs.Common;
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
        public async Task<PagedResult<GenreModel>> GetAllAsync(int skip, int? take)
        {
            var count = await dbContext.GenreModels.CountAsync();
            var items = await dbContext.GenreModels.Skip(skip).Take(take ?? dbContext.GenreModels.Count()).ToListAsync() ?? new List<GenreModel>();
            return new PagedResult<GenreModel> { Items = items, TotalCount = count };
        }
    }
}
