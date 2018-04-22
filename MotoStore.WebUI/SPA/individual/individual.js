angular.module("motoStoreIndividual", []).config([
    "$stateProvider",
    "$locationProvider",
    function($stateProvider, $locationProvider) {
      $stateProvider.state("individual", {
        url: "/categories/:category/:motoId",
        templateUrl: "/SPA/individual/template/individualTemplate.html",
        controller: "individualController",
        resolve: {
          motoById: function($stateParams, $q, motoService) {
            var deferred = $q.defer();
            var motoId = $stateParams.motoId;
            var make = $stateParams.category;
  
            motoService.get(
                motoId, make,
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
  