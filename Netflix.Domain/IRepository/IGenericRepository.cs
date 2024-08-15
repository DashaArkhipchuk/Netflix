﻿namespace Netflix.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<T> GetByIdAsync(Guid? id);
        Task<IReadOnlyList<T>> GetAllAsync(int skip, int take);

        //Task<int> AddAsync(T entity);
        //Task<int> UpdateAsync(T entity);
        //Task<int> DeleteAsync(Guid id);
        //Task<bool> ExistsAsync(Guid id);
        //Task<IReadOnlyList<T>> GetProcessedAsync(Processable<T> processable);
        //Task<int> GetProcessedCountAsync(Processable<T> processable);
    }
}
