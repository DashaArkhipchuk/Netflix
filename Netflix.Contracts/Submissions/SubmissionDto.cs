using Netflix.Contracts.ActorProfile.Common;
using Netflix.Contracts.CastingCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Submissions
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public Guid CastingCallId { get; set; }
        public string ActorName { get; set; } = null!;
        public string? SubmissionNote { get; set; }
        public ICollection<string> SubmissionMedias { get; set; } = new List<string>();
    }
}
