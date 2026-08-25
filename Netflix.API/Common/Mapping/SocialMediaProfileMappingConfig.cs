using Mapster;
using Netflix.Application.Actor.Commands.CreateActorProfile;
using Netflix.Application.Actor.Common;
using Netflix.Application.Actor.Queries.ExistsActorProfile;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Application.SocialMediaProfiles.Commands.CreateSocialMediaProfile;
using Netflix.Application.SocialMediaProfiles.Common;
using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.ActorProfile.CreateActorProfile;
using Netflix.Contracts.SocialMediaProfile.Common;
using Netflix.Contracts.SocialMediaProfile.CreateSocialMediaProfile;

namespace Netflix.API.Common.Mapping
{
    public class SocialMediaProfileMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, CreateSocialMediaProfileRequest), CreateSocialMediaProfileCommand>()
                .Map(dest => dest, src => src.Item2)
                .Map(dest => dest.ClientId, src => src.Item1);

            config.NewConfig<SocialMediaProfileResult, SocialMediaProfileResponse>()
                .Map(dest => dest, src => src.profile);


        }
    }
}
