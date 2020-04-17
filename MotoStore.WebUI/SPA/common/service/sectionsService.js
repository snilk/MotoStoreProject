angular.module('bookStoreCommon').factory('bookSections', function($http) {
    return {
        getUniqSections: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqSections').then(successCb, errorCb);
        },
        getUniqLevels: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqLevels').then(successCb, errorCb);
        },
        getUniqAuthors: function(successCb, errorCb) {
            return $http.get('/Products/GetUniqAuthors').then(successCb, errorCb);
        }
    };
});