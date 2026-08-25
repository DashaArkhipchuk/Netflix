using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.News
{
    public class ShortNewsDto
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Authors { get; set; } = Enumerable.Empty<string>();
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}
