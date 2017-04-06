using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.ResolveAnything;
using iassetTechnicalTest.Common.Dependencies;
namespace iassetTechnicalTest.Tests
{
    class DependencyConfig
    {

        public static void SetupDependencies()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Register our controller and service dependencies
            builder.RegisterModule(new DependencyModule());

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        

    
    }
}
