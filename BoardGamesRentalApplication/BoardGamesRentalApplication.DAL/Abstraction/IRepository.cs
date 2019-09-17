using System;

namespace BoardGamesRentalApplication.DAL.Abstraction
{
    public interface IRepository : IDisposable
    {
        bool SaveChanges();
    }
}
