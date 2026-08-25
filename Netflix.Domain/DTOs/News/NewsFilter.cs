using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.DTOs.News
{
    public class NewsFilter
    {
        public Guid? TypeId { get; init; }
        public List<Guid>? AuthorIds { get; init; }
        public string? Search { get; init; }
        public NewsSortOption SortBy { get; init; } = NewsSortOption.Latest;
    }

    public enum NewsSortOption
    {
        Latest,
        MostViewed,
        TitleAsc
    }
}
