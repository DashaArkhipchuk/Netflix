using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Films.Queries.GetAllFilms;
using Netflix.Contracts.Films.GetAllFilms;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FilmController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllFilmsRequest request)
        {
            var command = _mapper.Map<GetAllFilmsQuery>(request);

            var films = await _mediator.Send(command);
            var filmDtos = films
                .Select(x => new FilmDto() { Id = x.Id, Name = x.Name, Rating = x.Rating, ReleaseYear = x.ReleaseDate.Year }).ToList();

            return Ok(new FilmsResponse (filmDtos));
        }
    }
}
