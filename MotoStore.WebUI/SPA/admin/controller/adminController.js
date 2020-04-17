angular.module("bookStoreAdmin").controller("adminController", [
    "$scope",
    '$state',
    'adminData',
    'adminService',
    '$mdDialog',
    'toastr',
    '$rootScope',
function ($scope, $state, adminData, adminService, $mdDialog, toastr, $rootScope) {
        $scope.adminData = adminData;

        $scope.showAddShopDialog = function (ev, book) {
            $mdDialog.show({
                    controller: addShopDialogController,
                    templateUrl: '/SPA/admin/template/addShopDialogTemplate.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
                })
                .then(function (answer) {
                    $scope.Status = 'You said the information was "' + answer + '".';
                }, function () {
                    $scope.Status = 'You cancelled the dialog.';
                });
        };
        
        $scope.addBook = function (ev, book) {
            $mdDialog.show({
                controller: addBook,
                templateUrl: '/SPA/admin/template/editTemplate.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
            })
            .then(function (answer) {
                $scope.Status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.Status = 'You cancelled the dialog.';
            });
        };

        $scope.editStatus = function (id) {
            adminService.editStatus(id,
                function(res) {
                    if (res.data.Success) {

                        var resO = $.grep($scope.adminData.orders,
                            function(e) {
                                return e.orderId == id;
                            });
                        var resM = $.grep($scope.adminData.books, function(e) { return e.Id == resO[0].BookId });

                        toastr.success('Book with Id = ' +
                            resM[0].Id +
                            " changed the count of models by 1.Now :  " +
                            resM[0].ModelsCount +
                            " models in stock ");
                        $state.reload();

                        // $state.go('admin');
                    }
                },
                function(err) {
                    console.log(err)
                });
        }

        $scope.removeBook = function (id) {
            adminService.removeBook(id,
                function(res) {
                    if (res.data.Success) {

                        toastr.error('Book with Id = ' + id + ' was deleted');
                        $state.reload();
                    }
                },
                function(err) {

                    console.log(err)
                });
        }

        $scope.removeShopClick = function (id) {
            adminService.removeShop(id,
                function(res) {
                    if (res.data.Success) {
                        toastr.error('Shop was deleted');
                        $state.reload();
                    }
                },
                function(err) {
                    console.log(err)
                });
        }

        function addShopDialogController($scope, $mdDialog) {
            $scope.cancel = function() {
                $mdDialog.cancel();
            };

            $scope.addShopClick = function() {
                var query = {
                    Address: $scope.Address,
                    Phone1: $scope.Phone1,
                    Phone2: $scope.Phone2
                }

                adminService.addShop(query,
                    function(res) {
                        if (res.data.Success) {
                            $state.reload();
                            toastr.success('New shop added!', 'Success');
                            $mdDialog.cancel();
                        }
                    },
                    function(err) {
                        console.log(err);
                    });
            }
        }

        function addBook($scope, $mdDialog, $rootScope) {

            $scope.sections = [
                { "Section": "DataBase" }, { "Section": "Programming Languages" }, { "Section": "Operating Systems" },
                { "Section": "Multimedia & Design" }, { "Section": "Computer Security" }
            ];
            $scope.levels = ["Beginning", "Junior", "Middle", "Senior"];

            $scope.mainImage = {};
            $scope.additionalImages = {};

            $scope.cancel = function () {
                $mdDialog.cancel();
            };

            $scope.addBook = function () {
                var query = {
                    Section: $scope.Section ,           
                    Level: $scope.Level  ,    
                    Year: $scope.YearofIssue,
                    AuthorName: $scope.AuthorName,
                    Title: $scope.Title,
                    Description: $scope.Description ,
                    ModelsCount: $scope.NumberofModels ,
                    Price: $scope.Price,
                    MainImageFile: $scope.mainImage.file
                }

                query.AdditionalImagesFiles = new Array();
                angular.forEach($scope.additionalImages.files,
                    function(item) {
                        query.AdditionalImagesFiles.push(item);
                    });

                adminService.addBook(query,
                    function(res) {
                        if (res.data.Success) {
                            $rootScope.$emit('OnBookAdded', true);
                            $state.reload();
                            toastr.success('New book added!', 'Success');
                            $mdDialog.cancel();
                        }
                    },
                    function(err) {
                        console.log(err);
                    });
            }

        }
    }
]);
