using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Commands.CreateCastingCall
{
    public record CreateCastingCallCommand
    (
        string Title,
        DateTime SubmissionDue,
        DateTime WorkingDateFrom,
        DateTime WorkingDateTo,
        Guid ProjectTypeId,
        Guid RoleTypeId,

        int PlayableAgeFrom,
        int PlayableAgeTo,
        string? Payment,
        string? UnionDetails,
        string? RoleDescription,
        string? RateDetails,
        string? WorkRequirements,
        string? WorkInformation,
        string? RequestedMedia,
        string? InstructionsForSubmissionNote,
        string? RequestingSubmissionsFrom,
        bool IsAnyEthnicAppearanceAccepted,
        bool IsAnyGenderAccepted,
        Guid CreatedByDirectorId,

        ICollection<Guid> LocationIds,
        ICollection<Guid> GenderIds,
        ICollection<Guid> EthnicAppearanceIds
    ) : IRequest<CastingCall>;
}
