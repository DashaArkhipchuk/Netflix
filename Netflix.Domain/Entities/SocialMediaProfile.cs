using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class SocialMediaProfile
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public string DisplayName { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty; // "@handle"
        public string? AvatarUrl { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public string? AboutMeText { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Follow> Followers { get; set; } = new List<Follow>(); // people following ME
        public ICollection<Follow> Following { get; set; } = new List<Follow>(); // people I follow

        // query stats like NumOfLikes/FollowersCount/FollowingCount from vw_ProfileStats
    }
}
