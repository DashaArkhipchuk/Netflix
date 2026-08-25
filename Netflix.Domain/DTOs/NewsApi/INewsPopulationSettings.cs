using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.DTOs.NewsApi
{
    // Abstraction so Application never sees IOptions<T> (that's a Microsoft.Extensions.Options / Infrastructure concern, not a Application one).
    public interface INewsPopulationSettings
    {
        int DefaultPages { get; }
        int DefaultPageSize { get; }
        int MaxScrapeConcurrency { get; }
    }
}
