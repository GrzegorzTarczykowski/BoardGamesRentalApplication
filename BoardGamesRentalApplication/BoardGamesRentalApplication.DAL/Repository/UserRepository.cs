using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.MySqlDb;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Repository
{
    public class UserRepository : ARepository<User>
    {
        public UserRepository(MySqlDbContext mySqlDbContext, IUnitOfWork unitOfWork) : base(mySqlDbContext, unitOfWork) { }
    }
}
