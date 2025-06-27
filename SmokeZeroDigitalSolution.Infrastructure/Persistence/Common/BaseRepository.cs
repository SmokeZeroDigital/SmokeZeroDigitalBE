using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;

namespace SmokeZeroDigitalSolution.Infrastructure.Persistence.Common
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        Task<T> FindAsync(TKey id); //get by id
        IQueryable<T> GetAll(); //get all
        IQueryable<T> Get(Expression<Func<T, bool>> where); //get by condition
        IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes); //get by condition with include
        IQueryable<T> Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //get by condition with include ,can use nested include
        Task AddAsync(T entity); //add entity
        Task AddRangce(IEnumerable<T> entities); //add list entity
        void Update(T entity); //update entity
        Task<bool> Remove(TKey id); //remove entity by id
        void Remove(T entity); //remove entity
        Task Remove(Expression<Func<T, bool>> where); //remove entity by condition
        void Remove(IEnumerable<T> list); //remove list entity
        object Max(Expression<Func<T, object>> selector); //get max value of column
        Task<bool> CheckExist(Expression<Func<T, bool>> where); //check exist entity by condition
    }
    public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey> where T : class
    {
        private ApplicationDbContext _applicationDbContext;
        private DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            dbSet = _applicationDbContext.Set<T>();
        }
        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangce(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<T> FindAsync(Tkey id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = dbSet;
            if (where != null)
            {
                result = dbSet.Where(where);
            }
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> result = dbSet;
            if (where != null)
            {
                result = dbSet.Where(where);
            }

            if (include != null)
            {
                result = include(result);
            }
            return result;
        }

        public virtual async Task<bool> Remove(Tkey id)
        {
            T entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            dbSet.Remove(entity);
            return true;
        }

        public virtual void Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> CheckExist(Expression<Func<T, bool>> where)
        {
            return await dbSet.AnyAsync(where);
        }

        public async Task Remove(Expression<Func<T, bool>> where)
        {
            var entities = dbSet.Where(where);
            dbSet.RemoveRange(entities);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public object Max(Expression<Func<T, object>> selector)
        {
            return dbSet.Max(selector);
        }

        public void Remove(IEnumerable<T> list)
        {
            dbSet.RemoveRange(list);
        }
    }
}
