using Common.Logging;
using iassetTechnicalTest.ServiceAgent;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using iassetTechnicalTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace iassetTechnicalTest.Controllers
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            
            ILog mLog = LogManager.GetLogger(typeof(ILog));
            IGlobalWeatherServiceAgent globalWeatherServiceAgent = new GlobalWeatherServiceAgent(mLog);
            IGlobalWeatherServiceClient clientService = new GlobalWeatherServiceClient(globalWeatherServiceAgent, mLog);
            IController controller = Activator.CreateInstance(controllerType, new[] {clientService}) as Controller;
            return controller;
         
        }
    }
}