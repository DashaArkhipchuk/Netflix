using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;

namespace Netflix.Application.SocialMediaProfiles.Commands.CreateSocialMediaProfile
{
    public record CreateSocialMediaProfileCommand
    (
        Guid ClientId,
        string DisplayName,
        string Nickname,
        string? AvatarUrl,
        string? BackgroundUrl,
        string? PhoneNumber,
        string? Country,
        string? City,
        string? Gender,
        string? AboutMeText
    ):IRequest<SocialMediaProfile>;
}
