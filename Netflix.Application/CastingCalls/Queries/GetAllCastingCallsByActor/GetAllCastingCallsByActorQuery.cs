using MediatR;
using Netflix.Application.CastingCalls.Queries.GetAllCastingCalls;
using Netflix.Domain.DTOs;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCallsByActor
{
    public record GetAllCastingCallsByActorQuery
    (
        Guid ClientId,
        int Skip = 0,
        int Take = 10,
        CastingQueryCriteria? Criteria = null
    ) : IRequest<PagedResult<CastingCall>>;
}
