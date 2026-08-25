using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Actor.Commands.CreateActorProfile;
using Netflix.Application.Common.Content;
using Netflix.Application.News.Queries.GetAllNews;
using Netflix.Contracts.ActorProfile.CreateActorProfile;
using Netflix.Contracts.Common;
using Netflix.Contracts.News;
using Netflix.Domain.Entities;

namespace Netflix.API.Common.Mapping
{
    public class NewsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAllContentRequest, GetAllNewsQuery>()
                .Map(dest => dest.Skip, src => src.Skip)
                .Map(dest => dest.Take, src => src.Take);

            config.NewConfig<(GetAllContentRequest Request, CriteriaNews Criteria), GetAllNewsQuery>()
                .Map(dest => dest.Skip, src => src.Request.Skip)
                .Map(dest => dest.Take, src => src.Request.Take)
                .Map(dest => dest.Criteria, src => src.Criteria);

            config.NewConfig<Guid, GetContentByIdQuery<News>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<News, NewsExtendedDto>()
                .Map(dest => dest.Authors, src => src.Authors.Select(a => $"{a.Name} {a.Surname}"))
                .Map(dest => dest.Type, src => src.Type.Name)
                .Map(dest => dest.ImageURL, src => src.ImageUrl);

            config.NewConfig<News, NewsDto>()
                .Map(dest => dest.Authors, src => src.Authors.Select(a => $"{a.Name} {a.Surname}"))
                .Map(dest => dest.Type, src => src.Type.Name)
                .Map(dest => dest.ImageURL, src => src.ImageUrl);

            config.NewConfig<News, ShortNewsDto>()
                .Map(dest => dest.Authors, src => src.Authors.Select(a => $"{a.Name} {a.Surname}"))
                .Map(dest => dest.ImageURL, src => src.ImageUrl);
        }
    }
}
