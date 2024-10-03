using MediatR;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Application.Common.Services;

namespace Netflix.Application.Submissions.Commands.RemoveSubmission
{
    internal class RemoveSubmissionCommandHandler : IRequestHandler<RemoveSubmissionCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISubmissionRepository _submissionRepository;

        private readonly ICloudStorageService _cloudStorageService;

        public RemoveSubmissionCommandHandler(IClientRepository clientRepository, ISubmissionRepository submissionRepository, ICloudStorageService cloudStorageService)
        {
            _clientRepository = clientRepository;
            _submissionRepository = submissionRepository;

            _cloudStorageService = cloudStorageService;
        }

        public async Task<bool> Handle(RemoveSubmissionCommand request, CancellationToken cancellationToken)
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

            var submission = await _submissionRepository.GetSubmissionByIdAsync(request.SubmissionId);

            if (submission is null)
            {
                throw new NotFoundException("Submission", "Id", request.SubmissionId.ToString());
            }

            if (submission.ActorId != actorId)
            {
                throw new RestrictedAccessException("Only actor who created this submission can remove it");
            }

            // REMOVE SUBMISSION MEDIA FROM CLOUD

            foreach (var media in submission.SubmissionMedias)
            {
                var filename = await _cloudStorageService.RemoveBlobByUrlAsync(media.MediaUrl);
            }

            return await _submissionRepository.Remove(request.SubmissionId);

        }
    }
}
