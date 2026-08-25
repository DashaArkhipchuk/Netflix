using System;
using System.Collections.Generic;

namespace Netflix.Domain.Entities;

public partial class NewsType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
