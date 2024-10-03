using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface IAuditionRepository
    {
        Task<Audition?> GetByIdAsync(Guid? id);

        void Add(Audition audition);
        Task<bool> Remove(Guid auditionId);
    }
}
