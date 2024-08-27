using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.Content;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Cartoons.Queries.GetAllCartoons
{
    public class GetAllContentQueryCartoonValidator : GetAllContentQueryGenericValidator<ContentDtoWithTypeCartoon>
    {
        public GetAllContentQueryCartoonValidator() : base() { }
    }
}
