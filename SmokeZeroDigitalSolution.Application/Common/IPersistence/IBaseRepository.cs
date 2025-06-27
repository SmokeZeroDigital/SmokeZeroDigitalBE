using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace SmokeZeroDigitalSolution.Application.Common.IPersistence
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
}
