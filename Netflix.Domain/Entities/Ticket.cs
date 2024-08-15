using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class Ticket
{
    public Guid Id { get; set; }

    public int SeatNumber { get; set; }

    public decimal Price { get; set; }
}
