using Netflix.Application.Common.Content;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Series.Queries.GetAllSeries
{
    public class GetAllContentQuerySeriesValidator : GetAllContentQueryGenericValidator<Domain.Series>
    {
        public GetAllContentQuerySeriesValidator() : base() { }
    }
}
