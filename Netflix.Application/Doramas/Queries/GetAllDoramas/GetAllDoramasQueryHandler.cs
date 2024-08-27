using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.BaseHandler;
using Netflix.Application.Doramas.Common;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Netflix.Application.Doramas.Queries.GetAllDoramas
{
    internal class GetAllDoramasQueryHandler : GetAllContentQueryHandlerBase<ContentDtoWithTypeDorama>
    {
        public GetAllDoramasQueryHandler(IContentByTypesRepository contentRepository) : base(contentRepository) { }
        protected override string ContentType => "dorama";

        protected override ContentDtoWithTypeDorama MapToDto(ContentWithType content)
        {
            return new ContentDtoWithTypeDorama
            {
                Id = content.Id,
                Name = content.Film?.Name ?? content.Series?.Name,
                PictureUrl = content.Film?.PictureUrl ?? content.Series?.PictureUrl,
                Rating = content.Film?.Rating ?? content.Series?.Rating,
                ReleaseYear = content.Film?.ReleaseDate.Year ?? content.Series?.ReleaseDate.Year ?? 0,
                Type = content.Type
            };
        }
    }
}
