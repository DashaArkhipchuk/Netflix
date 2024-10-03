using MediatR;
using Netflix.Application.Actor.Common;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Queries.ExistsActorProfile
{
    internal class ExistsActorProfileQueyHandler : IRequestHandler<ExistsActorProfileQuery, bool>
    {
        private readonly IClientRepository _clientRepository;

        public ExistsActorProfileQueyHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<bool> Handle(ExistsActorProfileQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Actor is null)
            {
                return false;
            }

            return true;
        }
    }
}
