using Mapster;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Series.Queries.GetAllSeries;
using Netflix.Contracts.Authentication.Common;
using Netflix.Contracts.Common;
using Netflix.Domain;

namespace Netflix.API.Common.Mapping
{
    public class ContentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<GetAllContentRequest, GetAllFilmsQuery>()
            //    .Map(dest => dest.Criteria, src => src.Criteria)
            //    .Map(dest => dest.Skip, src => src.Skip)
            //    .Map(dest => dest.Take, src => src.Take);

            //config.NewConfig<GetAllContentRequest, GetAllSeriesQuery>()
            //    .Map(dest => dest.Criteria, src => src.Criteria)
            //    .Map(dest => dest.Skip, src => src.Skip)
            //    .Map(dest => dest.Take, src => src.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<Film>>()
                .Map(dest => dest.Criteria, src => src.Item2)
                .Map(dest => dest.Skip, src => src.Item1.Skip)
                .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<Series>>()
               .Map(dest => dest.Criteria, src => src.Item2)
               .Map(dest => dest.Skip, src => src.Item1.Skip)
               .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<Guid, GetContentByIdQuery<Film>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<Guid, GetContentByIdQuery<Series>>()
                .Map(dest => dest.Id, src => src);


        }
    }
}
