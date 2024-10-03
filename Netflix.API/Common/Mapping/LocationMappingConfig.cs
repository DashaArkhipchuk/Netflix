using Mapster;
using Netflix.Application.Regions.Queries.GetLocationByRegionName;
using Netflix.Contracts.Common;

namespace Netflix.API.Common.Mapping
{
    public class LocationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(string, GetAllContentRequest), GetLocationsByRegionNameQuery>()
                .Map(dest => dest.RegionName, src => src.Item1)
                .Map(dest => dest.Skip, src => src.Item2.Skip)
                .Map(dest => dest.Take, src => src.Item2.Take);
        }
    }
}
