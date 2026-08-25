using MediatR;
using Netflix.Domain.DTOs.Common;
using Netflix.Domain.DTOs.News;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetAllNews
{
    internal class GetAllNewsQueryHandler(INewsRepository newsRepository)
    : IRequestHandler<GetAllNewsQuery, PagedResult<Netflix.Domain.Entities.News>>
    {
        public Task<PagedResult<Netflix.Domain.Entities.News>> Handle(GetAllNewsQuery request, CancellationToken ct)
        {
            var filter = new NewsFilter
            {
                TypeId = request.Criteria?.TypeId,
                Search = request.Criteria?.Search,
                SortBy = request.Criteria is not null ? (NewsSortOption)request.Criteria.SortBy : NewsSortOption.Latest
            };

            return newsRepository.GetAllAsync(request.Skip, request.Take, filter, ct);
        }
    }
}
