using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T>
        {
            Task<IReadOnlyList<T>> GetAllAsync();
            Task<T?> GetEntity(Expression<Func<T, bool>> e);
            Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec);
            Task<int> CountAsync(ISpecifications<T> spec);
            void Add(T entity);
            void Update(T entity);
            void Delete(T entity);
        }
    }
}
