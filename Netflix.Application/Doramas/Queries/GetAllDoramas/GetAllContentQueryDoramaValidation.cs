using Netflix.Application.Common.Content;
using Netflix.Application.Doramas.Common;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Doramas.Queries.GetAllDoramas
{
    public class GetAllContentQueryDoramaValidator : GetAllContentQueryGenericValidator<ContentDtoWithTypeDorama>
    {
        public GetAllContentQueryDoramaValidator() : base() { }
    }
}
