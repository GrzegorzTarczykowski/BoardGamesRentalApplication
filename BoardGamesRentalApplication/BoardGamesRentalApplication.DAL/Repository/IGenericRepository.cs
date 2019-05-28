using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BoardGamesRentalApplication.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool Edit(T entity);
        bool Save();
        bool SaveChanges(T entity);
    }
}
