using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.TypesDtos
{
    public class RoleTypeWithoutCastingCallsReference
    {
        public Guid Id { get; set; }
        public string RoleTypeName { get; set; }
    }
}
