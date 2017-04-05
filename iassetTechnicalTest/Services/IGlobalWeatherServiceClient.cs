using iassetTechnicalTest.Models;
using System.Threading.Tasks;

namespace iassetTechnicalTest.Services
{
    /// <summary>
    /// client side service to interact with external and internal webapi and webservice calls
    /// </summary>
    public interface IGlobalWeatherServiceClient
    {
        string GetCitiesByCountry(string strcountryName);

        string GetWeather(string strCityName, string strCountryName);

        Task<Weather> GetWeatherFromExternalAPI(string strCityName, string strcountryName);

        Weather GetWeatherDetails(string cityName, string countryname);
    }
}
