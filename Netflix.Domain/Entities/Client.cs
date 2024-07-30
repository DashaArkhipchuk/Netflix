using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class Client
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Password { get; set; } = null!;

    public bool? IsActor { get; set; }

    public virtual ICollection<OperationLog> OperationLogs { get; set; } = new List<OperationLog>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
