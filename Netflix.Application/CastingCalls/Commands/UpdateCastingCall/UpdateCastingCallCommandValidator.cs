using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Commands.UpdateCastingCall
{
    public class UpdateCastingCallCommandValidator : AbstractValidator<UpdateCastingCallCommand>
    {
        public UpdateCastingCallCommandValidator()
        {
            // Title: Required and non-empty
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title must not exceed 255 characters.");

            // SubmissionDue: Must be in the future
            RuleFor(x => x.SubmissionDue)
                .GreaterThan(DateTime.Now).WithMessage("Submission due date must be in the future.");

            // WorkingDateFrom and WorkingDateTo: Date range validation
            RuleFor(x => x.WorkingDateFrom)
                .LessThanOrEqualTo(x => x.WorkingDateTo).WithMessage("Working Date From must be before or on the Working Date To.")
                .GreaterThanOrEqualTo(x => x.SubmissionDue).WithMessage("Working Dates must be after the Submission Due date");

            // ProjectTypeId and RoleTypeId: Required fields
            RuleFor(x => x.ProjectTypeId)
                .NotEmpty().WithMessage("Project Type is required.");

            RuleFor(x => x.RoleTypeId)
                .NotEmpty().WithMessage("Role Type is required.");

            // PlayableAgeFrom and PlayableAgeTo: Playable age range validation
            RuleFor(x => x.PlayableAgeFrom)
                .GreaterThanOrEqualTo(0).WithMessage("Playable Age From must be 0 or greater.");

            RuleFor(x => x.PlayableAgeTo)
                .GreaterThanOrEqualTo(x => x.PlayableAgeFrom).WithMessage("Playable Age To must be greater than or equal to Playable Age From.")
                .LessThanOrEqualTo(100).WithMessage("Playable Age To must be less than or equal to 100");

            // Optional fields with maximum length
            RuleFor(x => x.Payment)
                .NotEmpty().WithMessage("Payment information is required.")
                .MaximumLength(100).WithMessage("Payment details must not exceed 500 characters.");

            RuleFor(x => x.UnionDetails)
                .MaximumLength(255).WithMessage("Union Details must not exceed 500 characters.");

            RuleFor(x => x.RoleDescription)
                .NotEmpty().WithMessage("Role Description is required.")
                .MaximumLength(1000).WithMessage("Role Description must not exceed 1000 characters.");

            RuleFor(x => x.RateDetails)
                .MaximumLength(500).WithMessage("Rate Details must not exceed 500 characters.");

            RuleFor(x => x.WorkRequirements)
                .MaximumLength(1000).WithMessage("Work Requirements must not exceed 1000 characters.");

            RuleFor(x => x.WorkInformation)
                .MaximumLength(1000).WithMessage("Work Information must not exceed 1000 characters.");

            RuleFor(x => x.RequestedMedia)
                .MaximumLength(500).WithMessage("Requested Media must not exceed 500 characters.");

            RuleFor(x => x.InstructionsForSubmissionNote)
                .MaximumLength(1000).WithMessage("Instructions for Submission Note must not exceed 1000 characters.");

            RuleFor(x => x.RequestingSubmissionsFrom)
                .MaximumLength(500).WithMessage("Requesting Submissions From must not exceed 500 characters.");

            // LocationIds, GenderIds, EthnicAppearanceIds: Cannot be empty collections
            RuleFor(x => x.LocationIds)
                .NotEmpty().WithMessage("At least one location is required.");

            RuleFor(x => x.GenderIds)
                .NotEmpty().WithMessage("At least one gender is required unless any gender is accepted.")
                .When(x => !x.IsAnyGenderAccepted);

            RuleFor(x => x.EthnicAppearanceIds)
                .NotEmpty().WithMessage("At least one ethnic appearance is required unless any ethnic appearance is accepted.")
                .When(x => !x.IsAnyEthnicAppearanceAccepted);
        }
    }
}
