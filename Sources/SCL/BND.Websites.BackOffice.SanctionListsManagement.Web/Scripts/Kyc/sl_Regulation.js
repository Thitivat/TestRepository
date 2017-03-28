var app = angular.module('regulationsApp', ['ngAnimate', 'ui.bootstrap', 'ngMessages']);

app.service('alertManager', $alertManager);
app.controller('AlertsController', $alertController);

app.controller('regulationsController', ['$scope', '$http', '$modal', 'alertManager', function ($scope, $http, $modal, alertManager) {
    $scope.maxSize = 5;
    $scope.totalItems = 0;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 25;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
        alert("change to " + pageNo);
       
    };

    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;
    $scope.DateDisplay = DateDisplay;

    $scope.getRegulations = function () {
        $http({
            method: 'GET',
            url: '/api/SanctionLists/' + currentList + '/Regulations?limit=' + $scope.itemsPerPage + '&offset=' + (($scope.currentPage - 1) * $scope.itemsPerPage),
            data: { "limit": $scope.itemsPerPage, "offset": ($scope.currentPage - 1) * $scope.itemsPerPage },
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.regulations = response.data.Regulations;
            $scope.totalItems = response.data.RegulationsCount;
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error while changing filter.");
        })
        .finally(function () {
        });
    };

    $scope.pageChanged = function () {
        $scope.getRegulations();
    };

    //$scope.regulations = regulations;
    $scope.selectRegulationsOptions = {};
    $scope.selectRegulationsOptions.availableOptions = selectRegulationsOptions;
    $scope.selectRegulationsOptions.selected = currentList;

    $scope.getRegulations();

    $scope.listFilterChanged = function () {
        window.location = "/SanctionLists/" + $scope.selectRegulationsOptions.selected+ "/Regulations";
    }

}]);