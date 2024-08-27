using FluentValidation.Validators;
using MediatR;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.BaseHandler;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Cartoons.Queries.GetAllCartoons
{
    internal class GetAllCartoonsQueryHandler : GetAllContentQueryHandlerBase<ContentDtoWithTypeCartoon>
    {
        public GetAllCartoonsQueryHandler(IContentByTypesRepository contentRepository) : base(contentRepository) { }

        protected override string ContentType => "cartoon";

        protected override ContentDtoWithTypeCartoon MapToDto(Netflix.Domain.ContentWithTypeType.ContentWithType content)
        {
            return new ContentDtoWithTypeCartoon
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
