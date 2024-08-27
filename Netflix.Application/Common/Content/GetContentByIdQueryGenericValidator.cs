using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Content
{
    public class GetContentByIdQueryGenericValidator<T> : AbstractValidator<GetContentByIdQuery<T>> where T : class
    {
        public GetContentByIdQueryGenericValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
