angular.module('motoStoreCommon').factory('authService', function($http) {
    return {
        register: function(user, cb) {
            var req = {
                method: "POST",
                url: "Account/Registration",
                headers: {
                  "Content-Type": "application/json"
                },
                data: user
              };
        
              return $http(req).then(cb);
        },
        isAuthorizated: function (cb, errorcb) {
            var token = this.getToken();
            var req = {
                method: "POST",
                url: "Account/IsCorrectTokenUser",
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    token: token
                }
            };

            return $http(req).then(cb, errorcb);

        },
        logIn: function(user, cb) {
            var req = {
                method: "POST",
                url: "Account/Authorization",
                headers: {
                  "Content-Type": "application/json"
                },
                data: user
              };
        
              return $http(req).then(cb);;
        },
        storeToken: function (token, cb) {
            localStorage.setItem('user', token);
            cb();
        },
        getToken: function (cb) {
            var token = localStorage.getItem('user');
            return token;
        },
        logOut: function (cb) {
            localStorage.clear();
            cb();
        }
    };
});