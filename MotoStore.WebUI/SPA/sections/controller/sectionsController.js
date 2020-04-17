angular.module("bookStoreSections").controller("sectionsController", [
    "$scope",
    "bookBySections",
    "$state",
    '$filter',
    function ($scope, bookBySections, $state, $filter) {
        $scope.books = bookBySections;

        $scope.showInfoBtn = function (item) {
            item.isShowInfoBtn = true;
        };

        $scope.hideInfoBtn = function (item) {
            item.isShowInfoBtn = false;
        };

        // $scope.sortType     = 'Price'; // set the default sort Level
        $scope.sortReverse = false;

        $scope.filteredBooks = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 9;
        $scope.maxSize = 5;

        $scope.$watch("currentPage + numPerPage", function () {
            var begin = ($scope.currentPage - 1) * $scope.numPerPage,
                end = begin + $scope.numPerPage;
            $scope.filteredBooks = $scope.books.slice(begin, end);
        });
    }
]);
