using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public class NewsTypeDto
    {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int ArticleCount { get; set; } // lets the sidebar show "Movie News (23)" style counts
    }
}
