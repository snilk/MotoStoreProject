angular.module('motoStoreCommon').factory('motoCategories', function($http) {
    return {
        get: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqCategories').then(successCb, errorCb);
        }
    };
});