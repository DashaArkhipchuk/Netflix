using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Cartoons.Queries.GetCartoonById
{
    public class GetContentByIdQueryCartoonValidator : GetContentByIdQueryGenericValidator<ContentWithTypeCartoon>
    {
        public GetContentByIdQueryCartoonValidator() : base() { }
    }
}
