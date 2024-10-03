using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Auditions.Commands.AddAuditionForCastingCall
{
    public record AddAuditionForCastingCallCommand
    (
        Guid ClientId,
        Guid CastingCallId,
        Guid LocationId,
        DateTime DateFrom,
        DateTime DateTo
    ) : IRequest<Audition>;
}
