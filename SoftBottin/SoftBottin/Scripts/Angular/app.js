angular.module('SoftBottin', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider //add resolve function is pending.
         .when('/TiposZapatos', {
             templateUrl: '../Shoes/TiposZapatos',
             controller: 'TiposZapatosCtrl',

         })
        .when('/Zapatos', {
            templateUrl: '../Shoes/Zapatos',
            controller: 'ZapatosCtrl',

        })
        //.when('/perfil', {
        //    templateUrl: 'perfil.html',
        //    controller: 'PerfilCtrl',
        //})
        //.when('/mensajes', {
        //    templateUrl: 'mensajes.html',
        //    controller: 'MensajesCtrl',
        //})
        .otherwise({
            redirectTo: '/'
        });
}])
