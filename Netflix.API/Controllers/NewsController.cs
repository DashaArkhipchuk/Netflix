using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Animes.Common;
using Netflix.Application.Common.Content;
using Netflix.Application.News.Queries.GetAllNews;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.News;
using Netflix.Domain;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, CriteriaNews criteria)
        {
            var command = _mapper.Map<(GetAllContentRequest,CriteriaNews), GetAllNewsQuery>((request, criteria));

            var content = await _mediator.Send(command);

            var newsDtos = content
                .Select(x => new NewsDto { Id = x.Id, Author = $"{x.Author.Name} {x.Author.Surname}", Description = x.Description, ImageURL =x.ImageURL, PublishedDate = x.PublishedDate, Title = x.Title, Type = x.Type.Name  }).ToList();

            return Ok(newsDtos);
        }

        [HttpPost("GetAllShortDto")]
        public async Task<IActionResult> GetShort([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentRequest, GetAllNewsQuery>(request);

            var content = await _mediator.Send(command);

            var newsDtos = content
                .Select(x => new ShortNewsDto { Id = x.Id, Author = $"{x.Author.Name} {x.Author.Surname}",ImageURL = x.ImageURL, PublishedDate = x.PublishedDate, Title = x.Title }).ToList();

            return Ok(newsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<News>>(id);

            var news = await _mediator.Send(command);

            if (news == null)
                return NotFound();

            return Ok(_mapper.Map<NewsExtendedDto>(news));
        }
    }
}
