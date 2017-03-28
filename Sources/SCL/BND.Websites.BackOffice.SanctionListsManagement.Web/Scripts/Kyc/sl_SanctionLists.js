var app = angular.module('sanctionListsApp', ['ngAnimate', 'ui.bootstrap']);

app.controller('sanctionListsController', ['$scope', '$http', '$modal', function ($scope, $http, $modal) {
    $scope.sanctionLists = sanctionLists;
    
    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;
}]);