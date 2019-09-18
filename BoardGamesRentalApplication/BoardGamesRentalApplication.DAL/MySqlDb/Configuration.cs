using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    public sealed class Configuration : DbMigrationsConfiguration<MySqlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MyMigrationSQLGenerator());
        }
    }
}
