angular.module("motoStoreCategories", ['ui.bootstrap']).config([
    "$stateProvider",
    "$locationProvider",
    function ($stateProvider, $locationProvider) {
        $stateProvider.state("categories", {
            url: "/categories/:category",
            params: {
                filter: null
            },
            templateUrl: "/SPA/categories/template/categoriesTemplate.html",
            controller: "categoriesController",
            resolve: {
                motoByCategories: function ($stateParams, $q, motoByCategoriesService) {
                    var deferred = $q.defer();
                    var make = $stateParams.category;
                    var filter = $stateParams.filter;

                    motoByCategoriesService.get(
                        make,
                        function (res) {

                            if (filter) {
                                var newArr = res.data.filter(function (item) {
                                    return (item.price >= filter.Price.Low && item.price <= filter.Price.High)
                                        && (item.year_of_issue >= filter.YearofIssue.Low && item.year_of_issue <= filter.YearofIssue.High)
                                        && (item.engine_capacity >= filter.EngineCapacity.Low && item.engine_capacity <= filter.EngineCapacity.High)
                                        && (item.number_of_cilindrs >= filter.NumberofCilindrs.Low && item.number_of_cilindrs <= filter.NumberofCilindrs.High)
                                        && (filter.isABS === undefined || filter.isABS === false ? true : item.isABS === filter.isABS)
                                        && (filter.isElectrostarter === undefined || filter.isElectrostarter === false ? true : item.isElectrostarter === filter.isElectrostarter)
                                        && (filter.isCruizeControl === undefined || filter.isCruizeControl === false ? true : item.isCruizeControl === filter.isCruizeControl)
                                        && (filter.type === undefined || filter.type === 'None' ? true : item.type === filter.type)
                                        
                                });

                                deferred.resolve(newArr);
                            } else {
                                deferred.resolve(res.data);
                            }
                            
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
