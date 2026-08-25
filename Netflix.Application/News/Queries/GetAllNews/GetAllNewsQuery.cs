using MediatR;
using Netflix.Application.News.Common;
using Netflix.Domain.DTOs.Common;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetAllNews
{
    public record GetAllNewsQuery
    (

        int Skip = 0,
        int Take = 10,
        QueryCriteriaNews? Criteria = null
    ) : IRequest<PagedResult<Netflix.Domain.Entities.News>>;
}
