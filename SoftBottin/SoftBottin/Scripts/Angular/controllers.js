
//angular.module('SoftBottin')
//  .controller('ZapatosCtrl', function ($scope) {
//      $scope.message = "Inicio.";
//  });

//angular.module('SoftBottin')
//  .controller('PrincipalCtrl', function ($scope) {
//      $scope.message = "Perfil.";
//  });

//angular.module('SoftBottin')
//       .controller('carritoController', function ($scope) {
//           $scope.message = "Perfil.";
//           //Route1Controller
//       });

//angular.module('SoftBottin')
//       .controller('ShoppingCartCtrl', function ($scope, localStorageService) {
//           if (localStorageService.get("angular-shopping-cart")) {
//               $scope.shoe = localStorageService.get("angular-shopping-cart");
//           } else {
//               $scope.shoe = [];
//           }


           
//           /*
//            {
//                id:'1',
//                name: 'Bota negra',
//                size: '35',
//                quantity: '1',
//                price : $'70.000'
//                date: '17/07/2016 14:46'
//            }
//           */


//           $scope.addActv = function () {
               
//               $scope.shoe.push($scope.newActv);
//               $scope.newActv = {};
//               localStorageService.set($scope.shoe);
//           }
//           $scope.message = "Perfil.";
//       });


//function Route1Controller($scope, $routeParams) {
//    $scope.sProductReference = $routeParams.sProductReference;
//}


//angular.module('SoftBottin')
//  .controller('MensajesCtrl', function ($scope) {
//      $scope.message = "Mensajes.";
//  });