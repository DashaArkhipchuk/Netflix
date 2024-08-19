using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Common
{
    public class Criteria
    {
        public List<string> Genre { get; set; }  = [];
        public decimal? MinimumRating { get; set; }
        public bool SortByLatest { get; set; }
        public int? Year { get; set; }
        public int? Episodes { get; set; }

    }
}
