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
    internal class NewsRepository(NetflixProjectContext dbContext) : INewsRepository
    {
        public async Task<List<News>> GetAllAsync(int skip, int take,bool sortByLatest)
        {
            var query = dbContext.News.Include(c => c.Author).Include(c => c.Type).AsQueryable();
            if (sortByLatest)
            {
                query = query.OrderByDescending(news => news.PublishedDate);
            }

            query = query.Skip(skip).Take(take);
            return await query.ToListAsync() ?? new List<News>();
        }

        public async Task<News?> GetByIdAsync(Guid? id)
        {
            return await dbContext.News.Include(c => c.Type).Include(c => c.Author).SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
