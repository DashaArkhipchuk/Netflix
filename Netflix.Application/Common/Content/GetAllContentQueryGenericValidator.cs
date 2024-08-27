using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Content
{
    public class GetAllContentQueryGenericValidator<T> : AbstractValidator<GetAllContentQuery<T>> where T : class
    {
        public GetAllContentQueryGenericValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Skip must be greater than or equal to 0.");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Take must be between 1 and 100.");

            RuleFor(x => x.Criteria)
                .SetValidator(new QueryCriteriaValidator())
                .When(x => x.Criteria != null);
        }
    }
}
