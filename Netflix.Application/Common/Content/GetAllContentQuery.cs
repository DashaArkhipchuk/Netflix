using MediatR;
using Netflix.Domain;
using System.Reflection;

namespace Netflix.Application.Common.Content
{
    public record GetAllContentQuery<T>
    (

        int Skip = 0,
        int Take = 10,
        QueryCriteria? Criteria = null
    ) : IRequest<List<T>> where T : class;
}
