using System;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    public class MySqlDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<MySqlDbContext>
    {
        protected override void Seed(MySqlDbContext context)
        {
        }
    }
}
