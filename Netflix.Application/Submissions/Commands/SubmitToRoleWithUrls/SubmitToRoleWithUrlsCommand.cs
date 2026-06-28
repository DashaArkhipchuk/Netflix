using MediatR;
using Microsoft.AspNetCore.Http;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Submissions.Commands.SubmitToRoleWithUrls
{
    public record SubmitToRoleWithUrlsCommand
    (
        Guid CastingId,
        Guid ClientId,
        string? SubmissionNote,
        List<string> MediaUrls
    ) : IRequest<Submission>;
}
