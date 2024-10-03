using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Auditions
{
    public class AuditionExtendedDto
    {
        public Guid Id { get; set; }
        public string CastingCallTitle { get; set; } = null!;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Location { get; set; } = null!;

    }
}
