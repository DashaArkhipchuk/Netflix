using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Common
{
    public record GetAllContentRequest
    (
        int Skip = 0,
        int Take = 10
    );
}
