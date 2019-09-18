using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.MySqlDb
{
    public class MyMigrationSQLGenerator : MySqlMigrationSqlGenerator
    {
        protected override MigrationStatement Generate(CreateIndexOperation op)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                return base.Generate(op);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }
    }
}
