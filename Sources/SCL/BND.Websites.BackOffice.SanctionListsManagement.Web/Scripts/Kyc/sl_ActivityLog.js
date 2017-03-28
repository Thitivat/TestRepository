var app = angular.module('activityLogsApp', ['ngAnimate', 'ui.bootstrap', 'ngMessages']);

app.service('alertManager', $alertManager);
app.controller('AlertsController', $alertController);

app.controller('activityLogsController', ['$scope', '$http', '$modal', 'alertManager', function ($scope, $http, $modal, alertManager) {
    $scope.maxSize = 5;
    $scope.totalItems = logsCount;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 25;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
        alert("change to " + pageNo);
       
    };

    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;

    $scope.pageChanged = function () {

        $http({
            method: 'GET',
            url: '/api/Logs/' + currentList + '?limit=' + $scope.itemsPerPage + '&offset='+(($scope.currentPage - 1) * $scope.itemsPerPage),
            data: { "limit": $scope.itemsPerPage, "offset": ($scope.currentPage - 1) * $scope.itemsPerPage },
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.logs = response.data.logs;
            $scope.totalItems = response.data.LogsCount;
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error while changing filter.");
        })
        .finally(function () {
        });
    };

    $scope.logs = logs;
    $scope.selectLogsOptions = {};
    $scope.selectLogsOptions.availableOptions = selectLogsOptions;
    $scope.selectLogsOptions.selected = currentList;

    for (var i = 0; i < $scope.logs.length; i++) {
        try {
            $scope.logs[i].LogDateParsed = new Date(Date.parse($scope.logs[i].LogDate));
        }
        catch (err) {
            $scope.logs[i].LogDateParsed = "-";
        }
    }

    $scope.listFilterChanged = function () {
        window.location = "/SanctionLists/ActivityLog/" + $scope.selectLogsOptions.selected;
    }

}]);