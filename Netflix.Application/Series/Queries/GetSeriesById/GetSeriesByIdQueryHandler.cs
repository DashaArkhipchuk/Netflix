using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Series.Queries.GetSeriesById
{
    public class GetSeriesByIdQueryHandler : IRequestHandler<GetContentByIdQuery<Domain.Series>, Domain.Series?>
    {
        private readonly ISeriesRepository _seriesRepository;

        public GetSeriesByIdQueryHandler(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public Task<Domain.Series?> Handle(GetContentByIdQuery<Domain.Series> request, CancellationToken cancellationToken)
        {
            return _seriesRepository.GetByIdAsync(request.Id);
        }
    }
}
