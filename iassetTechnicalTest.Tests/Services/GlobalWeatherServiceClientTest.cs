using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Logging;
using iassetTechnicalTest.ServiceAgent;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using iassetTechnicalTest.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
namespace iassetTechnicalTest.Tests.Services
{
    [TestClass]
    public class GlobalWeatherServiceClientTest
    {

        ILog mLog = null;
        IGlobalWeatherServiceAgent globalWeatherServiceAgent = null;
        IGlobalWeatherServiceClient globalWeatherServiceClient = null;

        public GlobalWeatherServiceClientTest()
        {
            mLog = LogManager.GetLogger(typeof(ILog));
            globalWeatherServiceAgent = new GlobalWeatherServiceAgent(mLog);
            globalWeatherServiceClient = new GlobalWeatherServiceClient(globalWeatherServiceAgent, mLog);
        }

        [TestMethod]
        public void TestGetCitiesByCountry()
        {
            string strJsonText = globalWeatherServiceClient.GetCitiesByCountry("Australia");
            Assert.IsNotNull(strJsonText);
            Assert.IsTrue(strJsonText.Trim().Length > 0, "The message length must be greater than zero");
            dynamic jsonObjectCities = JsonConvert.DeserializeObject(strJsonText);
            Assert.IsNotNull(jsonObjectCities);
            Assert.IsTrue(jsonObjectCities.Count > 0);
            Assert.IsTrue(jsonObjectCities[0].Value.Trim().Length > 0);
        }

        [TestMethod]
        public void TestGetWeatherFromExternalAPI()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Sydney,Australia&APPID=bf6bcc8506d04497f31ebfdac89cd10f";
            var jsonTask = globalWeatherServiceClient.GetWeatherFromExternalAPI("Sydney Airport", "Australia");
            //var jsonTask = globalWeatherServiceClient.GetWeatherFromExternalAPI(HttpUtility.UrlEncode(url));
            Assert.IsNotNull(jsonTask);
            Assert.IsNotNull(jsonTask.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(jsonTask.Result.Location));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(jsonTask.Result.Temperature));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(jsonTask.Result.Visibility));
        }
    }
}
