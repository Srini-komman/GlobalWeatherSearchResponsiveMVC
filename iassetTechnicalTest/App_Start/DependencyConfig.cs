using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using iassetTechnicalTest.Common.Dependencies;

namespace iassetTechnicalTest
{
    public class DependencyConfig
    {
        public static void ConfigureAutofacContainer()
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