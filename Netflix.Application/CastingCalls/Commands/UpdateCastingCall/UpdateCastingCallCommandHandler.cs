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

namespace Netflix.Application.CastingCalls.Commands.UpdateCastingCall
{
    internal class UpdateCastingCallCommandHandler : IRequestHandler<UpdateCastingCallCommand, CastingCall>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICastingCallRepository _castingCallRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IEthnicAppearanceRepository _ethnicAppearanceRepository;

        public UpdateCastingCallCommandHandler(IClientRepository clientRepository, ICastingCallRepository castingCallRepository, ILocationRepository locationRepository, IGenderRepository genderRepository, IEthnicAppearanceRepository ethnicAppearanceRepository)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
            _locationRepository = locationRepository;
            _genderRepository = genderRepository;
            _ethnicAppearanceRepository = ethnicAppearanceRepository;
        }

        public async Task<CastingCall> Handle(UpdateCastingCallCommand request, CancellationToken cancellationToken)
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

            if (castingCall == null)
            {
                throw new NotFoundException("CastingCall", "Id", request.CastingCallId.ToString());
            }

            if (castingCall.CreatedByDirectorId != castingDirectorId)
            {
                throw new RestrictedAccessException("Only casting director who created this casting call can update it");
            }

            // Step 2: Update the casting call properties
            castingCall.Title = request.Title;
            castingCall.SubmissionDue = request.SubmissionDue;
            castingCall.WorkingDateFrom = request.WorkingDateFrom;
            castingCall.WorkingDateTo = request.WorkingDateTo;
            castingCall.ProjectTypeId = request.ProjectTypeId;
            castingCall.RoleTypeId = request.RoleTypeId;
            castingCall.PlayableAgeFrom = request.PlayableAgeFrom;
            castingCall.PlayableAgeTo = request.PlayableAgeTo;
            castingCall.Payment = request.Payment;
            castingCall.UnionDetails = request.UnionDetails;
            castingCall.RoleDescription = request.RoleDescription;
            castingCall.RateDetails = request.RateDetails;
            castingCall.WorkRequirements = request.WorkRequirements;
            castingCall.WorkInformation = request.WorkInformation;
            castingCall.RequestedMedia = request.RequestedMedia;
            castingCall.InstructionsForSubmissionNote = request.InstructionsForSubmissionNote;
            castingCall.RequestingSubmissionsFrom = request.RequestingSubmissionsFrom;
            castingCall.IsAnyEthnicAppearanceAccepted = request.IsAnyEthnicAppearanceAccepted;
            castingCall.IsAnyGenderAccepted = request.IsAnyGenderAccepted;

            // Step 3: Update related entities (Locations, Genders, EthnicAppearances)

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

            castingCall.Locations.Clear();

            foreach (var location in locations)
            {
                castingCall.Locations.Add(location);
            }


            castingCall.Genders.Clear();
            foreach (var gender in genders)
            {
                castingCall.Genders.Add(gender);
            }

            castingCall.EthnicAppearances.Clear();
            foreach (var ethnicAppearance in ethnicAppearances)
            {
                castingCall.EthnicAppearances.Add(ethnicAppearance);
            }

            var result = await _castingCallRepository.UpdateAsync(castingCall);

            return await _castingCallRepository.GetByIdAsync(castingCall.Id) ?? throw new Exception("Something went wrong while updating casting call");
        }
    }
}
