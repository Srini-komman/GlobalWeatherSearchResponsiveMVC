
namespace iassetTechnicalTest.ServiceAgent.Interfaces
{
    /// <summary>
    /// Service to interact with external webservice called globalweather
    /// </summary>
    /// </summary>
    public interface IGlobalWeatherServiceAgent
    {
        string GetCitiesByCountry(string strcountryName);
        string GetWeather(string strCityName, string strCountryName);
    }
}
