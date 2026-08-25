using Microsoft.Extensions.Options;
using Netflix.Domain.DTOs.NewsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.NewsApi
{
    public class NewsPopulationSettings(IOptions<NewsApiOptions> options) : INewsPopulationSettings
    {
        public int DefaultPages => options.Value.PagesPerRun;
        public int DefaultPageSize => options.Value.PageSize;
        public int MaxScrapeConcurrency => options.Value.MaxScrapeConcurrency;
    }
}