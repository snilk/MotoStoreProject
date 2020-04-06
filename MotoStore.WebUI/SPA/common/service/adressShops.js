angular.module('motoStoreCommon').factory('adressShops', function ($http) {
    return {
        get: function (successCb, errorCb) {
            return $http.get('/Shop/ShopInformation').then(successCb, errorCb);
        }
    };
});