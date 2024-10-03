using MediatR;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Auditions.Commands.RemoveAuditionForCastingCall
{
    internal class RemoveAuditionForCastingCallCommandHandler : IRequestHandler<RemoveAuditionForCastingCallCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAuditionRepository _auditionRepository;

        public RemoveAuditionForCastingCallCommandHandler(IAuditionRepository auditionRepository, IClientRepository clientRepository)
        {
            _auditionRepository = auditionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<bool> Handle(RemoveAuditionForCastingCallCommand request, CancellationToken cancellationToken)
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

            var audition = await _auditionRepository.GetByIdAsync(request.AuditionId);

            if (audition is null)
            {
                throw new NotFoundException("Audition", "Id", request.AuditionId.ToString());
            }

            if (audition.CastingCall.CreatedByDirectorId != castingDirectorId)
            {
                throw new RestrictedAccessException("Only casting director who created this casting call can update its information");
            }

            return await _auditionRepository.Remove(audition.Id);

        }
    }
}
