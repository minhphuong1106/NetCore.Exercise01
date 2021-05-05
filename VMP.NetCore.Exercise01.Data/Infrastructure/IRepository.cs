using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VMP.NetCore.Exercise01.Common;
using VMP.NetCore.Exercise01.Model.Abstract;

namespace VMP.NetCore.Exercise01.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        List<TEntity> GetList();
        TEntity GetById(Guid id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(Guid id);
        TEntity HardDelete(Guid id);

        Task<List<TEntity>> GetListAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);
        Task<TEntity> HardDeleteAsync(Guid id);

        IEnumerable<TEntity> GetEnumerable(string[] includes = null);
        TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> expression, string[] includes = null);
        IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> predicate, out int total, int index = 0, int size = ProjectConstants.PagingConstants.PageSize, string[] includes = null);
        bool CheckContains(Expression<Func<TEntity, bool>> predicate);

        void DeleteMulti(Expression<Func<TEntity, bool>> where);
    }
    /*
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
    */
}