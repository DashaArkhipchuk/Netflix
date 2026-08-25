using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Netflix.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public SocialMediaProfile Author { get; set; } = null!;

        public string PostText { get; set; } = string.Empty; // max 2200 chars
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public string? Location { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<SocialMediaProfile> CoAuthors { get; set; } = new List<SocialMediaProfile>();
        public ICollection<PostMedia> Media { get; set; } = new List<PostMedia>();
        public ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
        public ICollection<PostSave> Saves { get; set; } = new List<PostSave>();
    }

}
