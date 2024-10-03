using MediatR;
using Netflix.Application.Common.Errors;
using Netflix.Domain;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector
{
    internal class GetAllCastingCallsByDirectorQueryHandler : IRequestHandler<GetAllCastingCallsByDirectorQuery, List<CastingCall>>
    {
        IClientRepository _clientRepository;
        ICastingCallRepository _castingCallRepository;

        public GetAllCastingCallsByDirectorQueryHandler(IClientRepository clientRepository, ICastingCallRepository castingCallRepository)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
        }

        public async Task<List<CastingCall>> Handle(GetAllCastingCallsByDirectorQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.CastingDirector is null)
            {
                throw new NotFoundException("Casting Director Profile", "Client Id", request.ClientId.ToString());
            }

            var castingDirectorId = client.CastingDirector.Id;

            return await _castingCallRepository.GetAllAsync(request.Skip, request.Take, request.Criteria?.Locations ?? new List<string>(), request.Criteria?.PlayableAgeRanges ?? new List<string>(), request.Criteria?.ProjectTypes ?? new List<string>(), request.Criteria?.RoleTypes ?? new List<string>(), castingDirectorId);
        }
    }
}
