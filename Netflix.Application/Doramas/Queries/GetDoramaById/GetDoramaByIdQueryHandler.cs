using MediatR;
using Netflix.Application.Common.BaseHandler;
using Netflix.Application.Common.Content;
using Netflix.Application.Doramas.Common;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Doramas.Queries.GetDoramaById
{
    internal class GetDoramaByIdQueryHandler : GetContentByIdQueryHandlerBase<ContentWithTypeDorama>
    {
        public GetDoramaByIdQueryHandler(IContentByTypesRepository contentRepository)
             : base(contentRepository) { }

        protected override string ContentType => "dorama";

        protected override ContentWithTypeDorama MapToDto(ContentWithType content)
        {
            return new ContentWithTypeDorama
            {
                Type = content.Type,
                Film = content.Film,
                Series = content.Series
            };
        }
    }
}
