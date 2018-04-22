angular.module('motoStoreIndividual').factory('motoService', function($http) {
    return {
        get: function(motoId, make, successCb, errorCb) {
            return $http.get('/Products/Motorcycles/' + make + '/' + motoId).then(successCb, errorCb);
        }
    };
});