using Netflix.Application.Common.Content;
using Netflix.Application.Doramas.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Doramas.Queries.GetDoramaById
{
    public class GetContentByIdQueryDoramaValidator : GetContentByIdQueryGenericValidator<ContentWithTypeDorama>
    {
        public GetContentByIdQueryDoramaValidator() : base() { }
    }
}
