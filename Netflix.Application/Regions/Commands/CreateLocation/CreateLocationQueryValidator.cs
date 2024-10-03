using FluentValidation;
using Netflix.Application.Regions.Commands.AddRegion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Commands.CreateLocation
{
    public class CreateLocationQueryValidator : AbstractValidator<CreateLocationQuery>
    {
        public CreateLocationQueryValidator()
        {
            RuleFor(x => x.LocationName)
                .NotEmpty()
                .WithMessage("Location name cannot be empty");

            RuleFor(x => x.RegionName)
                .NotEmpty()
                .WithMessage("Region name cannot be empty");
        }
    }
}
