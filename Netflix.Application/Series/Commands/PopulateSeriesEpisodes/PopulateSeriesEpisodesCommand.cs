using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Series.Commands.PopulateSeriesEpisodes
{
    public record PopulateSeriesEpisodesCommand
    (
        int id = 0
    ) : IRequest<string>;
}
