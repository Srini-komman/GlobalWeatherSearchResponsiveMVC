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
    public class WeatherController : Controller
    {
        private IGlobalWeatherServiceClient globalWeatherServiceClient;
        
        
        public WeatherController(IGlobalWeatherServiceClient globalWeatherServiceClient)
        {
            this.globalWeatherServiceClient = globalWeatherServiceClient;
        }

        public WeatherController()
        {
            Console.Write("");
        }
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cities(string countryname)
        {
            ContentResult result = null;
            try
            {
                string strCities = globalWeatherServiceClient.GetCitiesByCountry(countryname);
                result = Content(strCities, "application/json");
            }
            catch(Exception ex)
            {
                Response.Write(ex.InnerException.Message);
            }

            return result;
        }

        public ActionResult GetWeather(string countryname, string cityname)
        {
            ContentResult result = null;
            try {
                Weather weather = globalWeatherServiceClient.GetWeatherDetails(cityname, countryname);
                result = Content(JsonConvert.SerializeObject(weather), "application/json");
            }
            catch(Exception ex)
            {
                Response.Write(ex.InnerException.Message);
            }
            return result;
        }
    }
}