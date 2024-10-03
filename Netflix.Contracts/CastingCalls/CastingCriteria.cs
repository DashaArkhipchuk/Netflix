using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.CastingCalls
{
    public class CastingCriteria
    {
        public List<string> Locations { get; set; } = [];
        public List<string> PlayableAgeRanges { get; set; } = [];
        public List<string> ProjectTypes { get; set; } = [];
        public List<string> RoleTypes { get; set; } = [];
    }
}
