using BoardGamesRentalApplication.DAL.MySqlDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Repository
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MySqlDbContext mySqlDbContext;

        public GenericRepository(MySqlDbContext mySqlDbContext)
        {
            this.mySqlDbContext = mySqlDbContext;
        }

        public bool Add(T entity)
        {
            mySqlDbContext.Set<T>().Add(entity);
            return true;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            mySqlDbContext.Set<T>().AddRange(entities);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return mySqlDbContext.Set<T>().Any(predicate);
        }

        public bool Edit(T entity)
        {
            mySqlDbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = mySqlDbContext.Set<T>().Where(predicate);
            return query.AsEnumerable();
        }

        public T FindById(int id)
        {
            return mySqlDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = mySqlDbContext.Set<T>();
            return query.AsEnumerable();
        }

        public bool Remove(T entity)
        {
            T entityToDelete = mySqlDbContext.Set<T>().Find(entity);
            if (mySqlDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                mySqlDbContext.Set<T>().Attach(entityToDelete);
            }
            mySqlDbContext.Set<T>().Remove(entityToDelete);
            return true;

        }

        public bool Save()
        {
            mySqlDbContext.SaveChanges();
            return true;
        }

        public bool SaveChanges(T entity)
        {
            if (mySqlDbContext.Entry(entity).State == EntityState.Detached)
            {
                mySqlDbContext.Set<T>().Attach(entity);
            }
            mySqlDbContext.Entry(entity).State = EntityState.Modified;
            mySqlDbContext.SaveChanges();
            return true;
        }
    }
}
