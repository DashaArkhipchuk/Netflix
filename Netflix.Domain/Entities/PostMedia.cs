using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class PostMedia
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public string Url { get; set; } = string.Empty;
        public string MediaType { get; set; } = "image"; // image | video
        public int SortOrder { get; set; }
    }
}
