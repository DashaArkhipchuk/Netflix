using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Commands.CreateActorProfile
{
    public class CreateActorProfileCommandValidator : AbstractValidator<CreateActorProfileCommand>
    {
        public CreateActorProfileCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required.");

            RuleFor(x => x.StageName)
                .NotEmpty().WithMessage("StageName is required.")
                .MaximumLength(70).WithMessage("StageName must be 70 characters or less.");

            RuleFor(x => x.WorkingLocation)
                .NotEmpty().WithMessage("WorkingLocation is required.")
                .MaximumLength(50).WithMessage("WorkingLocation must be 50 characters or less.");

            RuleFor(x => x.RangeFrom)
                .InclusiveBetween(0, 100).WithMessage("RangeFrom must be between 0 and 100.");

            RuleFor(x => x.RangeTo)
                .InclusiveBetween(0, 100).WithMessage("RangeTo must be between 0 and 100.");

            RuleFor(x => x.EthnicAppearance)
                .NotEmpty().WithMessage("EthnicAppearance is required.")
                .MaximumLength(70).WithMessage("EthnicAppearance must be 70 characters or less.");

            RuleFor(x => x.Sex)
                .NotNull().WithMessage("Sex is required.");

            RuleFor(x => x).Custom((command, context) =>
            {
                if (command.RangeFrom > command.RangeTo)
                {
                    context.AddFailure("RangeFrom", "RangeFrom must be less than or equal to RangeTo.");
                }
            });
        }
    }
}
