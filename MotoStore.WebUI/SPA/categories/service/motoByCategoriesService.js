angular.module('motoStoreCategories').factory('motoByCategoriesService', function($http) {
    return {
        get: function (make, successCb, errorCb) {
            console.log(make)
            return $http.get('/Products/Motorcycles/' + make).then(successCb, errorCb);
        }
    };
});