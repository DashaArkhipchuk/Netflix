using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public class NewsExtendedDto
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<string> Authors { get; set; } = Enumerable.Empty<string>();
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string ArticleText { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public string SourceUrl { get; set; } = string.Empty;
        public string? SourceName { get; set; }
        public List<ShortNewsDto> RelatedNews { get; set; } = new();
    }
}
