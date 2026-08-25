using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.NewsApi.Dtos
{

    public sealed class ArticleDto
    {
        [JsonPropertyName("source")]
        public SourceDto Source { get; init; } = new();

        [JsonPropertyName("author")]
        public string? Author { get; init; }

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;

        [JsonPropertyName("urlToImage")]
        public string? UrlToImage { get; init; }

        [JsonPropertyName("publishedAt")]
        public DateTimeOffset PublishedAt { get; init; }

        [JsonPropertyName("content")]
        public string? Content { get; init; }
    }
}
