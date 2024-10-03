using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.EthnicAppearances.Queries.GetAllEthnicAppearances
{
    public class GetAllEthnicAppearancesQueryValidator : GetAllContentWithoutFilteringQueryGenericValidator<EthnicAppearance>
    {
        public GetAllEthnicAppearancesQueryValidator() : base() { }
    }
}
