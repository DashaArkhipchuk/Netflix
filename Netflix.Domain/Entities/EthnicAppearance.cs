using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class EthnicAppearance
    {
        public Guid Id { get; set; }
        public string EthnicAppearanceName { get; set; } = null!;

        public ICollection<CastingCall> CastingCalls { get; set; } = new List<CastingCall>();
    }
}
