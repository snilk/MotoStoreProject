angular.module("motoStoreCategories").controller("categoriesController", [
    "$scope",
    "motoByCategories",
    "$state",
    '$filter',
    function ($scope, motoByCategories, $state, $filter) {
        $scope.motos = motoByCategories;

        $scope.showInfoBtn = function (item) {
            item.isShowInfoBtn = true;
        };

        $scope.hideInfoBtn = function (item) {
            item.isShowInfoBtn = false;
        };

        // $scope.sortType     = 'Price'; // set the default sort Type
        $scope.sortReverse = false;

        $scope.filteredMotos = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 9;
        $scope.maxSize = 5;

        $scope.$watch("currentPage + numPerPage", function () {
            var begin = ($scope.currentPage - 1) * $scope.numPerPage,
                end = begin + $scope.numPerPage;
            $scope.filteredMotos = $scope.motos.slice(begin, end);
        });
    }
]);
