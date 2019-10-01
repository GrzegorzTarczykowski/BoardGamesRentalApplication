using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.MySqlDb;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MySqlDbContext mySqlDbContext;
        private readonly DbSet<T> set;

        public Repository(MySqlDbContext mySqlDbContext, IUnitOfWork unitOfWork)
        {
            this.mySqlDbContext = mySqlDbContext;
            set = mySqlDbContext.Set<T>();
            unitOfWork.Register(this);
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

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return set.Where(predicate);
        }

        public T FindById(int id)
        {
            return set.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return set;
        }

        public IQueryable<T> GetAll(params string[] includeProperties)
        {
            return includeProperties.Aggregate(set.AsQueryable(), (query, path) => query.Include(path));
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

        public bool Remove(object[] keyValues)
        {
            T entityToDelete = set.Find(keyValues);
            if (mySqlDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                set.Attach(entityToDelete);
            }
            set.Remove(entityToDelete);
            return true;
        }

        public bool SaveChanges()
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

        public void Dispose()
        {
            mySqlDbContext.Dispose();
        }
    }
}
