using MediatR;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Application.Films.Queries.GetAllFilms
{
    internal class GetAllFilmsQueryHandler : IRequestHandler<GetAllFilmsQuery, List<Film>>
    {
        private readonly IFilmRepository _filmRepository;

        public GetAllFilmsQueryHandler(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        public Task<List<Film>> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
        {
            return _filmRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
