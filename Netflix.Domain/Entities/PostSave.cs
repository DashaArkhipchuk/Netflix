using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class PostSave
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int ProfileId { get; set; }
        public SocialMediaProfile Profile { get; set; } = null!;
    }
}
