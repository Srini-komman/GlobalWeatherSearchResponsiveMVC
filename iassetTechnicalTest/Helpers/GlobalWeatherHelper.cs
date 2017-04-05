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
    public class GlobalWeatherHelper
    {
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