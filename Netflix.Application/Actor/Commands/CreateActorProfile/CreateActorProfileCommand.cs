using MediatR;
using Netflix.Application.Actor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Actor.Commands.CreateActorProfile
{
    public record CreateActorProfileCommand
    (
        Guid ClientId,
        string StageName,
        string WorkingLocation,
        int RangeFrom,
        int RangeTo,
        string EthnicAppearance,
        bool Sex
    ) : IRequest<ActorProfileResult>;
}
