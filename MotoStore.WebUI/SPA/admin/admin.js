angular.module("motoStoreAdmin", []).config([
    "$stateProvider",
    "$locationProvider",
    function ($stateProvider, $locationProvider) {
        $stateProvider.state("admin", {
            url: "/admin",
            templateUrl: "/SPA/admin/template/adminTemplate.html",
            controller: "adminController",
            resolve: {
                isAdmin: function ($q, toastr) {
                    var deferred = $q.defer();

                    var role = localStorage.getItem('role');

                    if (role === "true") {
                        console.log(role)
                        deferred.resolve();
                    } else {
                        deferred.reject();
                        toastr.error('You are not have admin root', 'Error')
                    }

                    return deferred.promise;
                },
                adminData: function (adminService, $q) {
                    var deferred = $q.defer();

                    adminService.get(function (res) {
                        deferred.resolve(res.data);
                    }, function (err) {
                        deferred.reject(err);
                    })

                    return deferred.promise
                }
            }
        });
    }
]);