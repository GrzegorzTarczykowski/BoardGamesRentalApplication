using Autofac;
using BoardGamesRentalApplication.BLL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).AsImplementedInterfaces().InstancePerRequest();
        }
    }
}