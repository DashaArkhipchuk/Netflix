using MediatR;
using Netflix.Application.Actor.Common;
using Netflix.Application.Common.Errors;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Commands.CreateActorProfile
{
    internal class CreateActorProfileCommandHandler : IRequestHandler<CreateActorProfileCommand, ActorProfileResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IActorRepository _actorRepository;

        public CreateActorProfileCommandHandler(IActorRepository actorRepository, IClientRepository clientRepository)
        {
            _actorRepository = actorRepository;
            _clientRepository = clientRepository;
        }

        public async Task<ActorProfileResult> Handle(CreateActorProfileCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Actor is not null)
            {
                throw new AlreadyExistsException("User", "Actor");
            }

            var actor = new Domain.Actor
            {
                ClientId = request.ClientId,
                StageName = request.StageName,
                WorkingLocation = request.WorkingLocation,
                RangeFrom = request.RangeFrom,
                RangeTo = request.RangeTo,
                EthnicAppearance = request.EthnicAppearance,
                Sex = request.Sex
            };
            _actorRepository.Add(actor);

            return new ActorProfileResult(actor);


        }
    }
}
