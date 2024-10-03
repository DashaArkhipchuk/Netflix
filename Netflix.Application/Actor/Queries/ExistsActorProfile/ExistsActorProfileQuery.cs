using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Queries.ExistsActorProfile
{
    public record ExistsActorProfileQuery
    (
        Guid ClientId
    )  : IRequest<bool>;
}
