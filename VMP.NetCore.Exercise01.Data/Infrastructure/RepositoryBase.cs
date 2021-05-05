using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VMP.NetCore.Exercise01.Common;
using VMP.NetCore.Exercise01.Model.Abstract;

namespace VMP.NetCore.Exercise01.Data.Infrastructure
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
      where TEntity : class, IBaseEntity
      where TContext : DbContext
    {
        private readonly TContext _DataContext;
        public RepositoryBase(TContext dataContext)
        {
            this._DataContext = dataContext;
        }

        private void SetAddNewAuditable(TEntity entity)
        {
            if (entity != null)
            {
                if (string.IsNullOrEmpty(entity.InsBy))
                    entity.InsBy = ProjectConstants.DBConstants.DefaultUser;
                entity.InsAt = DateTime.UtcNow;
            }
        }

        private void SetUpdateAuditable(TEntity entity)
        {
            if (entity != null)
            {
                if (string.IsNullOrEmpty(entity.UpdBy))
                    entity.UpdBy = ProjectConstants.DBConstants.DefaultUser;
                entity.UpdAt = DateTime.UtcNow;
            }
        }

        public TEntity Add(TEntity entity)
        {
            _DataContext.Set<TEntity>().Add(entity);
            _DataContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _DataContext.Set<TEntity>().Add(entity);
            await _DataContext.SaveChangesAsync();
            return entity;
        }

        public TEntity Delete(Guid id)
        {
            TEntity entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                SetUpdateAuditable(entity);
                return Update(entity);
            }
            return null;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            TEntity entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                SetUpdateAuditable(entity);
                return await UpdateAsync(entity);
            }
            return null;
        }

        public TEntity HardDelete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return entity;
            }
            _DataContext.Set<TEntity>().Remove(entity);
            _DataContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> HardDeleteAsync(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return entity;
            }
            _DataContext.Set<TEntity>().Remove(entity);
            await _DataContext.SaveChangesAsync();
            return entity;
        }


        public List<TEntity> GetList()
        {
            return _DataContext.Set<TEntity>().ToList();
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            return await _DataContext.Set<TEntity>().ToListAsync();
        }

        public TEntity GetById(Guid id)
        {
            return _DataContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _DataContext.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Modified;
            _DataContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _DataContext.Entry(entity).State = EntityState.Modified;
            await _DataContext.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<TEntity> GetEnumerable(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _DataContext.Set<TEntity>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }
            return _DataContext.Set<TEntity>().AsQueryable();
        }

        public TEntity GetSingleByCondition(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _DataContext.Set<TEntity>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _DataContext.Set<TEntity>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<TEntity> GetMulti(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _DataContext.Set<TEntity>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<TEntity>(predicate).AsQueryable<TEntity>();
            }

            return _DataContext.Set<TEntity>().Where<TEntity>(predicate).AsQueryable<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> predicate, out int total, int index = 0, int size = ProjectConstants.PagingConstants.PageSize, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<TEntity> resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _DataContext.Set<TEntity>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                resetSet = predicate != null ? query.Where<TEntity>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                resetSet = predicate != null ? _DataContext.Set<TEntity>().Where<TEntity>(predicate).AsQueryable() : _DataContext.Set<TEntity>().AsQueryable();
            }

            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<TEntity, bool>> predicate)
        {
            return _DataContext.Set<TEntity>().Count<TEntity>(predicate) > 0;
        }
        
        public virtual void DeleteMulti(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = GetMulti(where);
            foreach (TEntity obj in objects)
                _DataContext.Set<TEntity>().Remove(obj);
            _DataContext.SaveChanges();
        }
        /*
        #region Properties

        private ProjectDbContext dataContext;

        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ProjectDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

        #endregion Properties

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation

        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            dbSet.Remove(entity);
            return entity;
        }

        public virtual T Delete(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            return entity;
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return dataContext.Set<T>().AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dataContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dataContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dataContext.Set<T>().Where<T>(predicate).AsQueryable() : dataContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        #endregion Implementation
        */
    }
}