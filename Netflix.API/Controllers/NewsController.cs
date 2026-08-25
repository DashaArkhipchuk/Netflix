using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Animes.Common;
using Netflix.Application.Common.Content;
using Netflix.Application.News.Commands;
using Netflix.Application.News.Commands.LinkRelatedNews;
using Netflix.Application.News.Commands.PopulateNews;
using Netflix.Application.News.Queries.GetAllNews;
using Netflix.Application.News.Queries.GetNewsTypes;
using Netflix.Application.News.Queries.GetRelatedNews;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.News;
using Netflix.Domain;
using Netflix.Domain.DTOs.Common;
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
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromQuery] CriteriaNews criteria, CancellationToken ct)
        {
            var query = _mapper.Map<(GetAllContentRequest, CriteriaNews), GetAllNewsQuery>((request, criteria));
            var content = await _mediator.Send(query, ct);

            var result = new PagedResult<NewsDto>
            {
                TotalCount = content.TotalCount,
                Items = content.Items.Select(x => _mapper.Map<NewsDto>(x)).ToList()
            };


            return Ok(result);
        }

        [HttpPost("GetAllShortDto")]
        public async Task<IActionResult> GetShort([FromQuery] GetAllContentRequest request, CancellationToken ct)
        {
            var query = _mapper.Map<GetAllContentRequest, GetAllNewsQuery>(request);
            var content = await _mediator.Send(query, ct);

            var result = new PagedResult<ShortNewsDto>
            {
                TotalCount = content.TotalCount,
                Items = content.Items.Select(x => _mapper.Map<ShortNewsDto>(x)).ToList()
            };

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var query = _mapper.Map<GetContentByIdQuery<News>>(id);
            var news = await _mediator.Send(query, ct);

            if (news == null)
                return NotFound();

            return Ok(_mapper.Map<NewsExtendedDto>(news));
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes(CancellationToken ct)
        {
            var types = await _mediator.Send(new GetNewsTypesQuery(), ct);

            var dto = types.Select(t => new NewsTypeDto
            {
                Id = t.Type.Id,
                Name = t.Type.Name,
                ArticleCount = t.Count
            }).ToList();

            return Ok(dto);
        }

        [HttpPost("PopulateNewsFromExternalApi")]
        public async Task<IActionResult> PopulateFromApi([FromQuery] int? pages, [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(new PopulateNewsCommand(pages, pageSize));
            return Ok(result); // { Fetched, Inserted, SkippedDuplicates, SkippedInvalid }
        }

        [HttpPost("LinkRelatedNews")]
        public async Task<IActionResult> LinkRelatedNews([FromQuery] int? maxRelatedPerArticle, [FromQuery] double? minSimilarity)
        {
            var result = await _mediator.Send(new LinkRelatedNewsCommand(maxRelatedPerArticle, minSimilarity));
            return Ok(result);
        }

        [HttpGet("{id:guid}/Related")]
        public async Task<IActionResult> GetRelatedNews(Guid id, int skip = 0, int take = 5)
        {
            var related = await _mediator.Send(new GetRelatedNewsQuery(id, skip, take));
            var result = new PagedResult<ShortNewsDto>
            {
                TotalCount = related.TotalCount,
                Items = related.Items.Select(x => _mapper.Map<ShortNewsDto>(x)).ToList()
            };
            return Ok(result);
        }
    }
}
