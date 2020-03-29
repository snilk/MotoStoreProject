angular.module('motoStoreIndividual').factory('motoService', function($http) {
    return {
        get: function(motoId, Make, successCb, errorCb) {
            return $http.get('/Products/Motorcycles/' + Make + '/' + motoId).then(successCb, errorCb);
        },

    };
});