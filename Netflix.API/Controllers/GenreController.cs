using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.Common.Content;
using Netflix.Contracts.Common;
using Netflix.Contracts.Genres.GetAllGenres;
using Netflix.Domain;

namespace Netflix.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenreController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromQuery] GetAllContentRequest request)
        {
            var command = _mapper.Map<GetAllContentQuery<GenreModel>>(request);

            var genres = await _mediator.Send(command);
            var genreDtos = genres
                .Select(x => new GenreDto() { Id = x.Id, GenreName = x.GenreName }).ToList();

            return Ok(new ContentResponse<GenreDto>(genreDtos));
        }
    }
}
