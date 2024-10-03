using MediatR;
using Netflix.Application.Common.Errors;
using Netflix.Application.Regions.Commands.AddRegion;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Commands.AddLocation
{
    internal class CreateLocationQueryHandler : IRequestHandler<CreateLocationQuery, Location>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(CreateLocationQuery request, CancellationToken cancellationToken)
        {
            if (await _locationRepository.GetLocationByLocationAndRegionNamesAsync(request.LocationName, request.RegionName) is Location l)
            {
                throw new AlreadyExistsException("Location", "location name and region name");
            }

            Location? existingRegion = null;
            if ((await _locationRepository.GetLocationsByRegionName(request.RegionName, 0, 1)) is List<Location> list)
            {
                if (list.Count > 0)
                {
                    existingRegion = list.FirstOrDefault();
                }
            }


            var newLocation = new Location
            {
                Id = Guid.NewGuid(),
                LocationName = request.LocationName,
                RegionName = existingRegion?.RegionName ?? request.RegionName
            };

            _locationRepository.Add(newLocation);


            return newLocation;
        }
    }
}
