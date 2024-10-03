using MediatR;
using Netflix.Application.Actor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Queries.GetActorProfile
{
    public record GetActorProfileQuery
    (
        Guid ClientId
    ): IRequest<ActorProfileResult>;
}
