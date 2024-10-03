using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Auditions.Commands.RemoveAuditionForCastingCall
{
    public record RemoveAuditionForCastingCallCommand
    (
        Guid ClientId,
        Guid AuditionId
    ) :IRequest<bool>;
}
