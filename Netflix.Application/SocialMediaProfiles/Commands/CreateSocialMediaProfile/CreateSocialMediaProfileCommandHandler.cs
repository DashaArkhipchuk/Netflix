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

namespace Netflix.Application.SocialMediaProfiles.Commands.CreateSocialMediaProfile
{
    internal class CreateSocialMediaProfileCommandHandler : IRequestHandler<CreateSocialMediaProfileCommand, SocialMediaProfile>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISocialMediaProfileRepository _profileRepository;

        public CreateSocialMediaProfileCommandHandler(ISocialMediaRepository profileRepository, IClientRepository clientRepository)
        {
            _profileRepository = profileRepository;
            _clientRepository = clientRepository;
        }
        public async Task<SocialMediaProfile> Handle(CreateSocialMediaProfileCommand request, CancellationToken cancellationToken)
        {
            if (await _clientRepository.GetClientByIdAsync(request.ClientId) is not Client client)
            {
                throw new NotFoundException("User", "Id", request.ClientId.ToString());
            }

            if (client.Profile is not null)
            {
                throw new AlreadyExistsException("User", "Social media profile");
            }

            if (await _profileRepository.NicknameExistsAsync(request.Nickname, excludeProfileId: null))
            {
                throw new DuplicateIdentifierException("The given nickname is already taken");
            }

            var profile = new SocialMediaProfile
            {
                ClientId = client.Id,
                DisplayName = request.DisplayName,
                Nickname = request.Nickname,
                AvatarUrl = request.AvatarUrl,
                BackgroundUrl = request.BackgroundUrl,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                City = request.City,
                Gender = request.Gender,
                AboutMeText = request.AboutMeText
            };

            await _profileRepository.AddAsync(profile);

            return profile;


        }
    }
}
