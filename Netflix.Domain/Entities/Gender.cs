using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class Gender
    {
        public Guid Id { get; set; }
        public string GenderName { get; set; }

        public ICollection<CastingCall> CastingCalls { get; set; } = new List<CastingCall>();
    }
}
