angular.module("bookStoreProfile").controller("profileController", [
    "$scope",
    "$state",
    "user",
    function($scope, $state, user) {
        $scope.user = user;

        $scope.goToBook = function (book) {
            var id = book.Id;
            var section = book.Section;

            $state.go('individual', { BookId: id, section: section });
            console.log(book);
        }
    }
  ]);