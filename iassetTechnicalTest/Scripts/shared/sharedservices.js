var app = angular.module('sharedServices', []);

app.config(['$provide', '$httpProvider', function ($provide, $httpProvider) {
    $provide.decorator('$templateRequest', ['$delegate', function ($delegate) {

        var fn = $delegate;
        $delegate = function (tpl) {

            for (var key in fn) {
                $delegate[key] = fn[key];
            }

            return fn.apply(this, [tpl, true]);
        };

        return $delegate;
    }]);
    $httpProvider.interceptors.push('httpInterceptor')
}]);


app.factory('httpInterceptor', function ($q, $window) {
    return {
        'request': function (config) {
            angular.element(document.querySelector('#spinner'))[0].hidden = false;
            return config || $q.when(config);
        },
        'response': function (response) {
            angular.element(document.querySelector('#spinner'))[0].hidden = true;
            return response || $q.when(response);
        }
    }
});