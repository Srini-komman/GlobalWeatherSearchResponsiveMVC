using Common.Logging;
using iassetTechnicalTest.Models;
using iassetTechnicalTest.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iassetTechnicalTest.Controllers
{
    /// <summary>
    /// Controller to interact with GLobal weather request
    /// </summary>
    public class WeatherController : Controller
    {
        private IGlobalWeatherServiceClient globalWeatherServiceClient;
        ILog mLog;

        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="globalWeatherServiceClient"></param>
        public WeatherController(IGlobalWeatherServiceClient globalWeatherServiceClient, ILog log)
        {
            this.globalWeatherServiceClient = globalWeatherServiceClient;
            this.mLog = log;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with internal client service called GlobalWeatherServiceClient
        /// 2. fetches list of cities by inputted country from the user
        /// 3. returns the data in json format
        /// </summary>
        /// <param name="countryname"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCities(string countryname)
        {
            ContentResult result = null;
            try
            {
                string strCities = globalWeatherServiceClient.GetCitiesByCountry(countryname);
                result = Content(strCities, "application/json");
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException.Message);
                Response.Write(ex.InnerException.Message);
            }

            return result;
        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with internal client service called GlobalWeatherServiceClient
        /// 2. fetches weather details by inputted country and the city from the user
        /// 3. returns the data in json format
        /// </summary>
        /// <param name="countryname"></param>
        /// <param name="cityname"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetWeather(string countryname, string cityname)
        {
            ContentResult result = null;
            try
            {
                Weather weather = globalWeatherServiceClient.GetWeatherDetails(cityname, countryname);
                result = Content(JsonConvert.SerializeObject(weather), "application/json");
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException.Message);
                Response.Write(ex.InnerException.Message);
            }
            return result;
        }
    }
}