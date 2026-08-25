using Netflix.Infrastructure.ExternalApi.NewsApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.NewsApi
{
    public interface INewsApiHttpClientService
    {
        Task<NewsApiResponseDto?> GetEverythingAsync(string query, int page, int pageSize, CancellationToken ct = default);
    }
}
