using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Queries.GetRegionNames
{
    internal class GetRegionNamesQueryHandler : IRequestHandler<GetRegionNamesQuery, List<string>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetRegionNamesQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<List<string>> Handle(GetRegionNamesQuery request, CancellationToken cancellationToken)
        {
            return _locationRepository.GetRegionsNamesAsync(request.Skip, request.Take);
        }
    }
}
