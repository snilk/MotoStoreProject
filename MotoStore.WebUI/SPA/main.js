angular
    .module("motoStore", [
        "motoStoreCommon",
        "ui.router",
        "ui.router.state.events",
        "motoStoreCategories",
        "motoStoreIndividual",
        'ngAnimate',
        'toastr'
    ])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state("home", {
                url: "/",
                
            })
            .state("error", {
                url: "/error"
                // data: {
                //     pageTitle: 'Error'
                // },
                // templateUrl: '/scripts/bonusApp/template/errorTemplate.html'
            })
            .state("notFound", {
                url: "/not_found"
                // data: {
                //     pageTitle: 'Error'
                // },
                // templateUrl: '/scripts/bonusApp/template/errorTemplate.html'
            });

        $urlRouterProvider.otherwise("/");

        $locationProvider.html5Mode(true);
    })
    .run(function (motoCategories, $rootScope, $state, $stateParams, $location, authService) {
        $rootScope.motoCategories = undefined;
        $location.path('/categories/All')

        motoCategories.get(
            function (res) {
                $rootScope.motoCategories = res.data;
            },
            function (err) {
                console.log(err);
            }
        );

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    })
    .controller("motoStoreController", [
        "$scope",
        "$state",
        'authService',
        'toastr',
        function ($scope, $state, authService, toastr) {
            $scope.isLogInShow = false;
            $scope.isLogIn = false;
            

            authService.isAuthorizated(function (res) {
                if (res.data.isCorrectToken) {
                    $scope.isAuthorizated = true;
                }
            }, function (err) {
                $scope.isAuthorizated = false;
            })

            $scope.toggleLogIn = function () {
                $scope.isLogInShow = !$scope.isLogInShow;
            };

            $scope.toggleAuthForm = function () {
                $scope.isLogIn = !$scope.isLogIn;
            };

            $scope.selectCategory = function (make) {
                $state.go("categories", { category: make });
            };

            $scope.registerUser = function () {
                var user = {
                    username: $scope.registerUsername,
                    password: $scope.registerPassword,
                    name: $scope.registerName,
                    surname: $scope.registerSurname,
                    email: $scope.registerEmail,
                    phone: $scope.registerPhone
                };

                authService.register(user).then(function (res) {
                    if (res.data.success) {
                        toastr.success('Success!', 'You are registrated');
                        $scope.registerUsername = '';
                        $scope.registerPassword = '';
                        $scope.registerName = '';
                        $scope.registerSurname = '';
                        $scope.registerEmail = '';
                        $scope.registerPhone = '';
                    } else {
                        toastr.error('Error!', 'User already exist');
                    }
                })
            };

            $scope.logInUser = function () {
                var user = {
                    username: $scope.loginUsername,
                    password: $scope.loginPassword,
                };
                console.log(user)
                authService.logIn(user).then(function (res) {
                    if (res.data.correctPassword && res.data.correctUsername) {
                        toastr.success('Success!', 'You are authorizated');
                        authService.storeToken(res.data.token, function () {
                            $scope.isLogInShow = false;
                            $scope.isAuthorizated = true;
                        })
                    } else if (!res.data.correctUsername) {
                        toastr.error('Error!', 'Wrong username');
                        $scope.loginPassword = '';
                        $scope.loginUsername = '';
                    } else if (!res.data.correctPassword) {
                        toastr.error('Error!', 'Wrong password');
                        $scope.loginPassword = '';
                    }
                })
            };

            $scope.logOut = function () {
                authService.logOut(function () {
                    $scope.isAuthorizated = false;
                })
            }

        }
    ]);