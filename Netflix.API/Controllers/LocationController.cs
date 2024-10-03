using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Application.Regions.Commands.AddRegion;
using Netflix.Application.Regions.Queries.GetLocationByRegionName;
using Netflix.Application.Regions.Queries.GetRegionNames;
using Netflix.Contracts.Common;
using Netflix.Contracts.Locations;
using Netflix.Domain;
using Netflix.Domain.Entities;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LocationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAllRegionNames")]
        public async Task<IActionResult> GetRegionNames([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetRegionNamesQuery>(request);

            var regions = await _mediator.Send(command);

            return Ok(regions);
        }

        [HttpPost("GetLocations/{regionName}")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request, [FromRoute] string regionName)
        {
            var command = _mapper.Map<(string, GetAllContentRequest), GetLocationsByRegionNameQuery>((regionName, request));

            var locations = await _mediator.Send(command);

            var locationDtos = locations.Select(x => new { x.Id, x.LocationName, x.RegionName });

            return Ok(locationDtos);
        }

        [HttpPost("CreateLocation")]
        public async Task<IActionResult> Create([FromBody] CreateLocationRequest request)
        {
            var command = _mapper.Map<CreateLocationQuery>(request);

            var location = await _mediator.Send(command);

            var locationDto = new {location.Id, location.LocationName, location.RegionName};

            return Ok(locationDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = _mapper.Map<GetContentByIdQuery<Location>>(id);

            var location = await _mediator.Send(command);

            if (location == null)
                return NotFound();

            return Ok(new { location.Id, location.LocationName, location.RegionName });
        }
    }
}
