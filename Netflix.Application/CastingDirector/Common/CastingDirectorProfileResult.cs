using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;

namespace Netflix.Application.CastingDirector.Common
{
    public record CastingDirectorProfileResult
    (
        Domain.Entities.CastingDirector director
    );
}
