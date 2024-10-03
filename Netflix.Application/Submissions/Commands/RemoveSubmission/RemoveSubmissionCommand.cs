using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Commands.RemoveSubmission
{
    public record RemoveSubmissionCommand
    (
        Guid ClientId, 
        Guid SubmissionId
    ) : IRequest<bool>;
}
