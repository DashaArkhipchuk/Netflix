using Netflix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface IGenreRepository
    {
        Task<PagedResult<GenreModel>> GetAllAsync(int skip, int? take);
    }
}
