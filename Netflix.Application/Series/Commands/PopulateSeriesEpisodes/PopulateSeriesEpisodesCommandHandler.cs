using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Series.Commands.PopulateSeriesEpisodes
{
    internal class PopulateSeriesEpisodesCommandHandler : IRequestHandler<PopulateSeriesEpisodesCommand, string>
    {
        private readonly ISeriesRepository _seriesRepository;

        public PopulateSeriesEpisodesCommandHandler(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public Task<string> Handle(PopulateSeriesEpisodesCommand request, CancellationToken cancellationToken)
        {
            var str = _seriesRepository.PopulateSeriesEpisodes();
            return str;
        }
    }
}
