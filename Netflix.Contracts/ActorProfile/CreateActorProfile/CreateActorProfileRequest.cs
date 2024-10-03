using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.ActorProfile.CreateActorProfile
{
    public record CreateActorProfileRequest
    (
        string StageName,
        string WorkingLocation,
        int RangeFrom,
        int RangeTo,
        string EthnicAppearance,
        bool Sex
    );
}
