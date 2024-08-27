using Mapster;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Animes.Common;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.Content;
using Netflix.Application.Doramas.Common;
using Netflix.Application.Series.Queries.GetAllSeries;
using Netflix.Contracts.Authentication.Common;
using Netflix.Contracts.Common;
using Netflix.Contracts.Content;
using Netflix.Contracts.Series.GetSeriesById;
using Netflix.Domain;

namespace Netflix.API.Common.Mapping
{
    public class ContentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<Film>>()
                .Map(dest => dest.Criteria, src => src.Item2)
                .Map(dest => dest.Skip, src => src.Item1.Skip)
                .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<Series>>()
               .Map(dest => dest.Criteria, src => src.Item2)
               .Map(dest => dest.Skip, src => src.Item1.Skip)
               .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<ContentDtoWithTypeCartoon>>()
               .Map(dest => dest.Criteria, src => src.Item2)
               .Map(dest => dest.Skip, src => src.Item1.Skip)
               .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<ContentDtoWithTypeDorama>>()
               .Map(dest => dest.Criteria, src => src.Item2)
               .Map(dest => dest.Skip, src => src.Item1.Skip)
               .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<(GetAllContentRequest, Criteria), GetAllContentQuery<ContentDtoWithTypeAnime>>()
              .Map(dest => dest.Criteria, src => src.Item2)
              .Map(dest => dest.Skip, src => src.Item1.Skip)
              .Map(dest => dest.Take, src => src.Item1.Take);

            config.NewConfig<Guid, GetContentByIdQuery<Film>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<Guid, GetContentByIdQuery<Series>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<Guid, GetContentByIdQuery<ContentWithTypeCartoon>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<Guid, GetContentByIdQuery<ContentWithTypeDorama>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<Guid, GetContentByIdQuery<ContentWithTypeAnime>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<ContentWithTypeCartoon, ContentWithTypeResponse>()
                .Map(dest=>dest.Film, src => src.Film)
                .Map(dest=>dest.Series, src=> src.Series)
                .Map(dest=>dest.Type, src => src.Type);

            config.NewConfig<ContentWithTypeDorama, ContentWithTypeResponse>()
                .Map(dest => dest.Film, src => src.Film)
                .Map(dest => dest.Series, src => src.Series)
                .Map(dest => dest.Type, src => src.Type);

            config.NewConfig<ContentWithTypeAnime, ContentWithTypeResponse>()
                .Map(dest => dest.Film, src => src.Film)
                .Map(dest => dest.Series, src => src.Series)
                .Map(dest => dest.Type, src => src.Type);


        }
    }
}
