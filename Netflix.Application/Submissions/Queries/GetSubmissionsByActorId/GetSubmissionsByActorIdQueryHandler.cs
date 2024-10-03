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

namespace Netflix.Application.Submissions.Queries.GetSubmissionsByActorId
{
    internal class GetSubmissionsByActorIdQueryHandler : IRequestHandler<GetSubmissionsByActorIdQuery, List<Submission>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public GetSubmissionsByActorIdQueryHandler(IClientRepository clientRepository, ISubmissionRepository submissionRepository)
        {
            _clientRepository = clientRepository;
            _submissionRepository = submissionRepository;
        }

        public async Task<List<Submission>> Handle(GetSubmissionsByActorIdQuery request, CancellationToken cancellationToken)
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

            return await _submissionRepository.GetSubmissionsByActorIdAsync(actorId, request.Skip, request.Take);
        }
    }
}
