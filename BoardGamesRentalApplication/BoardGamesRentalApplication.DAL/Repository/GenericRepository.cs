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
        private readonly MySqlDbContext mySqlDbContext;
        private readonly DbSet<T> set;

        public GenericRepository(MySqlDbContext mySqlDbContext)
        {
            this.mySqlDbContext = mySqlDbContext;
            set = mySqlDbContext.Set<T>();
        }

        public bool Add(T entity)
        {
            set.Add(entity);
            return true;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            set.AddRange(entities);
            return true;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return set.Any(predicate);
        }

        public bool Edit(T entity)
        {
            mySqlDbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return set.Where(predicate);
        }

        public T FindById(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public bool Remove(T entity)
        {
            T entityToDelete = set.Find(entity);
            if (mySqlDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                set.Attach(entityToDelete);
            }
            set.Remove(entityToDelete);
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
                set.Attach(entity);
            }
            mySqlDbContext.Entry(entity).State = EntityState.Modified;
            mySqlDbContext.SaveChanges();
            return true;
        }
    }
}
