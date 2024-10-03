using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Auditions
{
    public record AddAuditionsForCastingCallRequest
    (
        Guid CastingCallId,
        Guid LocationId,
        DateTime DateFrom,
        DateTime DateTo
    );
}
