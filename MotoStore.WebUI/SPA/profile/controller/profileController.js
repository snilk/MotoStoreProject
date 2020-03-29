angular.module("motoStoreProfile").controller("profileController", [
    "$scope",
    "$state",
    "user",
    function($scope, $state, user) {
        $scope.user = user;

        $scope.goToMoto = function (order) {
            var id = order.motoId;
            var category = order.Make;

            $state.go('individual', { motoId: id, category: category})
            console.log(order)
        }
    }
  ]);