using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Submission
    {
        public Guid Id { get; set; }
        public Guid CastingId { get; set; }
        public CastingCall CastingCall { get; set; } = null!;
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
        public string? SubmissionNote {  get; set; }

        public ICollection<SubmissionMedia> SubmissionMedias { get; set; } = new List<SubmissionMedia>();

    }
}
