angular.module('bookStoreIndividual').factory('bookService', function($http) {
    return {
        get: function(BookId, make, successCb, errorCb) {
            return $http.get('/Products/Books/' + make + '/' + BookId).then(successCb, errorCb);
        },

    };
});