using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Commands.AddRegion
{
    public record CreateLocationQuery
    (
        string LocationName,
        string RegionName
    ) : IRequest<Location>;
}
