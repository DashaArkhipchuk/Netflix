using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetAllNews
{
    public class GetAllNewsQueryValidator :AbstractValidator<GetAllNewsQuery>
    {
        public GetAllNewsQueryValidator()
        {
            RuleFor(x => x.Skip)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Skip must be greater than or equal to 0.");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                .WithMessage("Take must be between 1 and 100.")
                .LessThanOrEqualTo(100)
                .WithMessage("Take must be between 1 and 100.");
        }
    }
}
