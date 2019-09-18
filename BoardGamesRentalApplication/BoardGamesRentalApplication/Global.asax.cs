using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BoardGamesRentalApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
