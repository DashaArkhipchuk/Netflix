using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Queries.GetSubmissionsByActorId
{
    public record GetSubmissionsByActorIdQuery
    (
        Guid ClientId,
        int Skip = 0,
        int Take = 0
    ): IRequest<List<Submission>>;
}
