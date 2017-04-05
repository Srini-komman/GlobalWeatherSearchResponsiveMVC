angular
    .module('globalWeatherApp')
    .directive('globalWeather', globalWeather)


function globalWeather() {
    return {
        restrict: 'E',
        transclude: true,
        scope: true,
        templateUrl: '../Scripts/templates/citylistTemplate.html'
    }
}