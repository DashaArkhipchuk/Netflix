using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Queries.GetSubmissionsByActorId
{
    public class GetSubmissionsByActorIdQueryValidator : AbstractValidator<GetSubmissionsByActorIdQuery>
    {
        public GetSubmissionsByActorIdQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client must be logged in");

            RuleFor(x => x.Skip)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Skip must be greater than or equal to 0.");

            RuleFor(x => x.Take)
                .GreaterThan(0).WithMessage("Take must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Take must be between 1 and 100.");
        }
    }
}
