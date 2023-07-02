using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Game.DataAccess.Entity;

namespace Game.DataAccess.Repository
{
    public interface IRepository<T> where T : AbstractEntity
    {
        T Find(string id);
        IEnumerable<T> All { get; }
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Update(Expression<Func<T, bool>> predicate, T entity);
        Task UpdateAsync(Expression<Func<T, bool>> predicate, T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}