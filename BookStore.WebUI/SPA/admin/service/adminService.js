angular.module('bookStoreAdmin').factory('adminService', function ($http) {
    return {
        getAvailableSections: function (successCb, errorCb) {
            return $http.get('Admin/GetAvailableSections').then(successCb, errorCb);
        },
        getAvailableLevels: function (successCb, errorCb) {
            return $http.get('Admin/GetAvailableLevels').then(successCb, errorCb);
        },
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
        removeBook: function (id, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/RemoveBook",
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    id: id
                }
            };

            return $http(req).then(successCb, errorCb);
        },
        addBook: function (query, successCb, errorCb) {
            var getModelAsFormData = function (data) {
                var dataAsFormData = new FormData();
                angular.forEach(data, function (value, key) {
                    if (Array.isArray(value)) {
                        for (var i = 0; i < value.length; i++) {
                            dataAsFormData.append(key + "["+ i + "]", value[i]);
                        }
                    } else {
                        dataAsFormData.append(key, value);
                    }
                    
                });
                return dataAsFormData;
            };


            var req = {
                method: "POST",
                url: "Admin/AddNewBook",
                headers: {
                    "Content-Type": undefined
                },
                data: getModelAsFormData(query)
            };

            return $http(req).then(successCb, errorCb);
        },
        //addBook: function (query, successCb, errorCb) {
        //    var formData = new FormData();

        //    var req = {
        //        method: "POST",
        //        url: "Admin/AddNewBook",
        //        headers: {
        //            "Content-Type": "application/json"
        //        },
        //        data: query
        //    };

        //    return $http(req).then(successCb, errorCb);
        //},
        addShop: function (query, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/AddNewShop",
                headers: {
                    "Content-Type": "application/json"
                },
                data: query
            };

            return $http(req).then(successCb, errorCb);
        },
        removeShop: function (id, successCb, errorCb) {
            var req = {
                method: "POST",
                url: "Admin/RemoveShop",
                headers: {
                    "Content-Type": "application/json"
                },
                data: {
                    id: id
                }
            };

            return $http(req).then(successCb, errorCb);
        },
    };
});