
angular.module('SoftBottin')
  .controller('ZapatosCtrl', function ($scope) {
      $scope.message = "Inicio.";
  });

angular.module('SoftBottin')
  .controller('PrincipalCtrl', function ($scope) {
      $scope.message = "Perfil.";
  });

angular.module('SoftBottin')
       .controller('carritoController', function ($scope) {
           Route1Controller
       });



function Route1Controller($scope, $routeParams) {
    $scope.sProductReference = $routeParams.ID;
}


//angular.module('SoftBottin')
//  .controller('MensajesCtrl', function ($scope) {
//      $scope.message = "Mensajes.";
//  });