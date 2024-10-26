using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public class News
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public NewsType Type { get; set; } 
        public string Description { get; set; }

        public Guid AuthorId { get; set; }
        public AuthorModel Author { get; set; } 

        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ArticleText { get; set; }
        public string ImageURL { get; set; }
    }
}
