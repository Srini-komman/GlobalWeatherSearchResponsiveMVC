using Common.Logging;
using iassetTechnicalTest.ServiceAgent.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iassetTechnicalTest.ServiceAgent
{
    /// <summary>
    /// Service to interact with external webservice called globalweather
    /// </summary>
    public class GlobalWeatherServiceAgent : IGlobalWeatherServiceAgent
    {
        private ILog mLog;
        
        public GlobalWeatherServiceAgent(ILog log)
        {
            mLog = log;
        }
        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with external webservice called globalweather
        /// 2. fetches list of cities in the format of soap xml
        /// </summary>
        /// <param name="strcountryName"></param>
        /// <returns>returns list of cities in the format of soap xml</returns>
        public string GetCitiesByCountry(string strcountryName)
        {
            mLog.Info("Calling GlobalWeatherServiceAgent.GetCitiesByCountry...");

            try
            {
                if (string.IsNullOrWhiteSpace(strcountryName))
                {
                    mLog.Error("Variable country is empty.");
                    throw new ArgumentException("The country must not be empty");
                }

                GlobalWeatherService.GlobalWeather globalWeather = new GlobalWeatherService.GlobalWeather();
                string strCities = globalWeather.GetCitiesByCountry(strcountryName);
                return strCities;
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }

        }

        /// <summary>
        /// Implements the following business logic:
        /// 1. Interact with external webservice called globalweather
        /// 2. fetches weather details in the format of soap xml
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strCountryName"></param>
        /// <returns>wether details</returns>
        public string GetWeather(string strCityName, string strCountryName)
        {
            //[System.Obsolete("GetWeather is deprecated due to globalweather.GetWeather is not rturning any data")]
            mLog.Info("Calling GlobalWeatherServiceAgent.GetWeather...");

            try
            {
                if (string.IsNullOrWhiteSpace(strCityName))
                {
                    mLog.Error("Variable strCityName is empty.");
                    throw new ArgumentException("The strCityName must not be empty");
                }

                if (string.IsNullOrWhiteSpace(strCountryName))
                {
                    mLog.Error("Variable strCountryName is empty.");
                    throw new ArgumentException("The strCountryName must not be empty");
                }

                GlobalWeatherService.GlobalWeather globalWeather = new GlobalWeatherService.GlobalWeather();
                string strWeather = globalWeather.GetWeather(strCityName, strCountryName);
                return strWeather;
            }
            catch (Exception ex)
            {
                mLog.Error(ex.InnerException);
                throw ex;
            }
        }
    }
}
