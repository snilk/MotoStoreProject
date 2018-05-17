angular.module('motoStoreCommon').factory('adressShops', function ($http) {
    return {
        get: function (successCb, errorCb) {
            return $http.get('/Shop/shopInformaiton').then(successCb, errorCb);
        }
    };
});