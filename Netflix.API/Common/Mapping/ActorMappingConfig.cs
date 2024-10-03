using Mapster;
using Netflix.Application.Actor.Commands.CreateActorProfile;
using Netflix.Application.Actor.Common;
using Netflix.Application.Actor.Queries.ExistsActorProfile;
using Netflix.Application.Actor.Queries.GetActorProfile;
using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.ActorProfile.CreateActorProfile;

namespace Netflix.API.Common.Mapping
{
    public class ActorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, CreateActorProfileRequest), CreateActorProfileCommand>()
                .Map(dest => dest, src => src.Item2)
                .Map(dest => dest.ClientId, src => src.Item1);

            config.NewConfig<ActorProfileResult, ActorProfileResponse>()
                .Map(dest => dest, src => src.actor);

            config.NewConfig<Guid, GetActorProfileQuery>()
                .Map(dest => dest.ClientId, src => src);

            config.NewConfig<Guid, ExistsActorProfileQuery>()
               .Map(dest => dest.ClientId, src => src);


        }
    }
}
