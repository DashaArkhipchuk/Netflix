using MediatR;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector;
using Netflix.Application.Common.Errors;
using Netflix.Domain;
using Netflix.Domain.DTOs;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByActor
{
    internal class GetAllCastingCallsByActorQueryHandler : IRequestHandler<GetAllCastingCallsByActorQuery, PagedResult<CastingCall>>
    {
        IClientRepository _clientRepository;
        ICastingCallRepository _castingCallRepository;

        public GetAllCastingCallsByActorQueryHandler(IClientRepository clientRepository, ICastingCallRepository castingCallRepository, ISubmissionRepository submissionRepository)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
        }

        public async Task<PagedResult<CastingCall>> Handle(GetAllCastingCallsByActorQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Actor is null)
            {
                throw new NotFoundException("Actor Profile", "Client Id", request.ClientId.ToString());
            }

            var actorId = client.Actor.Id;

            return await _castingCallRepository.GetCastingCallsByActorIdAsync(actorId, request.Skip, request.Take, request.Criteria?.Locations ?? new List<string>(), request.Criteria?.PlayableAgeRanges ?? new List<string>(), request.Criteria?.ProjectTypes ?? new List<string>(), request.Criteria?.RoleTypes ?? new List<string>()); 
        }
    }
}
