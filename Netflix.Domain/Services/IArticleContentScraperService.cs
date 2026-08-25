using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Services
{
    public interface IArticleContentScraperService
    {
        // Returns null on failure (paywall, blocked, timeout, 404, etc.) — callers must treat that as "no full text available", not an error.
        Task<string?> ScrapeArticleTextAsync(string url, CancellationToken ct = default);
    }
}
