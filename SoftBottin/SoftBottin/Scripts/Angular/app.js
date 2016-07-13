angular.module('SoftBottin', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider //add resolve function is pending.
        .when('/', {
            templateUrl: '../Security/SubPrincipal',
            controller: 'PrincipalCtrl',
        })
        .when('/ProductDetail/:sProductReference', {
            templateUrl: function (stateParams){
                return '../Security/ProductDetail?sProductReference=' + stateParams.sProductReference;
            },
                //'../Security/ProductDetail?sProductReference=01',
            controller: 'carritoController',
        })
        .otherwise({
            redirectTo: '/'
        });
}])
