using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public partial class NewsType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<News> NewsCollection { get; set; } = new List<News>();
    }
}
