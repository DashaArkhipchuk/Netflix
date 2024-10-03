using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Queries.GetAllSubmissionsByCastingCall
{
    public record GetAllSubmissionsByCastingCallQuery
    (
        Guid ClientId,
        Guid CastingCallId,
        int Skip = 0,
        int Take = 0
    ) : IRequest<List<Submission>>;
}
