using Netflix.Application.Animes.Common;
using Netflix.Application.Common.Content;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Animes.Queries.GetAllAnimes
{
    public class GetAllContentQueryAnimeValidator : GetAllContentQueryGenericValidator<ContentDtoWithTypeAnime>
    {
        public GetAllContentQueryAnimeValidator() : base() { }
    }
}
