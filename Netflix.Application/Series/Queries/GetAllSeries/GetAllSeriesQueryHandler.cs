using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Application.Films.Queries.GetAllFilms;
using Netflix.Application.Series.Queries.GetAllSeries;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Application.Series.Queries.GetAllSeries
{
    internal class GetAllSeriesQueryHandler : IRequestHandler<GetAllContentQuery<Domain.Series>, List<Netflix.Domain.Series>>
    {
        private readonly ISeriesRepository _seriesRepository;

        public GetAllSeriesQueryHandler(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public Task<List<Domain.Series>> Handle(GetAllContentQuery<Domain.Series> request, CancellationToken cancellationToken)
        {
            return _seriesRepository.GetAllAsync(request.Skip, request.Take, request.Criteria?.Genre, request.Criteria?.SortByLatest ?? false, request.Criteria?.MinimumRating, request.Criteria?.Year, request.Criteria?.Episodes);
        }
    }
}
