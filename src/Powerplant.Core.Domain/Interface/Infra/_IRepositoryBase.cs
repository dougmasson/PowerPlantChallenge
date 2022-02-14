using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Powerplant.Core.Domain.Interface.Infra.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAsync();

        Task<TEntity> FindByIdAsync(int id);

        Task<IEnumerable<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> AddAsync(TEntity category);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Dispose();
    }
}
