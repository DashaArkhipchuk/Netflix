using MediatR;
using Netflix.Application.Actor.Common;
using Netflix.Application.Common.Errors;
using Netflix.Application.Common.Services;
using Netflix.Domain;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Commands.SubmitToRole
{
    internal class SubmitToRoleCommandHandler : IRequestHandler<SubmitToRoleCommand, Submission>
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICastingCallRepository _castingCallRepository;
        private readonly ICloudStorageService _cloudStorageService;

        public SubmitToRoleCommandHandler(ISubmissionRepository submissionRepository, IClientRepository clientRepository, ICastingCallRepository castingCallRepository, ICloudStorageService cloudStorageService)
        {
            _submissionRepository = submissionRepository;
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
            _cloudStorageService = cloudStorageService;
        }


        public async Task<Submission> Handle(SubmitToRoleCommand request, CancellationToken cancellationToken)
        {
            // 0. Check if castingCall with id exists

            if (!_castingCallRepository.ExistsCastingCallById(request.CastingId))
            {
                throw new NotFoundException("Casting call", "Id", request.CastingId.ToString());
            }

            // 0. Get Actor profile if exists

            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Actor is null)
            {
                throw new NotFoundException("Actor Profile", "Client Id", request.ClientId.ToString());
            }

            var actorId= client.Actor.Id;

            var callSubmissions = await _submissionRepository.GetAllSubmissionsByCastingCallAsync(request.CastingId);
            if (callSubmissions.Where(x=>x.ActorId == actorId).Count() > 0)
            {
                throw new AlreadyExistsException("Submission", "Actor id");
            }

            // 1. Upload files to cloud and get URIs
            var mediaUris = new List<string>();

            foreach (var file in request.Files)
            {
                string uri = await _cloudStorageService.UploadBlobAsync(file);
                mediaUris.Add(uri);
            }


            // 2. Create the submission entity
            var submission = new Submission
            {
                Id = Guid.NewGuid(),
                CastingId = request.CastingId,
                ActorId = client.Actor.Id,
                SubmissionNote = request.SubmissionNote
            };

            // 3. Add media URIs to the submission
            foreach (var mediaUrl in mediaUris)
            {
                submission.SubmissionMedias.Add(new SubmissionMedia
                {
                    Id = Guid.NewGuid(),
                    SubmissionId = submission.Id,
                    MediaUrl = mediaUrl
                });
            }

            _submissionRepository.Add(submission);

            return await _submissionRepository.GetSubmissionByIdAsync(submission.Id) ?? throw new Exception("Something went wrong");
        }
    }
}
