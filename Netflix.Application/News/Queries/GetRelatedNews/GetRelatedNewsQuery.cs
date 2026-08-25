using MediatR;
using Netflix.Domain.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetRelatedNews
{
    public record GetRelatedNewsQuery(Guid NewsId, int Skip = 0, int Take = 5) : IRequest<PagedResult<Netflix.Domain.Entities.News>>;
}
