using MediatR;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Application.Common.Content;

namespace Netflix.Application.Common.BaseHandler
{
    public abstract class GetAllContentQueryHandlerBase<TContentDto>
    : IRequestHandler<GetAllContentQuery<TContentDto>, List<TContentDto>> where TContentDto : class
    {
        protected readonly IContentByTypesRepository _contentRepository;

        protected GetAllContentQueryHandlerBase(IContentByTypesRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        protected abstract string ContentType { get; }

        public async Task<List<TContentDto>> Handle(GetAllContentQuery<TContentDto> request, CancellationToken cancellationToken)
        {
            _contentRepository.Type = ContentType;

            List<string> genres = request.Criteria?.Genre is not null ? request.Criteria.Genre : new List<string>();

            var content = await _contentRepository.GetAllAsync(
                request.Skip,
                request.Take,
                genres,
                request.Criteria?.SortByLatest ?? false,
                request.Criteria?.MinimumRating,
                request.Criteria?.Year,
                request.Criteria?.Episodes
            );

            return content.Select(x => MapToDto(x)).ToList();
        }

        protected abstract TContentDto MapToDto(Netflix.Domain.ContentWithTypeType.ContentWithType content);
    }
}
