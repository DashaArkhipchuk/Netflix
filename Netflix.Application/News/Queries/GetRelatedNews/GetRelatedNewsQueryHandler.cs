using MediatR;
using Netflix.Domain.DTOs.Common;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetRelatedNews
{
    internal class GetRelatedNewsQueryHandler(INewsRepository newsRepository)
    : IRequestHandler<GetRelatedNewsQuery, PagedResult<Netflix.Domain.Entities.News>>
    {
        public Task<PagedResult<Netflix.Domain.Entities.News>> Handle(GetRelatedNewsQuery request, CancellationToken ct) =>
            newsRepository.GetRelatedNewsAsync(request.NewsId, request.Skip, request.Take, ct); 
    }
}
