using Netflix.Infrastructure.ExternalApi.NewsApi;
using Netflix.Infrastructure.ExternalApi.NewsApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Services
{
    public class NewsApiHttpClientService(HttpClient httpClient) : INewsApiHttpClientService
    {
        public async Task<NewsApiResponseDto?> GetEverythingAsync(string query, int page, int pageSize, CancellationToken ct = default)
        {
            var encodedQuery = Uri.EscapeDataString(query);
            var url = $"everything?q={encodedQuery}&language=en&sortBy=relevancy&pageSize={pageSize}&page={page}";

            var response = await httpClient.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
            {
                // NewsAPI puts error details in the body even on 4xx/5xx
                var body = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"NewsAPI request failed ({(int)response.StatusCode}): {body}");
            }

            return await response.Content.ReadFromJsonAsync<NewsApiResponseDto>(cancellationToken: ct);
        }
    }
}
