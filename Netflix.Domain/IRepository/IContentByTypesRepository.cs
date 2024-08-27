using Netflix.Domain.ContentWithTypeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface IContentByTypesRepository : IGenericRepository<ContentWithType>
    {
        public string Type {  get; set; }
    }
}
