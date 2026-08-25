using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
