angular.module('search').config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Scripts/app/search/views/search.view.html'
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);