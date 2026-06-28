using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Commands.SubmitToRoleWithUrls
{
    internal class SubmitToRoleWithUrlsCommandValidator : AbstractValidator<SubmitToRoleWithUrlsCommand>
    {
        public SubmitToRoleWithUrlsCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.CastingId)
                .NotEmpty().WithMessage("CastingId is required.");
            RuleFor(x => x.MediaUrls)
                .NotEmpty().WithMessage("At least one media url is required.");
            
        }
    }
}
