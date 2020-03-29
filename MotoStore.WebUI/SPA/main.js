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
    .run(function (motoCategories, $rootScope, $state, $stateParams, $location, authService, adressShops) {
        $rootScope.motoCategories = undefined;
        console.log($location.path())
        if ($location.path() === '/') {
            $location.path('/categories/All')
        }

        $rootScope.shopsInfo = undefined;

        adressShops.get(function (res) {
            $rootScope.shopsInfo = res.data;
        }, function (err) {
            console.log(err)
        })

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
        '$mdSidenav',
        function ($scope, $state, authService, toastr, $mdSidenav) {
            $scope.isLogInShow = false;
            $scope.isLogIn = false;
            $scope.isAdmin = false;
            

            authService.isAuthorizated(function (res) {
                if (res.data.isCorrectToken) {
                    $scope.isAuthorizated = true;
                };
                if (res.data.IsAdmin === true) {
                    $scope.isAdmin = true;
                };
            }, function (err) {
                $scope.isAuthorizated = false;
            })

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

            $scope.selectCategory = function (Make) {
                
                $state.go("categories", { category: Make });
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

                            if (toastr.active() == 0) {
                            toastr.error('Error!', 'User already exist');
                             }
                        }
                    })
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

                authService.logIn(user).then(function (res) {
                    if (res.data.correctPassword && res.data.correctUsername) {
                        toastr.success('Success!', 'You are authorizated');
                        authService.storeToken(res.data.token, function () {
                            $scope.isLogInShow = false;
                            $scope.isAuthorizated = true;
                        })
                        localStorage.setItem('IsAdmin', res.data.IsAdmin);
                        if (res.data.IsAdmin === true) {
                            $scope.isAdmin = true;
                        }
                    } else if (!res.data.correctUsername) {
                        
                        if (toastr.active() == 0) {
                            toastr.error('Error!', 'Wrong UserName');
                        }
                        $scope.loginPassword = '';
                        $scope.loginUsername = '';
                    } else if (!res.data.correctPassword) {
                       
                        if (toastr.active() == 0) {
                            toastr.error('Error!', 'Wrong Password');
                        }
                        $scope.loginPassword = '';
                    }
                })
            };

            $scope.logOut = function () {
                authService.logOut(function () {

                    $scope.isAuthorizated = false;
                    $scope.isAdmin = false;
                    $state.go('categories', {
                        category: 'All'
                    });
                    toastr.info('You are left your account');
                    $scope.loginPassword = '';
                    $scope.loginUsername = '';
                })
            }

            $scope.toggleLeft = buildToggler('left');

            $scope.makes = [{ "Make": "BMW" }, { "Make": "Harley-Davidson" }, { "Make": "Izh" }, { "Make": "Jawa" }, { "Make": "Yamaha" }];
            $scope.types = ['Cruiser', 'Sports bike', 'Classic', 'Sport-tourist'];
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
                    $state.go('categories', { category: $scope.Make, filter: query })
                } else {
                    $state.go('categories', { category: 'All', filter: query})
                }
                

            };

            function buildToggler(componentId) {
                return function () {
                    $mdSidenav(componentId).toggle();
                };
            }

        }
    ]);