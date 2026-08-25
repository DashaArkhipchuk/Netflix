using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.DTOs.NewsApi
{
    public class NewsArticleDto
    {
        public string? SourceId { get; init; }
        public string? SourceName { get; init; }
        public string? Author { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string Url { get; init; } = string.Empty;
        public string? UrlToImage { get; init; }
        public DateTimeOffset PublishedAt { get; init; }
        public string? Content { get; init; }
    }
}
