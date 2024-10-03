using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.GetAllContentQueryWithoutFiltering
{
    public record GetAllContentWithoutFilteringQuery<T>
        (
        int Skip = 0,
        int Take = 10
    ) : IRequest<List<T>> where T: class;
}
