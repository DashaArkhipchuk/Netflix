using Netflix.Application.Common.Content;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Films.Queries.GetAllFilms
{
    public class GetAllContentQueryFilmValidator : GetAllContentQueryGenericValidator<Film>
    {
        public GetAllContentQueryFilmValidator() : base(){}
    }
}
