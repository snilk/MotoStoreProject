angular.module('bookStoreSections').factory('bookBySectionsService', function($http) {
    return {
        get: function (section, successCb, errorCb) {
            return $http.get('/Products/Books/' + section).then(successCb, errorCb);
        }
    };
});