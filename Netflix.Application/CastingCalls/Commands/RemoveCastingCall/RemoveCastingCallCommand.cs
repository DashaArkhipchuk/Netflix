using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Commands.RemoveCastingCall
{
    public record RemoveCastingCallCommand
    (
        Guid ClientId, 
        Guid CastingId
    ) : IRequest<bool>;
}
