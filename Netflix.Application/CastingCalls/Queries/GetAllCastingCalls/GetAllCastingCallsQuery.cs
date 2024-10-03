using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCalls
{
    public record GetAllCastingCallsQuery
    (
        int Skip = 0,
        int Take = 10,
        CastingQueryCriteria? Criteria = null
    ) : IRequest<List<CastingCall>>;
}
