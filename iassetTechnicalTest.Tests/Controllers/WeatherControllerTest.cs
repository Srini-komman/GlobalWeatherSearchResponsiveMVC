using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iassetTechnicalTest;
using iassetTechnicalTest.Controllers;
using iassetTechnicalTest.Models;
using iassetTechnicalTest.Services;
using iassetTechnicalTest.ServiceAgent;
using Common.Logging;

namespace iassetTechnicalTest.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest : UnitTestBase
    {
        private ILog mLog = null;
        private IGlobalWeatherServiceClient globalWeatherServiceClient = null;
        private WeatherController weatherController = null;
        public WeatherControllerTest()
        {
            this.mLog = LogManager.GetLogger(typeof(ILog));
            this.globalWeatherServiceClient = new GlobalWeatherServiceClient(new GlobalWeatherServiceAgent(mLog), mLog);
            this.weatherController = new WeatherController(globalWeatherServiceClient, mLog);
        }
        [TestMethod]
        public void TestIndex()
        {
            //Act
            ViewResult result = weatherController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetCities()
        {
            // Act
            ActionResult result = weatherController.GetCities("Australia") as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetWeather()
        {
            //Act
            ActionResult result = weatherController.GetWeather("Australia", "Sydney Airport") as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
