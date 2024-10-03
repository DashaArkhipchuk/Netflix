using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.CastingDirectorProfile.Common
{
    public record CastingDirectorProfileResponse
    (
        Guid Id,
        Guid ClientId,
        string FullName,
        string Type,
        string CompanyName,
        string? Website,
        string Address,
        string RegionName,
        string PhoneNumberWithCountryCode,
        string Email
    );
}
