angular.module("bookStoreProfile", ['ui.bootstrap']).config([
    "$stateProvider",
    "$locationProvider",
    function($stateProvider, $locationProvider) {
      $stateProvider.state("profile", {
        url: "/profile",
        templateUrl: "/SPA/profile/template/profileTemplate.html",
        controller: "profileController",
        resolve: {
            user: function (authService, $q, userService, toastr) {
                var token = authService.getToken();
                var deferred = $q.defer();
                var query = {
                    token: token
                };

                userService.get(query, function (res) {
                    var user = res.data;
                    if (user) {
                        deferred.resolve(user);
                    } else {
                        deferred.reject();
                        toastr.error('Please sign in or sign up', 'Error')
                    }
                }, function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }
        }
      });
    }
  ]);