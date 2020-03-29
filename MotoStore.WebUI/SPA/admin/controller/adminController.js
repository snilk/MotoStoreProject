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
                $scope.Status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.Status = 'You cancelled the dialog.';
            });
        };

        $scope.editStatus = function (id) {
            adminService.editStatus(id, function (res) {
                if (res.data.Success) {
                    
                    var resO = $.grep($scope.adminData.orders, function (e) {                     
                        return e.orderId == id;
                    });
                    var resM = $.grep($scope.adminData.motos, function (e) { return e.Id == resO[0].motoId });
                   
                  toastr.success('Motorcycle with Id = ' + resM[0].Id + " changed the count of models by 1.Now :  " + resM[0].ModelsCount + " models in stock ");
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

            $scope.makes = [{ "Make": "BMW" }, { "Make": "Harley-Davidson" }, { "Make": "Izh" }, { "Make": "Jawa" }, { "Make": "Yamaha" }];
            $scope.types = ['Cruiser', 'Sports bike', 'Classic', 'Sport-tourist']

            $scope.cancel = function () {
                $mdDialog.cancel();
            };

            $scope.addMoto = function () {
                var query = {
                    Make: $scope.Make ,           
                    Type: $scope.Type  ,    
                    Year: $scope.YearofIssue,
                    EngineCapacity: $scope.EngineCapacity,
                    Cylinders: $scope.NumberofCilindrs,
                    Abs: $scope.Abs  ,
                    ElectricStarter: $scope.ElectricStarter,
                    CruizeControl: $scope.CruizeControl,
                    Description: $scope.Description ,
                    ModelsCount: $scope.NumberofModels ,
                    Price: $scope.Price    ,
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
