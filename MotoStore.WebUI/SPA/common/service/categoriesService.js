angular.module('motoStoreCommon').factory('motoCategories', function($http) {
    return {
        getUniqMakes: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqCategories').then(successCb, errorCb);
        },
        getUniqTypes: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqTypes').then(successCb, errorCb);
        }
    };
});