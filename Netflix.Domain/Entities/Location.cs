using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        public string LocationName { get; set; }
        public string RegionName { get; set; }

        public ICollection<CastingCall> CastingCalls { get; set; } = new List<CastingCall>();
        public ICollection<Audition> Auditions { get; set; } = new LinkedList<Audition>();
    }
}
