using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Common
{
    public enum NewsSortOption
    {
        Latest,
        MostViewed,
        TitleAsc
    }

    public class QueryCriteriaNews
    {
        public NewsSortOption SortBy { get; set; } = NewsSortOption.Latest;

        // Maps directly to your sidebar categories (NewsType.Id).
        public Guid? TypeId { get; set; }
        
        public string? Search { get; set; }
    }
}
