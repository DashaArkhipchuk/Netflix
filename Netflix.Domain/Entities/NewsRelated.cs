using System;
using System.Collections.Generic;

namespace Netflix.Domain.Entities;

public partial class NewsRelated
{
    public Guid NewsId { get; set; }
    public Guid RelatedNewsId { get; set; }

    public virtual News News { get; set; } = null!;
    public virtual News RelatedNewsEntity { get; set; } = null!;
}
