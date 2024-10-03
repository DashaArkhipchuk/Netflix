using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.CastingDirectorProfileTypes.Queries.GetAllCastingDirectorProfileTypes;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Contracts.Common;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastingDirectorProfileTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CastingDirectorProfileTypeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentWithoutFilteringQuery<CastingDirectorType>>(request);

            var types = await _mediator.Send(command);

            var typeDtos = types.Select ( x=> new { x.Id, x.Name }).ToList();

            return Ok(typeDtos);
        }
    }
}
