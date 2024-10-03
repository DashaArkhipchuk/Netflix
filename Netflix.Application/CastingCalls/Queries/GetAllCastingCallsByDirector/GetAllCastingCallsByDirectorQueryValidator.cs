using FluentValidation;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector
{
    public class GetAllCastingCallsByDirectorQueryValidator : AbstractValidator<GetAllCastingCallsByDirectorQuery>
    {
        public GetAllCastingCallsByDirectorQueryValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty()
                .WithMessage("Client Id must not be empty");

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Skip must be greater than or equal to 0.");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Take must be between 1 and 100.");

            RuleFor(x => x.Criteria)
                .SetValidator(new CastingQueryCriteriaValidator())
                .When(x => x.Criteria != null);

        }
    }
}
