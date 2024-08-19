using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Application.Films.Queries.GetAllFilms
{
    internal class GetAllFilmsQueryHandler : IRequestHandler<GetAllContentQuery<Film>, List<Film>>
    {
        private readonly IFilmRepository _filmRepository;

        public GetAllFilmsQueryHandler(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        public Task<List<Film>> Handle(GetAllContentQuery<Film> request, CancellationToken cancellationToken)
        {
            return _filmRepository.GetAllAsync(request.Skip, request.Take, request.Criteria?.Genre ?? new List<string>(), request.Criteria?.SortByLatest ?? false, request.Criteria?.MinimumRating, request.Criteria?.Year);
        }
    }
}
