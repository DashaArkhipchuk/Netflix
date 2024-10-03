using MediatR;
using Microsoft.AspNetCore.Http;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Commands.SubmitToRole
{
    public record SubmitToRoleCommand
    (
        Guid CastingId,
        Guid ClientId,
        string? SubmissionNote,
        List<IFormFile> Files
    ) : IRequest<Submission>;
}
