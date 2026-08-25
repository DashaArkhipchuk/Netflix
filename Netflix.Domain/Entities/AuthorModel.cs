using System;
using System.Collections.Generic;

namespace Netflix.Domain.Entities;

public partial class AuthorModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public virtual ICollection<News>? News { get; set; } = new List<News>();
}
