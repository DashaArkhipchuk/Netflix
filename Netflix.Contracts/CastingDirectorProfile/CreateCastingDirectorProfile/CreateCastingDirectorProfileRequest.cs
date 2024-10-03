using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.CastingDirectorProfile.CreateCastingDirectorProfile
{
    public record CreateCastingDirectorProfileRequest
    (
        string FullName,
        Guid TypeId,
        string CompanyName,
        string? Website,
        string Address,
        string RegionName,
        string PhoneNumberWithCountryCode,
        string Email
    );
}
