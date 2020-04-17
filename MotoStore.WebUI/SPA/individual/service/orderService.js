angular.module('bookStoreIndividual').factory('orderService', function ($http, authService) {
    return {
        getOrderInfo: function (successCb, errorCb) {
            var token = authService.getToken();
            return $http.post('/Order/OrderInterCompose', {token: token}).then(successCb, errorCb);
        },
        makeOrder: function (query, successCb, errorCb) {
            return $http.post('/Order/OrderCompose', query).then(successCb, errorCb);
        }
    };
});