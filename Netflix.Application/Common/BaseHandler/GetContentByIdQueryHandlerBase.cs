using MediatR;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Common.Content;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.BaseHandler
{
    public abstract class GetContentByIdQueryHandlerBase<TContentDto>
    : IRequestHandler<GetContentByIdQuery<TContentDto>, TContentDto?> where TContentDto : class
    {
        protected readonly IContentByTypesRepository _contentRepository;

        protected GetContentByIdQueryHandlerBase(IContentByTypesRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        protected abstract string ContentType { get; }

        public async Task<TContentDto?> Handle(GetContentByIdQuery<TContentDto> request, CancellationToken cancellationToken)
        {
            _contentRepository.Type = ContentType;

            var content = await _contentRepository.GetByIdAsync(request.Id);

            if (content == null)
            {
                return null;
            }

            return MapToDto(content);
        }

        protected abstract TContentDto MapToDto(ContentWithType content);
    }
}
