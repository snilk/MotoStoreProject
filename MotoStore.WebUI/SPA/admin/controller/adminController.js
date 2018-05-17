angular.module("motoStoreAdmin").controller("adminController", [
    "$scope",
    '$state',
    'adminData',
    'adminService',
    '$mdDialog',
    'toastr',
    function ($scope, $state, adminData, adminService, $mdDialog, toastr) {
        $scope.adminData = adminData;
        
        $scope.addMoto = function (ev, moto) {
            $mdDialog.show({
                controller: addMoto,
                templateUrl: '/SPA/admin/template/editTemplate.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
            })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        };

        $scope.editStatus = function (id) {
            adminService.editStatus(id, function (res) {
                if (res.data.Success) {
                    
                    var resO = $.grep($scope.adminData.orders, function (e) {                     
                        return e.orderId == id;
                    });
                    var resM = $.grep($scope.adminData.motos, function (e) { return e.Id == resO[0].motoId });
                   
                  toastr.success('Motorcycle with Id = ' + resM[0].Id + " changed the count of models by 1.Now :  " + resM[0].number_of_models + " models in stock ");
                    $state.reload();
                    
                   // $state.go('admin');
                }
            }, function (err) {
                console.log(err)
            })
        }

        $scope.removeMoto = function (id) {
            adminService.removeMoto(id, function (res) {
                if (res.data.Success) {
                    
                    toastr.error('Motorcycle with Id = ' + id + ' was deleted');
                    $state.reload();
                }
            }, function (err) {
               
                console.log(err)
            })
        }

        function addMoto($scope, $mdDialog) {

            $scope.makes = [{ "make": "BMW" }, { "make": "Harley-Davidson" }, { "make": "Izh" }, { "make": "Jawa" }, { "make": "Yamaha" }];
            $scope.types = ['Cruiser', 'Sports bike', 'Classic', 'Sport-tourist']

            $scope.cancel = function () {
                $mdDialog.cancel();
            };

            $scope.addMoto = function () {
                var query = {
                    make: $scope.make ,           
                    type: $scope.type  ,    
                    year_of_issue: $scope.YearofIssue,
                    engine_capacity: $scope.EngineCapacity,
                    number_of_cilindrs: $scope.NumberofCilindrs,
                    isABS: $scope.isABS  ,
                    isElectrostarter: $scope.isElectrostarter,
                    isCruizeControl: $scope.isCruizeControl,
                    description: $scope.Description ,
                    number_of_models: $scope.NumberofModels ,
                    price: $scope.Price    ,
                }

                adminService.addMoto(query, function (res) {
                    if (res.data.Success) {
                        $state.reload();
                        toastr.success('New moto added!', 'Success');
                        $mdDialog.cancel();
                    }
                }, function (err) {
                    console.log(err)
                })
            }

        }
    }
]);
