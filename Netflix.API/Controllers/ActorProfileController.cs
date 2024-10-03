using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Common.Helpers;
using Netflix.Application.Actor.Commands.CreateActorProfile;
using Netflix.Application.Actor.Queries.ExistsActorProfile;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Application.Authentication.Queries;
using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.ActorProfile.CreateActorProfile;
using System.ComponentModel;
using System.Security.Claims;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Actor")]
    public class ActorProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ActorProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> Create(CreateActorProfileRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, CreateActorProfileRequest), CreateActorProfileCommand>((clientId, request));
            
            var actorCreationResult = await _mediator.Send(command);

            return Ok(_mapper.Map<ActorProfileResponse>(actorCreationResult));
        }

        [HttpGet("GetActorProfile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);
            //var claimsIdentity = HttpContext?.User?.Identity as ClaimsIdentity;
            //var strclientId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //Guid clientId;
            //if (strclientId is not null)
            //{
            //    clientId = new Guid(strclientId);
            //}
            //else
            //{
            //    clientId = Guid.Empty;
            //    throw new Exception("Failed to parse user id from token");
            //}

            var command = _mapper.Map<Guid, GetActorProfileQuery>(clientId);

            var actorResult = await _mediator.Send(command);

            return Ok(_mapper.Map<ActorProfileResponse>(actorResult));
        }

        [HttpGet("ActorProfileExists")]
        public async Task<IActionResult> Exists()
        {
            
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<Guid, ExistsActorProfileQuery>(clientId);

            var actorResult = await _mediator.Send(command);

            return Ok(actorResult);
        }
    }
}
