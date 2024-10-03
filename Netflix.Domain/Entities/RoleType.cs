using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class RoleType
    {
        public Guid Id { get; set; }
        public string RoleTypeName { get; set; }

        public ICollection<CastingCall> CastingCalls { get; set; } = new List<CastingCall>();
    }
}
