using BoardGamesRentalApplication.DAL.Abstraction;
using System;

namespace BoardGamesRentalApplication.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Register(IRepository repository);
        void SaveChanges();
    }
}
