using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.ActorProfile.Common
{
    public record ActorProfileResponse
    (
        Guid id,
        Guid ClientId,
        string StageName,
        string WorkingLocation,
        int RangeFrom,
        int RangeTo,
        string EthnicAppearance,
        bool Sex
    );
}
