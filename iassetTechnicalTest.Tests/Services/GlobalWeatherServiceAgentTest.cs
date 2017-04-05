using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Logging;
using iassetTechnicalTest.ServiceAgent;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using iassetTechnicalTest.Services;

namespace iassetTechnicalTest.Tests.ServiceAgent
{
    [TestClass]
    public class GlobalWeatherServiceAgentTest
    {
        ILog mLog = null;
        IGlobalWeatherServiceAgent globalWeatherServiceAgent = null;
        public GlobalWeatherServiceAgentTest()
        {
            mLog = LogManager.GetLogger(typeof(ILog));
            globalWeatherServiceAgent = new GlobalWeatherServiceAgent(mLog);
        }
        
        [TestMethod]
        public void TestGetCitiesByCountry()
        {
            string citiesxml = globalWeatherServiceAgent.GetCitiesByCountry("Australia");
            Assert.IsNotNull(citiesxml);
            Assert.IsTrue(citiesxml.Trim().Length > 0, "The message length must be greater than zero");
        }

        [TestMethod]
        public void TestGetWeather()
        {
            string weatherxml = globalWeatherServiceAgent.GetWeather("Sydney Airport", "Australia");
            Assert.IsNotNull(weatherxml);
            Assert.IsTrue(weatherxml.Trim().Length > 0, "The message length must be greater than zero");
            Assert.IsTrue(weatherxml != "Data Not Found");
        }

        [TestMethod]
        public void TestNegativeCases()
        {
            try
            {
                string citiesxml = globalWeatherServiceAgent.GetCitiesByCountry("");
                Assert.Fail("Should not be reached.");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(true, ex.Message);
            }

            try{
                string weatherxml = globalWeatherServiceAgent.GetWeather("", "");
                Assert.Fail("Should not be reached.");
            } 
            catch(ArgumentException ex)
            {
                Assert.IsTrue(true, ex.Message);
            }

        }
    }
}
