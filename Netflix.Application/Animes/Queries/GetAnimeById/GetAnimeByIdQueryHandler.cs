using Netflix.Application.Animes.Common;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.BaseHandler;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Animes.Queries.GetAnimeById
{
    internal class GetAnimeByIdQueryHandler : GetContentByIdQueryHandlerBase<ContentWithTypeAnime>
    {
        public GetAnimeByIdQueryHandler(IContentByTypesRepository contentRepository)
            : base(contentRepository) { }

        protected override string ContentType => "anime";

        protected override ContentWithTypeAnime MapToDto(ContentWithType content)
        {
            return new ContentWithTypeAnime
            {
                Type = content.Type,
                Film = content.Film,
                Series = content.Series
            };
        }
    }
}
