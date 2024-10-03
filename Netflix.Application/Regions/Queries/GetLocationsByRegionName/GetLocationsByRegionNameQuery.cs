using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Queries.GetLocationByRegionName
{
    public record GetLocationsByRegionNameQuery
    (
        string RegionName,
        int Skip = 0,
        int Take = 10
    ) : IRequest<List<Location>>;
}
