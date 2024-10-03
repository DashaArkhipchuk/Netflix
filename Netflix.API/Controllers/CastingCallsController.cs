using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Netflix.API.Common.Helpers;
using Netflix.Application.Auditions.Commands.AddAuditionForCastingCall;
using Netflix.Application.Auditions.Commands.RemoveAuditionForCastingCall;
using Netflix.Application.CastingCalls.Commands.CreateCastingCall;
using Netflix.Application.CastingCalls.Commands.RemoveCastingCall;
using Netflix.Application.CastingCalls.Commands.UpdateCastingCall;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector;
using Netflix.Application.Common.Content;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Application.Submissions.Commands.RemoveSubmission;
using Netflix.Contracts.Auditions;
using Netflix.Contracts.CastingCalls;
using Netflix.Contracts.CastingCalls.CreateCastingCall;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Domain;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastingCallsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CastingCallsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromBody] CastingCriteria castingCriteria)
        {
            var command = _mapper.Map<(GetAllContentRequest, CastingCriteria), GetAllCastingCallsQuery>((request, castingCriteria));

            var calls = await _mediator.Send(command);

            var callDtos = calls.Select(x =>
            new CastingCallDto { Id = x.Id, Title = x.Title, SubmissionDue = x.SubmissionDue, ProjectType = x.ProjectType.ProjectTypeName, RoleType = x.RoleType.RoleTypeName, PlayableAgeFrom = x.PlayableAgeFrom, PlayableAgeTo = x.PlayableAgeTo, Payment = x.Payment, UnionDetails = x.UnionDetails, RoleDescription = x.RoleDescription, IsAnyGenderAccepted=x.IsAnyGenderAccepted, Locations = x.Locations.Select(l => $"{l.LocationName}, {l.RegionName}").ToList(), Genders = x.Genders.Select(g => g.GenderName).ToList() });

            return Ok(callDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<CastingCall>>(id);

            var castingCall = await _mediator.Send(command);

            if (castingCall == null)
                return NotFound();

            return Ok(_mapper.Map<CastingCallExtendedDto>(castingCall));
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCastingCallRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, CreateCastingCallRequest), CreateCastingCallCommand>((clientId, request));

            var castingCall = await _mediator.Send(command);

            return Ok(_mapper.Map<CastingCallExtendedDto>(castingCall));
        }

        [HttpPost("GetCastingCallsByAuthenticatedCastingDirectorId")]
        public async Task<IActionResult> GetByDirector([FromQuery] GetAllContentRequest request, [FromBody] CastingCriteria castingCriteria)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, GetAllContentRequest, CastingCriteria), GetAllCastingCallsByDirectorQuery>((clientId, request, castingCriteria));

            var calls = await _mediator.Send(command);

            var callDtos = calls.Select(x =>
            new CastingCallDto { Id = x.Id, Title = x.Title, SubmissionDue = x.SubmissionDue, ProjectType = x.ProjectType.ProjectTypeName, RoleType = x.RoleType.RoleTypeName, PlayableAgeFrom = x.PlayableAgeFrom, PlayableAgeTo = x.PlayableAgeTo, Payment = x.Payment, UnionDetails = x.UnionDetails, RoleDescription = x.RoleDescription, IsAnyGenderAccepted = x.IsAnyGenderAccepted, Locations = x.Locations.Select(l => $"{l.LocationName}, {l.RegionName}").ToList(), Genders = x.Genders.Select(g => g.GenderName).ToList() });

            return Ok(callDtos);
        }

        [HttpDelete("Remove/{castingCallId}/")]
        public async Task<IActionResult> RemoveCastingCall([FromRoute] Guid castingCallId)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, Guid), RemoveCastingCallCommand>((clientId, castingCallId));

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("Update/{castingCallId}/")]
        public async Task<IActionResult> UpdateCastingCall([FromRoute] Guid castingCallId, [FromBody] CreateCastingCallRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, Guid, CreateCastingCallRequest), UpdateCastingCallCommand>((clientId, castingCallId, request));

            var castingCall = await _mediator.Send(command);


            return Ok(_mapper.Map<CastingCallExtendedDto>(castingCall));
        }

        [HttpPost("AddAudition")]
        public async Task<IActionResult> AddAudition(AddAuditionsForCastingCallRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, AddAuditionsForCastingCallRequest), AddAuditionForCastingCallCommand>((clientId, request));

            var audition = await _mediator.Send(command);

            return Ok(_mapper.Map<AuditionExtendedDto>(audition));

        }

        [HttpPost("RemoveAudition/{auditionId}/")]
        public async Task<IActionResult> RemoveAudition([FromRoute] Guid auditionId)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, Guid), RemoveAuditionForCastingCallCommand>((clientId, auditionId));

            var result = await _mediator.Send(command);

            return Ok(result);

        }
    }
}
