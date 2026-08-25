using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.SocialMediaProfile.Common
{
    public record SocialMediaProfileResponse
    (
        Guid id,
        Guid ClientId,
        string DisplayName,
        string Nickname,
        int AvatarUrl
    );
}
