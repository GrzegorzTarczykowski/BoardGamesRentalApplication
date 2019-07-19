using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.Repository;
using System;

namespace BoardGamesRentalApplication.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        void Save();
    }
}
