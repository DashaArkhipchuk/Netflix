using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.DTOs.Common
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
