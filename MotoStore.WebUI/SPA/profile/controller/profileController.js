angular.module("motoStoreProfile").controller("profileController", [
    "$scope",
    "$state",
    "user",
    function($scope, $state, user) {
        $scope.user = user;

        $scope.goToMoto = function (motorcycle) {
            var id = motorcycle.Id;
            var category = motorcycle.Make;

            $state.go('individual', { motoId: id, category: category });
            console.log(motorcycle);
        }
    }
  ]);