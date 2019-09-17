using BoardGamesRentalApplication.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Test.Mocks
{
    class MockGenericRepository<T> : IRepository<T> where T: class
    {
        List<T> data;
        public MockGenericRepository()
        {
            data = new List<T>();
        }

        public bool Add(T entity)
        {
            data.Add(entity);
            return true;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            data.AddRange(entities);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return data.AsQueryable().Any(predicate);
        }

        public bool Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return data.AsQueryable().Where(predicate);
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return data.AsQueryable();
        }

        public bool Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
