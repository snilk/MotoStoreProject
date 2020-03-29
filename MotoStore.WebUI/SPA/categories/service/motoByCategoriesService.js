angular.module('motoStoreCategories').factory('motoByCategoriesService', function($http) {
    return {
        get: function (Make, successCb, errorCb) {
            return $http.get('/Products/Motorcycles/' + Make).then(successCb, errorCb);
        }
    };
});