using MediatR;
using Netflix.Application.CastingDirector.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Queries.GetCastingDirectorProfile
{
    public record GetCastingDirectorProfileQuery
    (
        Guid ClientId
    ) : IRequest<CastingDirectorProfileResult?>;
}
