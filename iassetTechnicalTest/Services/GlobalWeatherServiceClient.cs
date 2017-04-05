using Common.Logging;
using iassetTechnicalTest.Common;
using iassetTechnicalTest.Helpers;
using iassetTechnicalTest.Models;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using System;
using System.Configuration;
using System.Net.Http;

namespace iassetTechnicalTest.Services
{
    /// <summary>
    /// client side service to interact with external and internal webapi and webservice calls
    /// </summary>
    public class GlobalWeatherServiceClient : IGlobalWeatherServiceClient
    {
        public IGlobalWeatherServiceAgent globalWeatherServiceAgent;
        private ILog mLog;

        /// <summary>
        /// Dependency injection to make sure the service is testable
        /// </summary>
        /// <param name="globalWeatherServiceAgent"></param>
        /// <param name="log"></param>
        public GlobalWeatherServiceClient(IGlobalWeatherServiceAgent globalWeatherServiceAgent, ILog log)
        {
            this.globalWeatherServiceAgent = globalWeatherServiceAgent;
            this.mLog = log;
        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with internal serviceagent called globalWeatherServiceAgent
        /// 2. fetches list of cities by inputted country
        /// 3. converts the soap xml message to json string
        /// </summary>
        /// <param name="strcountryName">name of the country</param>
        /// <returns>list of cities as json text format</returns>
        public string GetCitiesByCountry(string strcountryName)
        {
            mLog.Info("Calling GlobalWeatherServiceClient.GetCitiesByCountry...");

            try
            {
                string strCities = globalWeatherServiceAgent.GetCitiesByCountry(strcountryName);
                return GlobalWeatherHelper.ConvertXMLToJSON(strCities);
            }
            catch(Exception ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }
        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with internal serviceagent called globalWeatherServiceAgent
        /// 2. fetches weather details in the format of soap xml
        /// 3. converts the soap xml message to an object
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strcountryName"></param>
        /// <returns>returns weather details</returns>
        [System.Obsolete("GetWeather is deprecated due to globalweather.GetWeather is not rturning any dat, please use GetWeatherFromExternalAPI instead.")]
        public string GetWeather(string strCityName, string strcountryName)
        {
            
            mLog.Info("Calling GlobalWeatherServiceClient.GetWeather...");
            try
            {
                string strWeather = globalWeatherServiceAgent.GetWeather(strCityName, strcountryName);
                //TODO: Populate data with Weather model object when the service is ready to functioning
                return strWeather;
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }
        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with external web api called OpenWeather
        /// 2. fetches weather details in the format of json text
        /// 3. Maps the json data with model object called Weather
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strcountryName"></param>
        /// <returns>Returns model obejct Weather</returns>
        public async System.Threading.Tasks.Task<Weather> GetWeatherFromExternalAPI(string cityName, string countryname)
        {
            Weather weather = null;
            try
            {
                string apiKey = ConfigurationManager.AppSettings[Constants.OpenWeather_API_KEY];
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    //Replace any white spaces in the value of input parameters
                    countryname = System.Text.RegularExpressions.Regex.Replace(countryname, @"\s+", "%20");
                    cityName = System.Text.RegularExpressions.Regex.Replace(cityName, @"\s+", "%20");

                    //Construct url with registered API Key, valid key has to be configured in the web.config file
                    string url = string.Format("{0}{1},{2}{3}{4}", "?q=", cityName, countryname, "&APPID=", apiKey);
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings[Constants.OpenWeather_URI]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    var jsonString = await client.GetStringAsync(url).ConfigureAwait(false);
                    weather = GlobalWeatherHelper.PopulateWeatherModelFromJsonText(jsonString);
                }
            }
            catch(HttpRequestException ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }
            return weather;
        }

        /// <summary>
        /// It returns the model object weather 
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="countryname"></param>
        /// <returns></returns>
        public Weather GetWeatherDetails(string cityName, string countryname)
        {
            try
            {
                var jsonTask = GetWeatherFromExternalAPI(cityName, countryname);
                return jsonTask.Result;
            }
            catch(Exception ex)
            {
                mLog.Error(ex.InnerException.Message);
                throw ex;
            }
        }
    }
}