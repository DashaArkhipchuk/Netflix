using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Content
{
    public class QueryCriteriaValidator : AbstractValidator<QueryCriteria>
    {
        public QueryCriteriaValidator()
        {
            // Validate Genre list
            RuleFor(x => x.Genre)
                .ForEach(genreRule => genreRule
                    .NotEmpty()
                    .WithMessage("Genre cannot be empty or whitespace.")
                    .Must(g => !string.IsNullOrWhiteSpace(g))
                    .WithMessage("Genre cannot be whitespace.")
                );

            // Validate MinimumRating
            RuleFor(x => x.MinimumRating)
                .InclusiveBetween(0, 10)
                .When(x => x.MinimumRating.HasValue)
                .WithMessage("MinimumRating must be between 0 and 10.");

            // Validate Year
            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .When(x => x.Year.HasValue)
                .WithMessage($"Year must be between 1900 and {DateTime.Now.Year}.");

            // Validate Episodes
            RuleFor(x => x.Episodes)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Episodes.HasValue)
                .WithMessage("Episodes must be greater than or equal to 0.");
        }
    }
}
