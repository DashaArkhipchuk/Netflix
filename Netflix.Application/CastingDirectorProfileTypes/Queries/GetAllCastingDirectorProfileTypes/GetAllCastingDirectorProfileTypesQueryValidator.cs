using FluentValidation;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirectorProfileTypes.Queries.GetAllCastingDirectorProfileTypes
{
    public class GetAllCastingDirectorProfileTypesQueryValidator : AbstractValidator<GetAllContentWithoutFilteringQuery<CastingDirectorType>>
    {
        public GetAllCastingDirectorProfileTypesQueryValidator()
        {
            RuleFor(x => x.Skip)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Skip must be greater than or equal to 0.");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Take must be between 1 and 100.");
        }
    }
}
