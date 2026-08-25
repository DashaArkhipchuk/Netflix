using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public SocialMediaProfile Follower { get; set; } = null!;
        public int FollowingId { get; set; }
        public SocialMediaProfile Following { get; set; } = null!;
    }
}
