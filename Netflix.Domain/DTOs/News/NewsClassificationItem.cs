using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.DTOs.News
{
    // Lightweight projection leavig only fields classification needs,
    // no point loading full News entities into memory for this
    public class NewsClassificationItem
    {
        public Guid Id { get; init; }
        public Guid TypeId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? ArticleTextExcerpt { get; init; }
    }
}
