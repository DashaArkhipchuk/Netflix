using Netflix.Application.IServices;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using Netflix.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Netflix.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
           _filmRepository = filmRepository;
        }

        public IEnumerable<Film> GetAll(int skip, int take)
        {
            return _filmRepository.GetAllAsync(skip, take).Result;
        }
    }
}
