using MediatR;
using Netflix.Domain.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Content
{
    public record GetAllContentWithOptionalPaginationQuery<T>
    (

        int? Take,
        int Skip = 0,
        QueryCriteria? Criteria = null
    ) : IRequest<PagedResult<T>> where T : class;
}
