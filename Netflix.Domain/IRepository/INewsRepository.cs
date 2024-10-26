using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync(int skip, int take,bool sortByLatest);
        Task<News?> GetByIdAsync(Guid? id);
    }
}
