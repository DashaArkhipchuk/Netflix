using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int AuthorId { get; set; }
        public SocialMediaProfile Author { get; set; } = null!;
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<CommentLike> Likes { get; set; } = new List<CommentLike>();
    }
}
