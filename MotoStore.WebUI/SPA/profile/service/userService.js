angular.module('motoStoreProfile').factory('userService', function ($http) {
    return {
        get: function (data, successCb, errorCb) {
            return $http.post('/Account/EnterAccount', data).then(successCb, errorCb);
        }
    };
});