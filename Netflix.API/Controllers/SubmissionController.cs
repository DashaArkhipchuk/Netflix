using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Common.Helpers;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector;
using Netflix.Application.Common.Content;
using Netflix.Application.Common.Errors;
using Netflix.Application.Submissions.Commands.RemoveSubmission;
using Netflix.Application.Submissions.Commands.SubmitToRole;
using Netflix.Application.Submissions.Queries.GetAllSubmissionsByCastingCall;
using Netflix.Application.Submissions.Queries.GetSubmissionsByActorId;
using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.CastingCalls;
using Netflix.Contracts.Common;
using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.Submissions;
using Netflix.Domain;
using Netflix.Domain.Entities;
using System.Collections.Generic;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SubmissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SubmissionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("SubmitToRole")]
        [Authorize(Policy = "Actor")]
        public async Task<IActionResult> Submit([FromForm] SubmitToRoleRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            if (request.Files.Select(x => x.FileName).Count() != request.Files.Select(x => x.FileName).Distinct().Count())
            {
                throw new Exception("Duplicate file names exist. Check if you are trying to upload the same files");
            }

            var command = new SubmitToRoleCommand(request.CastingId, clientId, request.SubmissionNote, request.Files);

            var submission = await _mediator.Send(command);
            var a = _mapper.Map<ActorProfileResponse>(submission.Actor);
            var c = new CastingCallDto { Id = submission.CastingCall.Id, Title = submission.CastingCall.Title, SubmissionDue = submission.CastingCall.SubmissionDue, ProjectType = submission.CastingCall.ProjectType.ProjectTypeName, RoleType = submission.CastingCall.RoleType.RoleTypeName, PlayableAgeFrom = submission.CastingCall.PlayableAgeFrom, PlayableAgeTo = submission.CastingCall.PlayableAgeTo, Payment = submission.CastingCall.Payment, UnionDetails = submission.CastingCall.UnionDetails, RoleDescription = submission.CastingCall.RoleDescription, IsAnyGenderAccepted = submission.CastingCall.IsAnyGenderAccepted, Locations = submission.CastingCall.Locations.Select(l => $"{l.LocationName}, {l.RegionName}").ToList(), Genders = submission.CastingCall.Genders.Select(g => g.GenderName).ToList() };
            //var dto = new SubmissionExtendedDto {Id = submission.Id, Actor = a, CastingCall = c, SubmissionNote = submission.SubmissionNote, SubmissionMedias = submission.SubmissionMedias.Select(x=>x.MediaUrl).ToArray() };
            var dto = _mapper.Map<SubmissionExtendedDto>(submission);
            return Ok(dto);

        }

        [HttpPost("GetSubmissionByCastingCallId/{castingCallId}/")]
        [Authorize(Policy = "Director")]
        public async Task<IActionResult> GetSubmissionByCastingCall([FromRoute] Guid castingCallId, [FromQuery] GetAllContentRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, Guid, GetAllContentRequest), GetAllSubmissionsByCastingCallQuery>((clientId, castingCallId, request));

            var submissions = await _mediator.Send(command);

            var dtos = submissions.Select(x => _mapper.Map<SubmissionDto>(x)).ToList();

            return Ok(dtos);
        }

        [HttpDelete("RemoveSubmission/{submissionId}/")]
        [Authorize(Policy = "Actor")]
        public async Task<IActionResult> RemoveSubmission([FromRoute] Guid submissionId)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, Guid), RemoveSubmissionCommand>((clientId, submissionId));

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("GetSubmissionsOfAuthenticatedActor")]
        [Authorize(Policy = "Actor")]

        public async Task<IActionResult> GetByActor([FromQuery] GetAllContentRequest request)
        {
            Guid clientId = ClientContextHelper.GetClientId(HttpContext);

            var command = _mapper.Map<(Guid, GetAllContentRequest), GetSubmissionsByActorIdQuery>((clientId, request));

            var submissions = await _mediator.Send(command);

            var dtos = submissions.Select(x => _mapper.Map<SubmissionDto>(x)).ToList();

            return Ok(dtos);
        }
    }
}
