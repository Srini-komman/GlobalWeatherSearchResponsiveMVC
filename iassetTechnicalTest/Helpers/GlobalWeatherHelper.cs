using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using iassetTechnicalTest.Models;

namespace iassetTechnicalTest.Helpers
{
    /// <summary>
    /// This class contains helper shared methods that can be used in the application
    /// </summary>
    public class GlobalWeatherHelper
    {
        /// <summary>
        /// This fiction is used to convert xml message string into json string fromat
        /// </summary>
        /// <param name="strXMLDoc"></param>
        /// <returns></returns>
        public static string ConvertXMLToJSON(string strXMLDoc)
        {
            // To convert an XML node contained in string xml into a JSON string   
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXMLDoc);
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("City");

            List<string> cities = new List<string>();
            foreach (XmlNode element in elemList)
            {
                cities.Add(element.InnerText);
            }
            
            string citiesjsonText = JsonConvert.SerializeObject(cities);
            return citiesjsonText;
        }

        /// <summary>
        /// This fiction is used to map json message into model object called Weather
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static Weather PopulateWeatherModelFromJsonText(string strJson)
        {
            try
            {
                Weather weather = new Weather();
                dynamic jsonObject = JsonConvert.DeserializeObject(strJson);
                if (jsonObject != null)
                {
                    weather.Location = jsonObject.name.Value;
                    weather.Temperature = Convert.ToString(jsonObject.main.temp.Value);
                    weather.Pressure = Convert.ToString(jsonObject.main.pressure.Value);
                    weather.Wind = Convert.ToString(jsonObject.wind.speed.Value);
                    weather.Visibility = Convert.ToString(jsonObject.visibility.Value);
                }
                return weather;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}