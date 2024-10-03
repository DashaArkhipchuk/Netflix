using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.TypesDtos
{
    public class ProjectTypeWithoutCastingCallsReference
    {
        public Guid Id { get; set; }
        public string ProjectTypeName { get; set; } = null!;
    }
}
