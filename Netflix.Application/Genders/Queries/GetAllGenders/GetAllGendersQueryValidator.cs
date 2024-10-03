using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Genders.Queries.GetAllGenders
{
    public class GetAllGendersQueryValidator : GetAllContentWithoutFilteringQueryGenericValidator<Gender>
    {
        public GetAllGendersQueryValidator() : base() { }
    }
}
