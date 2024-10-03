using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.CastingCalls.CreateCastingCall
{
    public record CreateCastingCallRequest
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

        ICollection<Guid> LocationIds,
        ICollection<Guid> GenderIds,
        ICollection<Guid> EthnicAppearanceIds
    );
}
