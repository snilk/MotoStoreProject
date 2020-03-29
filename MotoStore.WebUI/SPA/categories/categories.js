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
                                    return (item.Price >= filter.Price.Low && item.Price <= filter.Price.High)
                                        && (item.Year >= filter.YearofIssue.Low && item.Year <= filter.YearofIssue.High)
                                        && (item.EngineCapacity >= filter.EngineCapacity.Low && item.EngineCapacity <= filter.EngineCapacity.High)
                                        && (item.Cylinders >= filter.NumberofCilindrs.Low && item.Cylinders <= filter.NumberofCilindrs.High)
                                        && (filter.Abs === undefined || filter.Abs === false ? true : item.Abs === filter.Abs)
                                        && (filter.ElectricStarter === undefined || filter.ElectricStarter === false ? true : item.ElectricStarter === filter.ElectricStarter)
                                        && (filter.CruizeControl === undefined || filter.CruizeControl === false ? true : item.CruizeControl === filter.CruizeControl)
                                        && (filter.Type === undefined || filter.Type === 'None' ? true : item.Type === filter.Type)
                                        
                                });

                                deferred.resolve(newArr);
                            } else {
                                deferred.resolve(res.data);
                            }
                            
                        },
                        function (err) {
                            deferred.resolve([]);
                        }
                    );

                    return deferred.promise;
                }, 
            }
        });
    }
]);
