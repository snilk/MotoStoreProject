angular.module("bookStoreSections", ['ui.bootstrap']).config([
    "$stateProvider",
    "$locationProvider",
    function ($stateProvider, $locationProvider) {
        $stateProvider.state("sections", {
            url: "/sections/:section",
            params: {
                filter: null
            },
            templateUrl: "/SPA/sections/template/sectionsTemplate.html",
            controller: "sectionsController",
            resolve: {
                bookBySections: function ($stateParams, $q, bookBySectionsService) {
                    var deferred = $q.defer();
                    var section = $stateParams.section;
                    var filter = $stateParams.filter;

                    bookBySectionsService.get(
                        section,
                        function (res) {

                            if (filter) {
                                var newArr = res.data.filter(function (item) {
                                    return (item.Price >= filter.Price.Low && item.Price <= filter.Price.High) &&
                                        (item.Year >= filter.YearofIssue.Low && item.Year <= filter.YearofIssue.High) &&
                                        (filter.Author === undefined || filter.Author === 'None'
                                            ? true
                                            : item.AuthorName === filter.Author) &&
                                        (filter.Level === undefined || filter.Level === 'None'
                                            ? true
                                            : item.Level === filter.Level);
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
