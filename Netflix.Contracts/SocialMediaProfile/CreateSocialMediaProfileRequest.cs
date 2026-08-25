using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.SocialMediaProfile
{
    public record CreateSocialMediaProfileRequest
    (
        string DisplayName,
        string Nickname,
        string? AvatarUrl,
        string? BackgroundUrl,
        string? PhoneNumber,
        string? Country,
        string? City,
        string? Gender,
        string? AboutMeText
    );
}
