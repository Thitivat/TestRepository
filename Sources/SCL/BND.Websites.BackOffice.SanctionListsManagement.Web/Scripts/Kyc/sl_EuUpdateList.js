var app = angular.module('euUpdateApp', ['ngAnimate', 'ui.bootstrap', 'ngMessages']);

app.service('alertManager', $alertManager);
app.controller('AlertsController', $alertController);

app.controller('euUpdateController', ['$scope', '$http', '$timeout', '$interval', 'alertManager', function ($scope, $http, $timeout, $interval, alertManager) {
    
    $scope.progressValue = 0;
    $scope.PressedUpdate = "N";
    $scope.listTypeId = "";
    $scope.IsUpdating = "N";

    $scope.$watch('varListTypeId', function () {
        $scope.listTypeId = varListTypeId;
    });

    $scope.$watch('varIsUpdting', function () {
        if (varIsUpdting == "Updating") {
            $scope.IsUpdating = "Y";
        } else {
            $scope.IsUpdating = "N";
        }
    });

    var refreshStatusUpdate = function () {
        if ($scope.listTypeId != "") {
            // call api to update eu sanction list
            $http({
                method: 'GET',
                url: '/api/SanctionLists/' + $scope.listTypeId + '/Updates',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
            })
            .then(function (response) {
                $scope.progressValue = response.data.Progress;
                $scope.ProgressStatus = response.data.Message;
                if (response.data.Status == "Finished" || response.data.Status == "") {
                    $scope.IsUpdating = "N";
                }
            })
            .catch(function (data) {
                //alert('error');
            })
            .finally(function () {
            });
        }
    };

    var promise = $interval(refreshStatusUpdate, 4000);

    // Cancel interval on page changes
    $scope.$on('$destroy', function () {
        if (angular.isDefined(promise)) {
            $interval.cancel(promise);
            promise = undefined;
        }
    });

    // stops the interval
    $scope.Stop = function () {
        $interval.cancel(promise);
    };

    $scope.UpdateList = function (listid) {
        $scope.IsUpdating = "Y";
        // call api to update eu sanction list
        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listid + '/Updates',
            //data: $.param($scope.newRegulation),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            alert(response.data);
            //refreshStatusUpdate();
            //$scope.Stop();
            // TODO add error handling
        })
        .catch(function (data) {
            //alert('error');
        })
        .finally(function () {
        });
    };
}]);
