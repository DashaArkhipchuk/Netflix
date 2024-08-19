using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid? id);
        Task<List<T>> GetAllAsync(int skip, int take, List<string> genre, bool sortByLatest = false, decimal? minimumRating = null, int? year = null, int? episodes = null);

        //Task<int> AddAsync(T entity);
        //Task<int> UpdateAsync(T entity);
        //Task<int> DeleteAsync(Guid id);
        //Task<bool> ExistsAsync(Guid id);
        //Task<IReadOnlyList<T>> GetProcessedAsync(Processable<T> processable);
        //Task<int> GetProcessedCountAsync(Processable<T> processable);
    }
}
