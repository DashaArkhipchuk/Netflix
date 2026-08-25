using Netflix.Domain.DTOs.NewsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface INewsApiExternalVendorRepository
    {
        Task<List<NewsArticleDto>> GetBulkArticlesAsync(int pages, int pageSize, CancellationToken ct = default);
    }
}
