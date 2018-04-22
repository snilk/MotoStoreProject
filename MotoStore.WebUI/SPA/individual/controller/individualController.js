angular.module("motoStoreIndividual").controller("individualController", [
  "$scope",
  "motoById",
  function($scope, motoById) {
      $scope.moto = motoById[0];
      

    $scope._Index = 0;
    $scope.isActive = function (index) {
        return $scope._Index === index;
    };
  
    $scope.showPrev = function (photos) {
        $scope._Index = ($scope._Index > 0) ? --$scope._Index : photos.length - 1;
    };
 
    $scope.showNext = function (photos) {
        $scope._Index = ($scope._Index < photos.length - 1) ? ++$scope._Index : 0;
    };
   
    $scope.showPhoto = function (index) {
        $scope._Index = index;
    };
  }
]);
