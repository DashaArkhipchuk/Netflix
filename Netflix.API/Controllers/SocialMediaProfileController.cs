using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Common.Helpers;
using Netflix.Application.CastingCalls.Commands.CreateCastingCall;
using Netflix.Contracts.CastingCalls;
using Netflix.Contracts.CastingCalls.CreateCastingCall;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SocialMediaProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCastingCallRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, CreateSocialMediaProfileRequest), CreateSocialMediaProfileCommand>((clientId, request));

            var  = await _mediator.Send(command);

            return Ok(_mapper.Map<CastingCallExtendedDto>(castingCall));
        }

    }
}
