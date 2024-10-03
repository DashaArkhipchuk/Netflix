using Mapster;
using Netflix.Application.Auditions.Commands.AddAuditionForCastingCall;
using Netflix.Application.Auditions.Commands.RemoveAuditionForCastingCall;
using Netflix.Contracts.Auditions;
using Netflix.Domain.Entities;

namespace Netflix.API.Common.Mapping
{
    public class AuditionsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid, AddAuditionsForCastingCallRequest), AddAuditionForCastingCallCommand>()
                .Map(dest => dest, src => src.Item2)
                .Map(dest => dest.ClientId, src => src.Item1);

            config.NewConfig<Audition, AuditionExtendedDto>()
                .Map(dest => dest.DateFrom, src => src.DateFrom)
                .Map(dest => dest.DateTo, src => src.DateTo)
                .Map(dest => dest.CastingCallTitle, src => src.CastingCall.Title)
                .Map(dest => dest.Location, src => src.Location.LocationName + ", " + src.Location.RegionName);

            config.NewConfig<(Guid, Guid), RemoveAuditionForCastingCallCommand>()
                .Map(dest => dest.ClientId, src => src.Item1)
                .Map(dest => dest.AuditionId, src => src.Item2);
        }
    }
}
