using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.CastingCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Submissions
{
    public class SubmissionExtendedDto
    {
        public Guid Id { get; set; }
        public CastingCallDto CastingCall { get; set; } = null!;
        public ActorProfileResponse Actor { get; set; } = null!;
        public string? SubmissionNote { get; set; }

        public ICollection<string> SubmissionMedias { get; set; } = new List<string>();
    }
}
