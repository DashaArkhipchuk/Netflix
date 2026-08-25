using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.NewsApi
{
    public class NewsApiOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://newsapi.org/v2/";
        public int PagesPerRun { get; set; } = 3;   // 1 HTTP request per page
        public int PageSize { get; set; } = 100;    // NewsAPI max
        public int MaxScrapeConcurrency { get; set; } = 5; // be polite to source sites
    }
}
