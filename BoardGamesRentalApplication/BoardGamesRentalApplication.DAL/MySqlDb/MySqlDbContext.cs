using BoardGamesRentalApplication.DAL.Models;
using MySql.Data.Entity;
using System.Data.Entity;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("BoardGamesRentalConnectionStrings")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
