using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Films.Queries.GetFilmByIdQuery;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Domain;

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

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromBody] Criteria criteria)
        {
            var command = _mapper.Map<(GetAllContentRequest, Criteria), GetAllContentQuery<Film>>((request, criteria));

            var films = await _mediator.Send(command);
            var filmDtos = films
                .Select(x => new ContentDto() { Id = x.Id, Name = x.Name, PictureUrl = x.PictureUrl, Rating = x.Rating, ReleaseYear = x.ReleaseDate.Year }).ToList();

            return Ok(new ContentResponse<ContentDto>(filmDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<Film>>(id);

            var film = await _mediator.Send(command);

            if (film == null)
                return NotFound();

            return Ok(_mapper.Map<FilmExtendedDto>(film));
        }
    }
}
