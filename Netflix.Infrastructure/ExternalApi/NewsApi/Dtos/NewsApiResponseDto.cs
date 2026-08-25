using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.NewsApi.Dtos
{
    public sealed class NewsApiResponseDto
    {
        [JsonPropertyName("status")]
        public string Status { get; init; } = string.Empty;

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; init; }

        [JsonPropertyName("articles")]
        public List<ArticleDto> Articles { get; init; } = [];
    }
}
