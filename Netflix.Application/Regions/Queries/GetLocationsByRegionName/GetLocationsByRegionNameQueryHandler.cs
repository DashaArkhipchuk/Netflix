using MediatR;
using Netflix.Application.Regions.Queries.GetLocationByRegionName;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Queries.GetLocationsByRegionName
{
    internal class GetLocationsByRegionNameQueryHandler : IRequestHandler<GetLocationsByRegionNameQuery, List<Location>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationsByRegionNameQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<List<Location>> Handle(GetLocationsByRegionNameQuery request, CancellationToken cancellationToken)
        {
            return await _locationRepository.GetLocationsByRegionName(request.RegionName, request.Skip,  request.Take);
        }
    }
}
