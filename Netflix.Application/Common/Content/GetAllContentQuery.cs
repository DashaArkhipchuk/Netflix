using MediatR;
using Netflix.Domain;
using Netflix.Domain.DTOs.Common;
using System.Reflection;

namespace Netflix.Application.Common.Content
{
    public record GetAllContentQuery<T>
    (

        int Skip = 0,
        int Take = 10,
        QueryCriteria? Criteria = null
    ) : IRequest<PagedResult<T>> where T : class;
}
