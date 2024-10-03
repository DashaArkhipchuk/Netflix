using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Animes.Common;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.Content;
using Netflix.Application.Doramas.Common;
using Netflix.Contracts.Common;
using Netflix.Contracts.Content;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AnimeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromBody] Criteria criteria)
        {
            var command = _mapper.Map<(GetAllContentRequest, Criteria), GetAllContentQuery<ContentDtoWithTypeAnime>>((request, criteria));

            var content = await _mediator.Send(command);

            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<ContentWithTypeAnime>>(id);

            var content = await _mediator.Send(command);

            if (content == null)
                return NotFound();



            return Ok(_mapper.Map<ContentWithTypeResponse>(content));
        }

    }
}
