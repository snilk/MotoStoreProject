angular
    .module("bookStore", [
        "bookStoreCommon",
        "ui.router",
        "ui.router.state.events",
        "bookStoreSections",
        'ngMaterial',
        'ngMessages',
        "bookStoreIndividual",
        "bookStoreProfile",
        'bookStoreAdmin',
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
    .run(function (bookSections, $rootScope, $state, $stateParams, $location, authService, adressShops) {
        $rootScope.bookSections = undefined;
        console.log($location.path());
        if ($location.path() === '/') {
            $location.path('/sections/All');
        }

        $rootScope.shopsInfo = undefined;

        adressShops.get(function(res) {
                $rootScope.shopsInfo = res.data;
            },
            function(err) {
                console.log(err);
            });

        bookSections.getUniqSections(
            function (res) {
                $rootScope.bookSections = res.data;
            },
            function (err) {
                console.log(err);
            }
        );

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('OnBookAdded',
            function(event, data) {
                if (data) {
                    bookSections.getUniqSections(
                        function (res) {
                            $rootScope.bookSections = res.data;
                        },
                        function (err) {
                            console.log(err);
                        }
                    );
                }
            });
    })
    .controller("bookStoreController", [
        "$scope",
        "$state",
        'authService',
        'toastr',
        '$mdSidenav',
        'bookSections',
        '$rootScope',
        function ($scope, $state, authService, toastr, $mdSidenav, bookSections, $rootScope) {
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

            $scope.selectSection = function (make) {
                
                $state.go("sections", { section: make });
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
                    $state.go('sections',
                        {
                            section: 'All'
                        });
                    toastr.info('You are left your account');
                    $scope.loginPassword = '';
                    $scope.loginUsername = '';
                });
            }

            $scope.toggleLeft = buildToggler('left');

            var uniqLevels = function() {
                bookSections.getUniqLevels(
                    function (res) {
                        $scope.levels = res.data;
                    },
                    function (err) {
                        console.log(err);
                    }
                );
            }

            var uniqSections = function() {
                bookSections.getUniqSections(
                    function (res) {
                        $scope.sections = res.data;
                    },
                    function (err) {
                        console.log(err);
                    }
                );
            }

            var uniqAuthors = function() {
                bookSections.getUniqAuthors(
                    function(res) {
                        $scope.authors = res.data;
                    },
                    function(err) {
                        console.log(err);
                    }
                );
            }

            $rootScope.$watch('bookSections',
                function(newValue) {
                    if (newValue && newValue.length) {
                        $scope.sections = $rootScope.bookSections;
                        uniqLevels();
                        uniqAuthors();
                    }
                });

            uniqSections();
            uniqLevels();
            uniqAuthors();
            $scope.YearofIssue = {};
            $scope.AuthorName = {};
            $scope.Title = {};
            $scope.Price = {};

            $scope.filter = function () {
                var query = {
                    Level: $scope.Level,
                    YearofIssue: {
                        Low: +$scope.YearofIssue.Low || 0,
                        High: +$scope.YearofIssue.High || 1000000000000000
                    },
                    AuthorName: {
                        Low: +$scope.AuthorName.Low || 0,
                        High: +$scope.AuthorName.High || 1000000000000000
                    },
                    Title: {
                        Low: +$scope.Title.Low || 0,
                        High: +$scope.Title.High || 1000000000000000
                    },
                    Price: {
                        Low: +$scope.Price.Low || 0,
                        High: +$scope.Price.High || 1000000000000000
                    }
                }

                if ($scope.Section && $scope.Section !== 'None') {
                    $state.go('sections', { section: $scope.Section, filter: query });
                } else {
                    $state.go('sections', { section: 'All', filter: query });
                }
            };
            $scope.clearFiler = function() {
                $scope.Level = 'None';
                $scope.Section = 'None';
                $scope.YearofIssue = {};
                $scope.AuthorName = {};
                $scope.Title = {};
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