using Microsoft.AspNetCore.Mvc;
using Netflix.Application.IServices;
using Netflix.Domain;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;
        private readonly IFilmService _filmService;

        public FilmController(ILogger<FilmController> logger,
            IFilmService filmService)
        {
            _logger = logger;
            _filmService = filmService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Film> Get(int skip = 0, int take = 10)
        {
            var result = _filmService.GetAll(skip,take);
            return result;
        }
    }
}
