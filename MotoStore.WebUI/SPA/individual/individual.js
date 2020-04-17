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
            var BookId = $stateParams.BookId;
            var Section = $stateParams.section;
  
            bookService.get(
                BookId, Section,
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
  