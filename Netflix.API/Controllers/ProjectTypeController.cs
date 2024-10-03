using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Application.Regions.Queries.GetRegionNames;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Domain;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectTypeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentWithoutFilteringQuery<ProjectType>>(request);

            var types = await _mediator.Send(command);

            var typeDtos = types.Select(x => new { x.Id, x.ProjectTypeName }).ToList();

            return Ok(typeDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<ProjectType>>(id);

            var type = await _mediator.Send(command);

            if (type == null)
                return NotFound();

            return Ok(new { type.Id, type.ProjectTypeName });
        }
    }
}
