using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.Repository;
using System;

namespace BoardGamesRentalApplication.DAL.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        void Save();
    }
}
