using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Audition
    {
        public Guid Id { get; set; }
        public Guid IdCastingCall { get; set; }
        public Guid LocationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public CastingCall CastingCall { get; set; } = null!;
        public Location Location { get; set; } = null!;
    }
}
