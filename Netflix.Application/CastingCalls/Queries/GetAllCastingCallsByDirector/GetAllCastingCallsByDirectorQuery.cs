using MediatR;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByDirector
{
    public record GetAllCastingCallsByDirectorQuery
    (
        Guid ClientId,
        int Skip = 0,
        int Take = 10,
        CastingQueryCriteria? Criteria = null
    ) :IRequest<List<CastingCall>>;
}
