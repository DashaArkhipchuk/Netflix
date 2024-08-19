using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface IGenreRepository
    {
        Task<List<GenreModel>> GetAllAsync(int skip, int take);
    }
}
