var idealApp = angular.module('idealApp', ['ngRoute', 'ngCookies']);

idealApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/paymentresult', {
            title: 'U heeft betaald',
            templateUrl: 'Pages/paymentResult.html',
            controller: 'paymentResultController'
        })
        .when('/', {
            title: 'I want to pay',
            templateUrl: 'Pages/payment.html',
            controller: 'paymentController'
        });

    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: false
    //});
});

idealApp.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {

        if (current.hasOwnProperty('$$route')) {
            $rootScope.title = current.$$route.title;
        }
    });
}]);