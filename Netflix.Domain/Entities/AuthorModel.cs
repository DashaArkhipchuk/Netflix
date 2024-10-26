using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.Entities
{
    public partial class AuthorModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public virtual ICollection<News> NewsCollection { get; set; } = new List<News>();

        public override string? ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
