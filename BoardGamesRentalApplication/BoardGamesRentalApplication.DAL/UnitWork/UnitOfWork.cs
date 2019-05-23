using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.MySqlDb;
using BoardGamesRentalApplication.DAL.Repository;

namespace BoardGamesRentalApplication.DAL.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MySqlDbContext mySqlDbContext;
        private IGenericRepository<User> userRepository;

        public UnitOfWork()
        {
            this.mySqlDbContext = new MySqlDbContext();
        }

        public IGenericRepository<User> UserRepository
        {
            get { return this.userRepository ?? (this.userRepository = new GenericRepository<User>(mySqlDbContext)); }
        }

        public void Save()
        {
            mySqlDbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    mySqlDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
