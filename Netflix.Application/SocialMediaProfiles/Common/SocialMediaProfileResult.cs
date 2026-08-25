using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;

namespace Netflix.Application.SocialMediaProfiles.Common
{
    public record SocialMediaProfileResult
    (
        SocialMediaProfile profile
    );
}
