angular.module('bookStoreIndividual').factory('bookService', function($http) {
    return {
        get: function(bookId, make, successCb, errorCb) {
            return $http.get('/Products/Books/'+ bookId).then(successCb, errorCb);
        },

    };
});