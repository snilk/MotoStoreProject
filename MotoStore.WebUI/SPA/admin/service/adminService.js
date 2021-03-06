﻿angular.module('motoStoreAdmin').factory('adminService', function ($http) {
    return {
        get: function (successCb, errorCb) {
            return $http.get('Admin/EnterAdmin').then(successCb, errorCb);
        },
        editStatus: function (id, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/ChangeOrderStatus",
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    id: id
                }
            };

            return $http(req).then(successCb, errorCb);
        },
        removeMoto: function (id, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/RemoveMoto",
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    id: id
                }
            };

            return $http(req).then(successCb, errorCb);
        },
        addMoto: function (query, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/AddNewMoto",
                headers: {
                    "Content-Type": "application/json"
                },
                data: query
            };

            return $http(req).then(successCb, errorCb);
        }
    };
});