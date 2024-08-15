using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class OperationLog
{
    public Guid Id { get; set; }

    public Guid OperationType { get; set; }

    public DateTime OperationDateTimeStart { get; set; }

    public DateTime? OperationDateTimeEnd { get; set; }

    public Guid IdClient { get; set; }

    public Guid IdProduct { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual OperationType OperationTypeNavigation { get; set; } = null!;
}
