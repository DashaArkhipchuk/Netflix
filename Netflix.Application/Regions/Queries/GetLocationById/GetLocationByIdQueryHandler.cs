using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Regions.Queries.GetLocationById
{
    internal class GetLocationByIdQueryHandler : IRequestHandler<GetContentByIdQuery<Location>, Location?>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationByIdQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<Location?> Handle(GetContentByIdQuery<Location> request, CancellationToken cancellationToken)
        {
            return _locationRepository.GetLocationById(request.Id);
        }
    }
}
