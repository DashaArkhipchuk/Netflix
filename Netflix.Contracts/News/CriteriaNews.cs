using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public enum NewsSortOption
    {
        Latest,
        MostViewed,
        TitleAsc
    }

    public class CriteriaNews
    {
        public NewsSortOption SortBy { get; set; } = NewsSortOption.Latest;

        public Guid? TypeId { get; set; }

        public string? Search { get; set; }
    }
}
