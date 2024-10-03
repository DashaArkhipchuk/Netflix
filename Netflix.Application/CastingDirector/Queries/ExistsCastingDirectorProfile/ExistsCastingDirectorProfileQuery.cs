using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Queries.ExistsCastingDirectorProfile
{
    public record ExistsCastingDirectorProfileQuery
    (
        Guid ClientId
    ) : IRequest<bool>;
}
