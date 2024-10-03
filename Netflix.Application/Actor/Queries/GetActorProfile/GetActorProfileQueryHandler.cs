using MediatR;
using Netflix.Application.Actor.Commands.CreateActorProfile;
using Netflix.Application.Actor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;

namespace Netflix.Application.Actor.Queries.GetActorProfile
{
    internal class GetActorProfileQueryHandler : IRequestHandler<GetActorProfileQuery, ActorProfileResult>
    {
        private readonly IClientRepository _clientRepository;

        public GetActorProfileQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ActorProfileResult> Handle(GetActorProfileQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Actor is null)
            {
                throw new NotFoundException("Actor Profile", "Client Id", request.ClientId.ToString());
            }

            return new ActorProfileResult ( client.Actor );
        }
    }
}
