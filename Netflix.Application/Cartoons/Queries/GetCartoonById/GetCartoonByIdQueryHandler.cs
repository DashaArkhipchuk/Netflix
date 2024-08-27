using MediatR;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.BaseHandler;
using Netflix.Application.Common.Content;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Cartoons.Queries.GetCartoonById
{
    internal class GetCartoonByIdQueryHandler : GetContentByIdQueryHandlerBase<ContentWithTypeCartoon>
    {
        public GetCartoonByIdQueryHandler(IContentByTypesRepository contentRepository)
            : base(contentRepository) { }

        protected override string ContentType => "cartoon";

        protected override ContentWithTypeCartoon MapToDto(ContentWithType content)
        {
            return new ContentWithTypeCartoon
            {
                Type = content.Type,
                Film = content.Film,
                Series = content.Series
            };
        }
    }
}
