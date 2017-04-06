using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using iassetTechnicalTest.ServiceAgent;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using iassetTechnicalTest.Services;
namespace iassetTechnicalTest.Common.Dependencies
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GlobalWeatherServiceAgent>().As<IGlobalWeatherServiceAgent>().InstancePerRequest();
            builder.RegisterType<GlobalWeatherServiceClient>().As<IGlobalWeatherServiceClient>().InstancePerRequest();
            builder.RegisterInstance(LogManager.GetLogger(typeof(ILog))).As<ILog>();
            base.Load(builder);
        }
    }
}