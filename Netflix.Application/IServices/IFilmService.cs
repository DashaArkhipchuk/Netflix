using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.IServices
{
    public interface IFilmService
    {
        IEnumerable<Film> GetAll(int skip, int take);
    }
}
