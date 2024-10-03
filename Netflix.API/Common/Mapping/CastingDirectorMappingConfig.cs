using Mapster;
using Netflix.Application.CastingDirector.Commands.CreateCastingDirectorProfile;
using Netflix.Application.CastingDirector.Common;
using Netflix.Application.CastingDirector.Queries.ExistsCastingDirectorProfile;
using Netflix.Application.CastingDirector.Queries.GetCastingDirectorProfile;
using Netflix.Contracts.CastingDirectorProfile.Common;
using Netflix.Contracts.CastingDirectorProfile.CreateCastingDirectorProfile;

namespace Netflix.API.Common.Mapping
{
    public class CastingDirectorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, CreateCastingDirectorProfileRequest), CreateCastingDirectorProfileCommand>()
                .Map(dest => dest, src => src.Item2)
                .Map(dest => dest.ClientId, src => src.Item1);

            config.NewConfig<CastingDirectorProfileResult, CastingDirectorProfileResponse>()
                .Map(dest => dest, src => src.director)
                .Map(dest => dest.Type, src => src.director.CastingDirectorType.Name);

            config.NewConfig<Guid, GetCastingDirectorProfileQuery>()
                .Map(dest => dest.ClientId, src => src);

            config.NewConfig<Guid, ExistsCastingDirectorProfileQuery>()
               .Map(dest => dest.ClientId, src => src);
        }
    }
}
