using FluentValidation;
using Netflix.Application.CastingCalls.Commands.CreateCastingCall;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Auditions.Commands.AddAuditionForCastingCall
{
    public class AddAuditionForCastingCallCommandValidator : AbstractValidator<AddAuditionForCastingCallCommand>
    {

        public AddAuditionForCastingCallCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client id must not be empty");

            RuleFor(x => x.CastingCallId)
                .NotEmpty().WithMessage("Casting call id must not be empty");

            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo).WithMessage("Date From must be less or equal to Date To");
        }


    }
}
