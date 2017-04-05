'use strict';

// Declare app level module which depends on views, and controllers
angular.module('globalWeatherApp', ['ngRoute']).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider
				.when('/Weather', {
				    templateUrl: 'Weather/Index'
				    //controller: 'CountryListCtrl'
				})
				.otherwise({
				    redirectTo: '/'
				})
}]);