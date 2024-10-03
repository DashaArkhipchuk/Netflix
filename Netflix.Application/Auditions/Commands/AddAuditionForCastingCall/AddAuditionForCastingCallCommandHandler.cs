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

namespace Netflix.Application.Auditions.Commands.AddAuditionForCastingCall
{
    internal class AddAuditionForCastingCallCommandHandler : IRequestHandler<AddAuditionForCastingCallCommand, Audition>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICastingCallRepository _castingCallRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IAuditionRepository _auditionRepository;

        public AddAuditionForCastingCallCommandHandler(IClientRepository clientRepository, ICastingCallRepository castingCallRepository, ILocationRepository locationRepository, IAuditionRepository auditionRepository)
        {
            _clientRepository = clientRepository;
            _castingCallRepository = castingCallRepository;
            _locationRepository = locationRepository;
            _auditionRepository = auditionRepository;
        }

        public async Task<Audition> Handle(AddAuditionForCastingCallCommand request, CancellationToken cancellationToken)
        {
            //Step 0: Get casting director if of current client

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

            var errors = new List<ValidationError>();
            if (request.DateFrom < castingCall.SubmissionDue)
            {
                errors.Add(new ValidationError("Date From", "Audition dates must be after the Submission Due date"));
            }
            if (request.DateFrom > castingCall.WorkingDateFrom)
            {
                errors.Add(new ValidationError("Date From", "Audition dates must be before filming dates"));
            }
            if (request.DateTo > castingCall.WorkingDateFrom)
            {
                errors.Add(new ValidationError("Date To", "Audition dates must be before filming dates"));
            }
            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }

            var location = await _locationRepository.GetLocationById(request.LocationId);
            if (location is not Location)
            {
                throw new NotFoundException("Location", "Id", request.LocationId.ToString());
            }

            var audition = new Audition
            {
                IdCastingCall = castingCall.Id,
                LocationId = location.Id,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
            };

            _auditionRepository.Add(audition);

            return await _auditionRepository.GetByIdAsync(audition.Id) ?? throw new Exception("Something went wrong while adding new audition");
        }
    }
}
