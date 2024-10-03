using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Contracts.Common;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EthnicAppearanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EthnicAppearanceController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentWithoutFilteringQuery<EthnicAppearance>>(request);

            var appearances = await _mediator.Send(command);

            var appearancesDtos = appearances.Select(x => new { x.Id, x.EthnicAppearanceName }).ToList();

            return Ok(appearancesDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<EthnicAppearance>>(id);

            var appearance = await _mediator.Send(command);

            if (appearance == null)
                return NotFound();

            return Ok(new { appearance.Id, appearance.EthnicAppearanceName });
        }
    }
}
