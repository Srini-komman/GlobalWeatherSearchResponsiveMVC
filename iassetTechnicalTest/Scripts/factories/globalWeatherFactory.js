angular
    .module('globalWeatherApp')
    .factory('globalWeatherFactory', globalWeatherFactory)

globalWeatherFactory.$inject = ['$http'];

function globalWeatherFactory($http) {
    function getcities(country, successcallback, errorcallback) {
        $http({
            method: 'GET',
            url: '/Weather/GetCities',
            params: {
                countryname: country
            },
            cache: false
        }).then(successcallback, errorcallback);
    };
    function getweather(country, city, successcallback, errorcallback) {
        $http({
            method: 'GET',
            url: '/Weather/GetWeather',
            params: {
                countryname: country,
                cityname: city
            },
            cache: false
        }).then(successcallback, errorcallback);
    };
    return {
        cityList: getcities,
        weatherdetails: getweather
        };

}