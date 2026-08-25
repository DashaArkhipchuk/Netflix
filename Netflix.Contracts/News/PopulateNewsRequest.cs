using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public class PopulateNewsRequest
    {
        public bool IncludeCelebrityNews { get; set; } = true;
        public bool IncludeMovieNews { get; set; } = true;
        public bool IncludeAwardsNews { get; set; } = true;
        public int MaxArticlesPerType { get; set; } = 100;
    }
}
