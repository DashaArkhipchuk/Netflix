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

            config.NewConfig<(GetAllContentRequest, CriteriaNews), GetAllNewsQuery>()
                .Map(dest => dest.Skip, src => src.Item1.Skip)
                .Map(dest => dest.Take, src => src.Item1.Take)
                .Map(dest => dest.Criteria, src => src.Item2);

            config.NewConfig<Guid, GetContentByIdQuery<News>>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<News, NewsExtendedDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Author, src => $"{src.Author.Name} {src.Author.Surname}")
                .Map(dest => dest.Type, src => src.Type.Name);


        }
    }
}
