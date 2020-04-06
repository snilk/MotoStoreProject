angular
    .module("motoStore", [
        "motoStoreCommon",
        "ui.router",
        "ui.router.state.events",
        "motoStoreCategories",
        'ngMaterial',
        'ngMessages',
        "motoStoreIndividual",
        "motoStoreProfile",
        'motoStoreAdmin',
        'ngAnimate',
        'toastr'
    ])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state("home", {
                url: "/"
            })
            .state("error", {
                url: "/error"
            })
            .state("notFound", {
                url: "/not_found"
            });

        $urlRouterProvider.otherwise("/");

        $locationProvider.html5Mode(true);
    })
    .run(function (motoCategories, $rootScope, $state, $stateParams, $location, authService, adressShops) {
        $rootScope.motoCategories = undefined;
        console.log($location.path());
        if ($location.path() === '/') {
            $location.path('/categories/All');
        }

        $rootScope.shopsInfo = undefined;

        adressShops.get(function(res) {
                $rootScope.shopsInfo = res.data;
            },
            function(err) {
                console.log(err);
            });

        motoCategories.getUniqMakes(
            function (res) {
                $rootScope.motoCategories = res.data;
            },
            function (err) {
                console.log(err);
            }
        );

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('OnMotoAdded',
            function(event, data) {
                if (data) {
                    motoCategories.getUniqMakes(
                        function (res) {
                            $rootScope.motoCategories = res.data;
                        },
                        function (err) {
                            console.log(err);
                        }
                    );
                }
            });
    })
    .controller("motoStoreController", [
        "$scope",
        "$state",
        'authService',
        'toastr',
        '$mdSidenav',
        'motoCategories',
        '$rootScope',
        function ($scope, $state, authService, toastr, $mdSidenav, motoCategories, $rootScope) {
            $scope.isLogInShow = false;
            $scope.isLogIn = false;
            $scope.isAdmin = false;


            authService.isAuthorizated(function(res) {
                    if (res.data.IsCorrectToken) {
                        $scope.isAuthorizated = true;
                    };
                    if (res.data.IsAdmin === true) {
                        $scope.isAdmin = true;
                    };
                },
                function(err) {
                    $scope.isAuthorizated = false;
                });

            $scope.toggleLogIn = function () {
                
                if ($scope.isAuthorizated) {
                    $state.go('profile');
                } else {
                    $scope.isLogInShow = !$scope.isLogInShow;
                }
            };

            $scope.toggleAuthForm = function () {
                toastr.clear();
                $scope.isLogIn = !$scope.isLogIn;
            };

            $scope.selectCategory = function (make) {
                
                $state.go("categories", { category: make });
            };

            $scope.goToAdmin = function () {
                $state.go('admin');
            }

            $scope.registerUser = function () {
                var user = {
                    UserName: $scope.registerUsername,
                    Password: $scope.registerPassword,
                    Name: $scope.registerName,
                    Surname: $scope.registerSurname,
                    Email: $scope.registerEmail,
                    Phone: $scope.registerPhone
                };
               
                if (user.UserName && user.Password && user.Name && user.Surname && user.Email && user.Phone) {
                    authService.register(user).then(function(res) {
                        if (res.data.Success) {
                            toastr.success('Success!', 'You are registrated');
                            $scope.registerUsername = '';
                            $scope.registerPassword = '';
                            $scope.registerName = '';
                            $scope.registerSurname = '';
                            $scope.registerEmail = '';
                            $scope.registerPhone = '';
                        } else {

                            if (toastr.active() == 0) {
                                toastr.error('Error!', 'User already exist');
                            }
                        }
                    });
                }
                else {
                   
                    if (toastr.active() == 0) {
                        toastr.error('Error!', 'Fill all fields');
                       
                    }
                }
            };

            $scope.logInUser = function () {
                var user = {
                    UserName: $scope.loginUsername,
                    Password: $scope.loginPassword,
                };

                authService.logIn(user).then(function(res) {
                    if (res.data.CorrectPassword && res.data.CorrectUsername) {
                        toastr.success('Success!', 'You are authorizated');
                        authService.storeToken(res.data.Token,
                            function() {
                                $scope.isLogInShow = false;
                                $scope.isAuthorizated = true;
                            })
                        localStorage.setItem('IsAdmin', res.data.IsAdmin);
                        if (res.data.IsAdmin === true) {
                            $scope.isAdmin = true;
                        }
                    } else if (!res.data.CorrectUsername) {

                        if (toastr.active() == 0) {
                            toastr.error('Error!', 'Wrong UserName');
                        }
                        $scope.loginPassword = '';
                        $scope.loginUsername = '';
                    } else if (!res.data.CorrectPassword) {

                        if (toastr.active() == 0) {
                            toastr.error('Error!', 'Wrong Password');
                        }
                        $scope.loginPassword = '';
                    }
                });
            };

            $scope.logOut = function () {
                authService.logOut(function() {

                    $scope.isAuthorizated = false;
                    $scope.isAdmin = false;
                    $state.go('categories',
                        {
                            category: 'All'
                        });
                    toastr.info('You are left your account');
                    $scope.loginPassword = '';
                    $scope.loginUsername = '';
                });
            }

            $scope.toggleLeft = buildToggler('left');


            $rootScope.$watch('motoCategories',
                function(newValue) {
                    if (newValue && newValue.length) {
                        $scope.makes = $rootScope.motoCategories;
                        motoCategories.getUniqTypes(
                            function (res) {
                                $scope.types = res.data;
                            },
                            function (err) {
                                console.log(err);
                            }
                        );
                    }
                });

            $scope.YearofIssue = {};
            $scope.EngineCapacity = {};
            $scope.NumberofCilindrs = {};
            $scope.Price = {};

            $scope.filter = function () {
                var query = {
                    Type: $scope.Type,
                    YearofIssue: {
                        Low: +$scope.YearofIssue.Low || 0,
                        High: +$scope.YearofIssue.High || 1000000000000000
                    },
                    EngineCapacity: {
                        Low: +$scope.EngineCapacity.Low || 0,
                        High: +$scope.EngineCapacity.High || 1000000000000000
                    },
                    NumberofCilindrs: {
                        Low: +$scope.NumberofCilindrs.Low || 0,
                        High: +$scope.NumberofCilindrs.High || 1000000000000000
                    },
                    Abs: $scope.Abs,
                    ElectricStarter: $scope.ElectricStarter,
                    CruizeControl: $scope.CruizeControl,
                    Price: {
                        Low: +$scope.Price.Low || 0,
                        High: +$scope.Price.High || 1000000000000000
                    }
                }

                if ($scope.Make && $scope.Make !== 'None') {
                    $state.go('categories', { category: $scope.Make, filter: query });
                } else {
                    $state.go('categories', { category: 'All', filter: query });
                }
            };
            $scope.clearFiler = function() {
                $scope.Type = 'None';
                $scope.Make = 'None';
                $scope.YearofIssue = {};
                $scope.EngineCapacity = {};
                $scope.NumberofCilindrs = {};
                $scope.Price = {};
                $scope.filter();
            };

            function buildToggler(componentId) {
                return function () {
                    $mdSidenav(componentId).toggle();
                };
            }

        }
    ]);