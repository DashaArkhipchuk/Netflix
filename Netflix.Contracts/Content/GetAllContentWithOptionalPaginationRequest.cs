using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Content
{
    public record GetAllContentWithOptionalPaginationRequest
    (
        int? Take,
        int Skip = 0
    );
}
