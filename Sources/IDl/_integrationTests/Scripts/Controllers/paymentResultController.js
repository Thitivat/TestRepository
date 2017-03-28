'use strict';

idealApp.controller('paymentResultController', function ($scope, $http, $cookieStore, $location) {

    function dateTimeNow() {
        var utc = new Date().toJSON();
        return utc;
    }

    $scope.$on('$routeChangeSuccess', function () {
        $scope.message = JSON.stringify($cookieStore.get('paymentResult'), undefined, 4);

        let params = $location.search();
        $scope.status.transactionId = params.trxid;
        $scope.status.entranceCode = params.ec;
    });

    $scope.isLoading = false;
    $scope.status = {};
    $scope.status.apiToken = "BND123456"

    $scope.getStatus = function (status) {
        $scope.isLoading = true;
        $http({
            method: 'POST',
            url: '/api/iDeal/GetTransactionStatus',
            data: {
                TransactionId: status.transactionId,
                EntranceCode: status.entranceCode,
                ApiToken: status.apiToken,
            }
        }).then(function successCallback(response) {
            $scope.statusResult = JSON.stringify(response.data, undefined, 4);
            $scope.isLoading = false;
            $scope.LastedUpdate = dateTimeNow();
        }, function errorCallback(response) {
            $scope.statusResult = JSON.stringify(response.data, undefined, 4);
            $scope.isLoading = false;
            $scope.LastedUpdate = dateTimeNow();
        });
    }
});