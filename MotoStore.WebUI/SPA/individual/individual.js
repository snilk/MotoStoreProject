angular.module("bookStoreIndividual", []).config([
    "$stateProvider",
    "$locationProvider",
    function($stateProvider, $locationProvider) {
      $stateProvider.state("individual", {
        url: "/sections/:section/:BookId",
        templateUrl: "/SPA/individual/template/individualTemplate.html",
        controller: "individualController",
        resolve: {
          bookById: function($stateParams, $q, bookService) {
            var deferred = $q.defer();
            var bookId = $stateParams.BookId;
            var section = $stateParams.section;
  
            bookService.get(
                bookId, section,
              function(res) {
                deferred.resolve(res.data);
              },
              function(err) {
                deferred.reject(err);
              }
            );
  
            return deferred.promise;
          }
        }
      });
    }
  ]);
  