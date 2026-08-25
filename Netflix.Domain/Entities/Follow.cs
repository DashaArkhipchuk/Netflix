using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Follow
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public SocialMediaProfile Follower { get; set; } = null!;
        public Guid FollowingId { get; set; }
        public SocialMediaProfile Following { get; set; } = null!;
    }
}
