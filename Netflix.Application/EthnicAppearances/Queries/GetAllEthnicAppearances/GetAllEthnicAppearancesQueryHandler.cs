using MediatR;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.EthnicAppearances.Queries.GetAllEthnicAppearances
{
    internal class GetAllEthnicAppearancesQueryHandler : IRequestHandler<GetAllContentWithoutFilteringQuery<EthnicAppearance>, List<EthnicAppearance>>
    {
        private readonly IEthnicAppearanceRepository _appearanceRepository;

        public GetAllEthnicAppearancesQueryHandler(IEthnicAppearanceRepository appearanceRepository)
        {
            _appearanceRepository = appearanceRepository;
        }

        public Task<List<EthnicAppearance>> Handle(GetAllContentWithoutFilteringQuery<EthnicAppearance> request, CancellationToken cancellationToken)
        {
            return _appearanceRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
