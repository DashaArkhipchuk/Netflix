using MediatR;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Queries.GetNewsTypes
{
    public record GetNewsTypesQuery : IRequest<List<(NewsType Type, int Count)>>;
}
