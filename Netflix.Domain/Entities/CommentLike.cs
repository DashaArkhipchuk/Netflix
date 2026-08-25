using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class CommentLike
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
        public int ProfileId { get; set; }
        public SocialMediaProfile Profile { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
