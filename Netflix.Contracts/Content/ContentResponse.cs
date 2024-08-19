using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Common
{
    public record ContentResponse<T> 
    (
        IReadOnlyList<T> content
    ) where T : class;
}
