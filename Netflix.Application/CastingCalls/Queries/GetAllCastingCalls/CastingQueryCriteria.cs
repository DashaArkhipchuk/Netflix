using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCalls
{
    public class CastingQueryCriteria
    {
        public List<string> Locations { get; set; } = [];
        public List<string> PlayableAgeRanges { get; set; } = [];
        public List<string> ProjectTypes { get; set; } = [];
        public List<string> RoleTypes { get; set; } = [];

    }
}
