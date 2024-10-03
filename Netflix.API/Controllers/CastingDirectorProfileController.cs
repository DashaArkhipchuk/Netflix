using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Common.Helpers;
using Netflix.Application.Actor.Queries.ExistsActorProfile;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Application.CastingDirector.Commands.CreateCastingDirectorProfile;
using Netflix.Application.CastingDirector.Queries.ExistsCastingDirectorProfile;
using Netflix.Application.CastingDirector.Queries.GetCastingDirectorProfile;
using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.CastingDirectorProfile.Common;
using Netflix.Contracts.CastingDirectorProfile.CreateCastingDirectorProfile;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Director")]
    public class CastingDirectorProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CastingDirectorProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> Create(CreateCastingDirectorProfileRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, CreateCastingDirectorProfileRequest), CreateCastingDirectorProfileCommand>((clientId, request));

            var directorCreationResult = await _mediator.Send(command);

            return Ok(_mapper.Map<CastingDirectorProfileResponse>(directorCreationResult));
        }

        [HttpGet("GetDirectorProfile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<Guid, GetCastingDirectorProfileQuery>(clientId);

            var directorResult = await _mediator.Send(command);

            if (directorResult is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CastingDirectorProfileResponse>(directorResult));
        }

        [HttpGet("DirectorProfileExists")]
        public async Task<IActionResult> Exists()
        {

            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<Guid, ExistsCastingDirectorProfileQuery>(clientId);

            var directorResult = await _mediator.Send(command);

            return Ok(directorResult);
        }
    }
}
