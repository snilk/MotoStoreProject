angular.module("bookStoreIndividual").controller("individualController", [
  "$scope",
    "bookById",
    'orderService',
    'authService',
  'toastr',
  function ($scope, bookById, orderService, authService, toastr) {
      $scope.book = bookById;
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
      
       
        if ($scope.isAuthorizated) {
            if ($scope.book.ModelsCount == 0) {
                if (toastr.active() == 0) { toastr.warning('This book is not in stock', 'Sorry'); }
            } else {
                $scope.isModalOpen = true;
                orderService.getOrderInfo(function(res) { $scope.orderInfo = res.data },
                    function(err) {
                        console.log(err);
                    });
            }
            } else {
                if (toastr.active() == 0) {
                    toastr.warning('Warning!', 'You have to login or registered to make order!');
                }
        }

        
    }

    $scope.makeOrder = function (isPickup, isDelivery, selectModel, homeAdress) {
        var token = authService.getToken();
        var shop = selectModel ? selectModel : $scope.orderInfo.ShopsInformation[0].Id
        var query = {
            Token: token,
            BookId: $scope.book.Id,
            Address: isPickup ? homeAdress : null,
            ShopId: shop
        };
       
        if ((isPickup === undefined || isPickup === false) && (isDelivery === undefined || isDelivery === false)) {
            $scope.error = 'Fill Address or delivery field';
        }
        else if (isPickup && (query.Address === '' || query.Address === undefined || query.Address === null)) {
            $scope.error = 'Fill  delivery field';
        }
        else if (isDelivery && selectModel === null) {
            $scope.error = 'Fill  shop Address field';
        }
        else {
            $scope.error = '';
            orderService.makeOrder(query,
                function(res) {
                    if (res.data.Success) {
                        toastr.success('Success!',
                            'You have made order. Order is pending. Manager will contact you in the near future');
                        $scope.isModalOpen = false;
                    } else {

                        if (toastr.active() == 0) {
                            toastr.error('Error!', 'Make order failed');
                        }
                    }
                },
                function(err) {
                    console.log(err);
                });
        }


        //if (isPickup === undefined && (query.Address === '' || query.Address === undefined || query.Address === null)) {
        //    $scope.error = 'Fill Address field';
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
