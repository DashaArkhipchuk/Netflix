using FluentValidation;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes
{
    public class GetAllProjectTypesQueryValidator : GetAllContentWithoutFilteringQueryGenericValidator<ProjectType>
    {
        public GetAllProjectTypesQueryValidator() : base() { }
    }
}
