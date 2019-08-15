using Autofac;
using BoardGamesRentalApplication.DAL.MySqlDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Autofac
{
    public class DbContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MySqlDbContext>().AsSelf().InstancePerRequest();
        }
    }
}