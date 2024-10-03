using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain;

public partial class Actor
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string StageName { get; set; } = null!;
    public string WorkingLocation { get; set; } = null!;
    public int RangeFrom { get; set; }
    public int RangeTo { get; set; }
    public string EthnicAppearance { get; set; } = null!;
    public bool Sex { get; set; }
    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

}
