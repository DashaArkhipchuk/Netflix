using MediatR;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetAllNews
{
    internal class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, List<Netflix.Domain.Entities.News>>
    {
        private readonly INewsRepository _newsRepository;

        public GetAllNewsQueryHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public Task<List<Domain.Entities.News>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            return _newsRepository.GetAllAsync(request.Skip, request.Take, request.Criteria?.SortByLatest ?? false);
        }
    }
}
