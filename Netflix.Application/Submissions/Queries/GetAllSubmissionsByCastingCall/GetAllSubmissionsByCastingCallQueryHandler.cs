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

namespace Netflix.Application.Submissions.Queries.GetAllSubmissionsByCastingCall
{
    internal class GetAllSubmissionsByCastingCallQueryHandler : IRequestHandler<GetAllSubmissionsByCastingCallQuery, List<Submission>>
    {
        IClientRepository _clientRepository;
        ICastingCallRepository _castingCallRepository;
        ISubmissionRepository _submissionRepository;

        public GetAllSubmissionsByCastingCallQueryHandler(IClientRepository clientRepository,ICastingCallRepository castingCallRepository, ISubmissionRepository submissionRepository)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
            _submissionRepository = submissionRepository;
        }

        public async Task<List<Submission>> Handle(GetAllSubmissionsByCastingCallQuery request, CancellationToken cancellationToken)
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

            var castingCall = await _castingCallRepository.GetByIdAsync(request.CastingCallId);

            if (castingCall is null)
            {
                throw new NotFoundException("Casting call", "Id", request.CastingCallId.ToString());
            }

            if (castingCall.CreatedByDirectorId is null || castingCall.CreatedByDirectorId != castingDirectorId)
            {
                throw new RestrictedAccessException("You cannot view this casting call`s submissions");
            }

            return await _submissionRepository.GetAllSubmissionsByCastingCallAsync(request.CastingCallId, request.Skip, request.Take);
        }
    }
}
