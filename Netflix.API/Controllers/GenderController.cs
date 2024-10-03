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
    public class GenderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentWithoutFilteringQuery<Gender>>(request);

            var genders = await _mediator.Send(command);

            var genderDtos = genders.Select(x => new { x.Id, x.GenderName }).ToList();

            return Ok(genderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<Gender>>(id);

            var gender = await _mediator.Send(command);

            if (gender == null)
                return NotFound();

            return Ok(new { gender.Id, gender.GenderName });
        }
    }
}
