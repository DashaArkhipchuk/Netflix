using Netflix.Application.Animes.Common;
using Netflix.Application.Common.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Animes.Queries.GetAnimeById
{
    public class GetContentByIdQueryAnimeValidator : GetContentByIdQueryGenericValidator<ContentWithTypeAnime>
    {
        public GetContentByIdQueryAnimeValidator() : base() { }
    }
}
