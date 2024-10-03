using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Locations
{
    public record CreateLocationRequest
    (
        string LocationName,
        string RegionName
    );
}