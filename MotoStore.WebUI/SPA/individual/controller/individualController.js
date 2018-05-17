angular.module("motoStoreIndividual").controller("individualController", [
  "$scope",
    "motoById",
    'orderService',
    'authService',
  'toastr',
  function ($scope, motoById, orderService, authService, toastr) {
      $scope.moto = motoById[0];
      $scope.isModalOpen = false;
      $scope.orderInfo = undefined;
      $scope.data = {
          singleSelect: null
      };

    $scope._Index = 0;
    $scope.isActive = function (index) {
        return $scope._Index === index;
    };

    authService.isAuthorizated(function (res) {
        if (res.data.isCorrectToken) {
            $scope.isAuthorizated = true;
        }
    }, function (err) {
        $scope.isAuthorizated = false;
    })

    $scope.openModal = function () {
        if ($scope.moto.number_of_models==0) {
            if (toastr.active() == 0) { toastr.warning('This motorcycle is not in stock', 'Sorry'); }
        }
        else {
            if ($scope.isAuthorizated) {
                $scope.isModalOpen = true;
                orderService.getOrderInfo(function (res) { $scope.orderInfo = res.data[0] }, function (err) {
                    console.log(err)
                })
            } else {
                if (toastr.active() == 0) {
                    var toast = toastr.warning('Warning!', 'You have to login or registrated for make order!');
                }


            }

        }
    }

    $scope.makeOrder = function (isPickup, isDelivery, selectModel, homeAdress) {
        var token = authService.getToken();
        var shop = selectModel ? selectModel : $scope.orderInfo.shops[0].Id
        var query = {
            token: token,
            idMoto: $scope.moto.Id,
            adress: isPickup ? homeAdress : null,
            idShop: shop

        };
       
        if ((isPickup === undefined || isPickup === false) && (isDelivery === undefined || isDelivery === false)) {
            $scope.error = 'Fill adress or delivery field';
        }
        else if (isPickup && (query.adress === '' || query.adress === undefined || query.adress === null)) {
            $scope.error = 'Fill  delivery field';
        }
        else if (isDelivery && selectModel === null) {
            $scope.error = 'Fill  shop adress field';
        }
        else {
            $scope.error = '';
            orderService.makeOrder(query, function (res) {
                if (res.data.isCorrectOrder) {
                    toastr.success('Success!', 'You are make order. Order is pending. Manager will contact you in the near future');
                    $scope.isModalOpen = false;
                } else {
                    
                    if (toastr.active() == 0) { toastr.error('Error!', 'Make order failed'); }
                }
            }, function (err) {
        console.log(err)
            })
        }


        //if (isPickup === undefined && (query.adress === '' || query.adress === undefined || query.adress === null)) {
        //    $scope.error = 'Fill adress field';
        //    console.log(1)
        //} else if ((isDelivery === undefined || isDelivery === false) || selectModel === null) {
        //    console.log(isDelivery, selectModel)
        //    $scope.error = 'Fill delivery field';
        //    console.log(2)
        //} else {
        //    console.log(3)
        //    $scope.error = '';
        //    orderService.makeOrder(query, function (res) { console.log(res) }, function (err) {
        //        console.log(err)
        //    })
        //}

        
    }
  
    $scope.showPrev = function (photos) {
        $scope._Index = ($scope._Index > 0) ? --$scope._Index : photos.length - 1;
    };
 
    $scope.showNext = function (photos) {
        $scope._Index = ($scope._Index < photos.length - 1) ? ++$scope._Index : 0;
    };
   
    $scope.showPhoto = function (index) {
        $scope._Index = index;
    };
  }
]);
