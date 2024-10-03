using MediatR;
using Netflix.Application.Actor.Common;
using Netflix.Application.CastingDirector.Common;
using Netflix.Application.Common.Errors;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Queries.GetCastingDirectorProfile
{
    internal class GetCastingDirectorProfileQueryHandler : IRequestHandler<GetCastingDirectorProfileQuery, CastingDirectorProfileResult?>
    {
        private readonly IClientRepository _clientRepository;

        public GetCastingDirectorProfileQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<CastingDirectorProfileResult?> Handle(GetCastingDirectorProfileQuery request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.CastingDirector is null)
            {
                throw new NotFoundException("Casting Director Profile", "Client Id", request.ClientId.ToString());
            }

            return new CastingDirectorProfileResult(client.CastingDirector);
        }
    }
}
