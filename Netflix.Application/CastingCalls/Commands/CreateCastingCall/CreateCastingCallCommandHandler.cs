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

namespace Netflix.Application.CastingCalls.Commands.CreateCastingCall
{
    internal class CreateCastingCallCommandHandler : IRequestHandler<CreateCastingCallCommand, CastingCall>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IRoleTypeRepository _roleTypeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IEthnicAppearanceRepository _ethnicAppearanceRepository;
        private readonly ICastingCallRepository _castingCallRepository;

        public CreateCastingCallCommandHandler(IClientRepository clientRepository, IProjectTypeRepository projectTypeRepository, IRoleTypeRepository roleTypeRepository, ILocationRepository locationRepository, IGenderRepository genderRepository,  IEthnicAppearanceRepository ethnicAppearanceRepository, ICastingCallRepository castingCallRepository)
        {
            _clientRepository = clientRepository;
            _projectTypeRepository = projectTypeRepository;
            _roleTypeRepository = roleTypeRepository;
            _locationRepository = locationRepository;
            _genderRepository = genderRepository;
            _ethnicAppearanceRepository = ethnicAppearanceRepository;
            _castingCallRepository = castingCallRepository;
        }

        public async Task<CastingCall> Handle(CreateCastingCallCommand request, CancellationToken cancellationToken)
        {
            //Step 0: Get casting director if of current client

            if (await _clientRepository.GetClientByIdAsync(request.CreatedByDirectorId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.CreatedByDirectorId.ToString());
            }

            if (client.CastingDirector is null)
            {
                throw new NotFoundException("Casting Director Profile", "Client Id", request.CreatedByDirectorId.ToString());
            }

            var castingDirectorId=client.CastingDirector.Id;




            // Step 1: Get the project type and role type
            var projectType = await _projectTypeRepository.GetTypeById(request.ProjectTypeId);
            if (projectType is not ProjectType)
            {
                throw new NotFoundException("Project type", "Id", request.ProjectTypeId.ToString());
            }

            var roleType = await _roleTypeRepository.GetTypeById(request.RoleTypeId);
            if (roleType is not RoleType)
            {
                throw new NotFoundException("Role type", "Id", request.RoleTypeId.ToString());
            }

            // Step 2: Get locations sequentially
            var locations = new List<Location>();
            foreach (var id in request.LocationIds)
            {
                var location = await _locationRepository.GetLocationById(id);
                if (location is not null)
                {
                    locations.Add(location);
                }
            }

            // Step 3: Get genders sequentially
            var genders = new List<Gender>();
            foreach (var id in request.GenderIds)
            {
                var gender = await _genderRepository.GetTypeById(id);
                if (gender is not null)
                {
                    genders.Add(gender);
                }
            }

            // Step 4: Get ethnic appearances sequentially
            var ethnicAppearances = new List<EthnicAppearance>();
            foreach (var id in request.EthnicAppearanceIds)
            {
                var ethnicAppearance = await _ethnicAppearanceRepository.GetTypeById(id);
                if (ethnicAppearance is not null)
                {
                    ethnicAppearances.Add(ethnicAppearance);
                }
            }

                // Step 3: Create the CastingCall entity
                var castingCall = new CastingCall
            {
                Title = request.Title,
                SubmissionDue = request.SubmissionDue,
                WorkingDateFrom = request.WorkingDateFrom,
                WorkingDateTo = request.WorkingDateTo,
                PostedDate = DateTime.UtcNow,
                ProjectTypeId = request.ProjectTypeId,
                ProjectType = projectType,
                RoleTypeId = request.RoleTypeId,
                RoleType = roleType,
                PlayableAgeFrom = request.PlayableAgeFrom,
                PlayableAgeTo = request.PlayableAgeTo,
                Payment = request.Payment,
                UnionDetails = request.UnionDetails,
                RoleDescription = request.RoleDescription,
                RateDetails = request.RateDetails,
                WorkRequirements = request.WorkRequirements,
                WorkInformation = request.WorkInformation,
                RequestedMedia = request.RequestedMedia,
                InstructionsForSubmissionNote = request.InstructionsForSubmissionNote,
                RequestingSubmissionsFrom = request.RequestingSubmissionsFrom,
                IsAnyEthnicAppearanceAccepted = request.IsAnyEthnicAppearanceAccepted,
                IsAnyGenderAccepted = request.IsAnyGenderAccepted,
                CreatedByDirectorId = castingDirectorId,
                Locations = locations,
                Genders = genders,
                EthnicAppearances = ethnicAppearances
            };

            // Step 4: Add the new CastingCall entity to the db
            _castingCallRepository.Add(castingCall);

            // Return the newly created CastingCall
            return await _castingCallRepository.GetByIdAsync(castingCall.Id) ?? throw new Exception("Something went wrong while adding new casting call");
        }
    }
}
