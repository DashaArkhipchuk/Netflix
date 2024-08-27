using Netflix.Application.Common.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Series.Queries.GetSeriesById
{
    public class GetContentByIdQuerySeriesValidator : GetContentByIdQueryGenericValidator<Domain.Series>
    {
        public GetContentByIdQuerySeriesValidator() : base() { }
    }
}
