using MediatR;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Content
{
    public record GetContentByIdQuery<T>
    (
        Guid Id
    ) : IRequest<T> where T : class;
}
