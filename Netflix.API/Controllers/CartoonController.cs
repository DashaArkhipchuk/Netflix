using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.Series.GetSeriesById;
using Netflix.Domain;
using Netflix.Contracts.Content;
using Netflix.Application.Cartoons.Common;
using Microsoft.AspNetCore.Authorization;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartoonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartoonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromBody] Criteria criteria)
        {
            var command = _mapper.Map<(GetAllContentRequest, Criteria), GetAllContentQuery<ContentDtoWithTypeCartoon>>((request, criteria));

            var content = await _mediator.Send(command);
            
            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<ContentWithTypeCartoon>>(id);

            var content = await _mediator.Send(command);

            if (content == null)
                return NotFound();

            

            return Ok(_mapper.Map<ContentWithTypeResponse>(content));
        }
    }
}
