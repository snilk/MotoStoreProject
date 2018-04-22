angular.module("motoStoreCategories", ['ui.bootstrap']).config([
    "$stateProvider",
    "$locationProvider",
    function ($stateProvider, $locationProvider) {
        $stateProvider.state("categories", {
            url: "/categories/:category",
            templateUrl: "/SPA/categories/template/categoriesTemplate.html",
            controller: "categoriesController",
            resolve: {
                motoByCategories: function ($stateParams, $q, motoByCategoriesService) {
                    var deferred = $q.defer();
                    var make = $stateParams.category;

                    motoByCategoriesService.get(
                        make,
                        function (res) {
                            deferred.resolve(res.data);
                        },
                        function (err) {
                            deferred.reject(err);
                        }
                    );

                    return deferred.promise;
                }, 
            }
        });
    }
]);
