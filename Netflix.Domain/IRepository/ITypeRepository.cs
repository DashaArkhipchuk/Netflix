using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface ITypeRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(int skip, int take);
        Task<T?> GetTypeById(Guid id);
    }
}
