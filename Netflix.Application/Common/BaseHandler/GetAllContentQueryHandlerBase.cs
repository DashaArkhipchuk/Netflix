using MediatR;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Application.Common.Content;
using Netflix.Domain.DTOs.Common;

namespace Netflix.Application.Common.BaseHandler
{
    public abstract class GetAllContentQueryHandlerBase<TContentDto>
    : IRequestHandler<GetAllContentQuery<TContentDto>, PagedResult<TContentDto>> where TContentDto : class
    {
        protected readonly IContentByTypesRepository _contentRepository;

        protected GetAllContentQueryHandlerBase(IContentByTypesRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        protected abstract string ContentType { get; }

        public async Task<PagedResult<TContentDto>> Handle(GetAllContentQuery<TContentDto> request, CancellationToken cancellationToken)
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

            return new PagedResult<TContentDto> { Items = content.Items.Select(x => MapToDto(x)).ToList(), TotalCount = content.TotalCount };
        }

        protected abstract TContentDto MapToDto(ContentWithType content);
    }
}
