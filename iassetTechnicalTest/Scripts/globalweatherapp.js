'use strict';

// Declare app level module which depends on views, and controllers
var app = angular.module("globalWeatherApp", ['ngRoute', 'sharedServices']).
    config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
        $routeProvider
                    .when('/Weather', {
                        templateUrl: 'Weather/Index'
                    })
                    .otherwise({
                        redirectTo: '/'
                    })
}]);


