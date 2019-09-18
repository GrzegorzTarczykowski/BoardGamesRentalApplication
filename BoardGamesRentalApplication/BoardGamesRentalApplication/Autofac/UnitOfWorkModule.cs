using Autofac;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Autofac
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
        }
    }
}