using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }        // needed so frontend can highlight the active sidebar category
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<string> Authors { get; set; } = Enumerable.Empty<string>();
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int ViewCount { get; set; }
    }
}
