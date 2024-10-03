using MediatR;
using Netflix.Application.CastingDirector.Common;
using Netflix.Application.Common.Errors;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;

namespace Netflix.Application.CastingDirector.Commands.CreateCastingDirectorProfile
{
    internal class CreateCastingDirectorProfileCommandHandler : IRequestHandler<CreateCastingDirectorProfileCommand, CastingDirectorProfileResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICastingDirectorRepository _directorRepository;
        private readonly ICastingDirectorTypeRepository _directorTypeRepository;

        public CreateCastingDirectorProfileCommandHandler(ICastingDirectorRepository actorRepository, IClientRepository clientRepository, ICastingDirectorTypeRepository castingDirectorTypeRepository)
        {
            _directorRepository = actorRepository;
            _clientRepository = clientRepository;
            _directorTypeRepository = castingDirectorTypeRepository;
        }

        public async Task<CastingDirectorProfileResult> Handle(CreateCastingDirectorProfileCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.CastingDirector is not null)
            {
                throw new AlreadyExistsException("User", "Casting Director");
            }

            if (await _directorTypeRepository.GetTypeById(request.TypeId) is null)
            {
                throw new NotFoundException("Casting Director Profile Type", "Id", request.TypeId.ToString());
            }

            var castingDirector = new Domain.Entities.CastingDirector
            {
                //Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                FullName = request.FullName,
                TypeId = request.TypeId,
                CompanyName = request.CompanyName,
                Website = request.Website,
                Address = request.Address,
                RegionName = request.RegionName,
                PhoneNumberWithCountryCode = request.PhoneNumberWithCountryCode,
                Email = request.Email
            };

            _directorRepository.Add(castingDirector);

            var director = await _directorRepository.GetProfileByClientId(request.ClientId);

            if (director is null)
            {
                throw new Exception("Error occured while creating that profile");
            }

            return new CastingDirectorProfileResult(director);
        }
    }
}
