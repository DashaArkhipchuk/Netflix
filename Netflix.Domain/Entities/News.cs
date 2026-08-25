using System;
using System.Collections.Generic;

namespace Netflix.Domain.Entities;

public partial class News
{
    public Guid Id { get; set; }

    public Guid TypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ArticleText { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime PublishedDate { get; set; }

    public int ViewCount { get; set; }

    public string SourceUrl { get; set; } = string.Empty;

    public string? SourceName { get; set; }

    public virtual NewsType Type { get; set; } = null!;

    public virtual ICollection<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
}
