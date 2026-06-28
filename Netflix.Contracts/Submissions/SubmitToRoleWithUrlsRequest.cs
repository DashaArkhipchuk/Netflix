using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Submissions
{
    public record SubmitToRoleWithUrlsRequest
    (
        Guid CastingId,
        string? SubmissionNote,
        List<string> MediaUrls
    );
}
