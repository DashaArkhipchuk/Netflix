using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Queries.GetRegionNames
{
    public record GetRegionNamesQuery
    (
        int Skip = 0,
        int Take = 10
    ) : IRequest<List<string>>;
}
