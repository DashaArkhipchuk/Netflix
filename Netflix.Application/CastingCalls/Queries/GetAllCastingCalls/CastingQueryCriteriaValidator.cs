using FluentValidation;
using Netflix.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCalls
{
    public class CastingQueryCriteriaValidator : AbstractValidator<CastingQueryCriteria>
    {
        public CastingQueryCriteriaValidator() 
        {
            RuleFor(x => x.ProjectTypes)
                .ForEach(rule => rule
                    .NotEmpty()
                    .WithMessage("Project type cannot be empty or whitespace.")
                    .Must(ValidationPredicateHelpers.BeNotNullOrWhitespace)
                    .WithMessage("Project type cannot be whitespace.")
                );

            RuleFor(x => x.RoleTypes)
                .ForEach(rule => rule
                    .NotEmpty()
                    .WithMessage("Role type cannot be empty or whitespace.")
                    .Must(ValidationPredicateHelpers.BeNotNullOrWhitespace)
                    .WithMessage("Role type cannot be whitespace.")
                );

            RuleFor(x => x.Locations)
                .ForEach(rule => rule
                    .NotEmpty()
                    .WithMessage("Location cannot be empty or whitespace.")
                    .Must(ValidationPredicateHelpers.BeNotNullOrWhitespace)
                    .WithMessage("Location cannot be whitespace.")
                );

            RuleFor(x => x.PlayableAgeRanges)
                .ForEach(rule => rule
                    .NotEmpty()
                    .WithMessage("Playable age range cannot be empty or whitespace.")
                    .Must(ValidationPredicateHelpers.BeNotNullOrWhitespace)
                    .WithMessage("Playable age range cannot be whitespace.")
                    .Must(ValidationPredicateHelpers.BeAValidRange).WithMessage("Range format is invalid.")
                );
        }
    }
}
