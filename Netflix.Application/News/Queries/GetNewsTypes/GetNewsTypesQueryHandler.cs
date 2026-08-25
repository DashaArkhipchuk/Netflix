using MediatR;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetNewsTypes
{
    internal class GetNewsTypesQueryHandler(INewsRepository newsRepository)
    : IRequestHandler<GetNewsTypesQuery, List<(NewsType Type, int Count)>>
    {
        public Task<List<(NewsType Type, int Count)>> Handle(GetNewsTypesQuery request, CancellationToken ct)
        {
            return newsRepository.GetAllNewsTypesWithCountsAsync(ct);
        }
    }
}
