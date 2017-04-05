angular
    .module('globalWeatherApp')
    .controller('globalWeatherController', globalWeatherController);

// Dependency injection - injecting globalWeatherFactory to fetch the real time data
globalWeatherController.$inject = ['$scope', 'globalWeatherFactory'];

function globalWeatherController($scope, globalWeatherFactory) {
    $scope.error = "";
    $scope.loading = true;
    
    $scope.cities = null;
    $scope.weather = null;
    weatherdetails = function (response) {
        $scope.weather = response.data
    }
    $scope.getweather = function (cityname, countryName) {
        if (cityname && countryName)
        {
            globalWeatherFactory.weatherdetails(countryName, cityname, weatherdetails, error);
        }
    }
    
    citylist = function (response) {
        $scope.error = "";
        $scope.cities = response.data;
    }

    error = function (data) {
        $scope.error = "An error has occured while loading counties";
        $scope.loading = false;
    }
    $scope.getcities = function (countryname) {
       globalWeatherFactory.cityList(countryname, citylist, error);
    }


}