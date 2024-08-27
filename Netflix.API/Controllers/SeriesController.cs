using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Films.Queries.GetAllFilms;
using Netflix.Application.Films.Queries.GetFilmByIdQuery;
using Netflix.Application.Series.Commands.PopulateSeriesEpisodes;
using Netflix.Application.Series.Queries.GetAllSeries;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.Series.GetSeriesById;
using Netflix.Domain;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SeriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromBody] Criteria criteria)
        {
            var command = _mapper.Map<(GetAllContentRequest, Criteria), GetAllContentQuery<Domain.Series>>((request, criteria));

            var series = await _mediator.Send(command);
            var seriesDtos = series
                .Select(x => new ContentDto() { Id = x.Id, Name = x.Name, PictureUrl = x.PictureUrl, Rating = x.Rating, ReleaseYear = x.ReleaseDate.Year }).ToList();

            return Ok(new ContentResponse<ContentDto>(seriesDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<Series>>(id);

            var series = await _mediator.Send(command);

            if (series == null)
                return NotFound();

            return Ok(_mapper.Map<SeriesExtendedDto>(series));
            return Ok();
        }

        [HttpPost("PoulateEpisodes")]
        public async Task<IActionResult> Populate(int id = 0)
        {
            var command = new PopulateSeriesEpisodesCommand(id);

            var series = await _mediator.Send(command);

            return Ok();
        }
    }
}
