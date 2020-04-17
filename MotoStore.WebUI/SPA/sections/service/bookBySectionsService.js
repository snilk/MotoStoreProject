angular.module('bookStoreSections').factory('bookBySectionsService', function($http) {
    return {
        get: function (make, successCb, errorCb) {
            return $http.get('/Products/Books/' + make).then(successCb, errorCb);
        }
    };
});