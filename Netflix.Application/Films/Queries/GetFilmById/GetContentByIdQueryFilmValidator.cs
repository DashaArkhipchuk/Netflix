using Netflix.Application.Common.Content;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Films.Queries.GetFilmById
{
    public class GetContentByIdQueryFilmValidator : GetContentByIdQueryGenericValidator<Film>
    {
        public GetContentByIdQueryFilmValidator() : base() { }
    }
}
