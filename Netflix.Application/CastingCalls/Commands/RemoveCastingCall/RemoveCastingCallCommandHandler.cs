using MediatR;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;
using Netflix.Application.Common.Services;
using System.Net.WebSockets;

namespace Netflix.Application.CastingCalls.Commands.RemoveCastingCall
{
    internal class RemoveCastingCallCommandHandler : IRequestHandler<RemoveCastingCallCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICastingCallRepository _castingCallRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ICloudStorageService _cloudStorageService;
        
        public RemoveCastingCallCommandHandler(IClientRepository clientRepository, ICastingCallRepository castingCallRepository, ISubmissionRepository submissionRepository, ICloudStorageService cloudStorageService)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
            _submissionRepository = submissionRepository;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<bool> Handle(RemoveCastingCallCommand request, CancellationToken cancellationToken)
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

            var castingCall = await _castingCallRepository.GetByIdAsync(request.CastingId);

            if (castingCall is null)
            {
                throw new NotFoundException("Casting call", "Id", request.CastingId.ToString());
            }

            if (castingCall.CreatedByDirectorId != castingDirectorId )
            {
                throw new RestrictedAccessException("Only casting director who created this casting call can remove it");
            }

            var submissions = castingCall.Submissions.ToList();

            for (int i = 0; i < submissions.Count; i++)
            {
                var submission = submissions[i];

                for (int j = 0; j < submission.SubmissionMedias.Count; j++)
                {
                    var medias = submission.SubmissionMedias.ToList();
                    var media = medias[j];

                    string str = await _cloudStorageService.RemoveBlobByUrlAsync(media.MediaUrl);
                    if (str is null)
                    {
                        throw new Exception($"Failed to delete file: {media.MediaUrl}");
                    }
                }

                var resultSubmission = await _submissionRepository.Remove(submission.Id);
                if (!resultSubmission)
                {
                    throw new Exception($"Failed to delete submission with id {submission.Id}");
                }
            }

            return await _castingCallRepository.Remove(castingCall.Id);
        }
    }
}
