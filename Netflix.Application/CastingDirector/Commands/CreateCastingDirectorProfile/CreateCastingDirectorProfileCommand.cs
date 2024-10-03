using MediatR;
using Netflix.Application.CastingDirector.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Commands.CreateCastingDirectorProfile
{
    public record CreateCastingDirectorProfileCommand
    (
        Guid ClientId,
        string FullName,
        Guid TypeId,
        string CompanyName,
        string? Website,
        string Address,
        string RegionName,
        string PhoneNumberWithCountryCode,
        string Email
     ) : IRequest<CastingDirectorProfileResult>;
}
