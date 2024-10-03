using MediatR;
using Netflix.Application.Actor.Queries.ExistsActorProfile;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Queries.ExistsCastingDirectorProfile
{
    internal class ExistsCastingDirectorProfileQueryHandler : IRequestHandler<ExistsCastingDirectorProfileQuery, bool>
    {
        private readonly IClientRepository _clientRepository;

        public ExistsCastingDirectorProfileQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
         
        public async Task<bool> Handle(ExistsCastingDirectorProfileQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.CastingDirector is null)
            {
                return false;
            }

            return true;
        }
    }
}
