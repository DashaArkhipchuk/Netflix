using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetNewsById
{
    internal class GetNewsByIdQueryHandler : IRequestHandler<GetContentByIdQuery<Domain.Entities.News>, Domain.Entities.News?>
    {
        private readonly INewsRepository _newsRepository;

        public GetNewsByIdQueryHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<Domain.Entities.News?> Handle(GetContentByIdQuery<Domain.Entities.News> request, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.GetByIdAsync(request.Id, cancellationToken);

            if (news is not null)
                await _newsRepository.IncrementViewCountAsync(request.Id, cancellationToken);

            return news;
        }
    }
}
