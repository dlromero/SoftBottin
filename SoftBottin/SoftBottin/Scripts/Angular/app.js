var app = angular.module('SoftBottin', ['ngRoute', 'LocalStorageModule'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider //add resolve function is pending.
        .when('/', {
            templateUrl: rootURL + 'Security/SubPrincipal',
            controller: 'PrincipalCtrl',
        })
        .when('/ViewShoppingCart', {
            templateUrl: rootURL + 'Security/ViewShoppingCart',
            controller: 'ShoppingCartCtrl',
        })
        .when('/ProductDetail/:sProductReference', {
            templateUrl: function (stateParams) {
                return rootURL + 'Security/ProductDetail?sProductReference=' + stateParams.sProductReference;
            },
            controller: 'carritoController',
        })
    ;
    //.otherwise({
    //    redirectTo: '/'
    //});
}]);

/*CONFIG*/
app.run(function ($rootScope, $location, $route, $timeout) {

    $rootScope.config = {};
    $rootScope.config.app_url = $location.url();
    $rootScope.config.app_path = $location.path();
    $rootScope.layout = {};
    $rootScope.layout.loading = false;

    $rootScope.$on('$routeChangeStart', function () {
        console.log('$routeChangeStart');
        //show loading gif
        $timeout(function () {
            $rootScope.layout.loading = true;
        });
    });
    $rootScope.$on('$routeChangeSuccess', function () {
        console.log('$routeChangeSuccess');
        //hide loading gif
        $timeout(function () {
            $rootScope.layout.loading = false;
        }, 200);
    });
    $rootScope.$on('$routeChangeError', function () {

        //hide loading gif
        alert('wtff');
        $rootScope.layout.loading = false;

    });
});


app.controller('ZapatosCtrl', function ($scope) {
    $scope.message = "Inicio.";
});

app.controller('PrincipalCtrl', function ($scope) {
    $scope.message = "Perfil.";
});

app.controller('carritoController', function ($scope) {
    $scope.message = "Perfil.";
    //Route1Controller
});


app.controller('ShoppingCartCtrl', function ($scope, localStorageService) {
    if (localStorageService.get("angular-shopping-cart")) {
        $scope.shoe = localStorageService.get("angular-shopping-cart");
        if (localStorageService.cookie.isSupported) {
            localStorageService.cookie.set('angular-shopping-cart-cookie', $scope.shoe);
        }
    } else {
        $scope.shoe = [];
    }

    $scope.numberShoes = $scope.shoe.length;

    /*
     {
         id:'1',
         name: 'Bota negra',
         size: '35',
         quantity: '1',
         price : $'70.000'
         date: '17/07/2016 14:46'
     }
    */


    $scope.addActv = function () {
        $scope.shoe.push($scope.newActv);
        $scope.newActv = {};
        localStorageService.set("angular-shopping-cart", $scope.shoe);
    }
    $scope.message = "Perfil.";
});
