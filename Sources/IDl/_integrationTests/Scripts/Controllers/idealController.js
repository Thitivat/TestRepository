idealApp.controller('idealController', function ($scope, $http, $location, $cookieStore) {

    function dateTimeNow() {
        var utc = new Date().toJSON();
        return utc;
    }

    function reloadDirectories() {
        $scope.isLoading = true;
        $http({
            method: 'GET',
            url: '/api/iDeal/GetDirectories'
        }).then(function successCallback(response) {
            $scope.Directories = response.data.Directories;
            $scope.JsonResult = JSON.stringify(response.data, undefined, 4);
            $scope.isReloading = false;
            $scope.LastedUpdate = dateTimeNow();
        }, function errorCallback(response) {
            $scope.JsonResult = JSON.stringify(response.data, undefined, 4);
            $scope.isReloading = false;
            $scope.LastedUpdate = dateTimeNow();
        });

    }

    $scope.reload = function () {
        reloadDirectories();
    }

    $scope.pay = function (payment) {
        $scope.isPaying = true;

        $http({
            method: 'POST',
            url: '/api/iDeal/SendTransactionRequest',
            data: {
                IssuerID: payment.issuer,
                BNDIBAN: payment.bndiban,
                CustomerIBAN: payment.acc,
                PaymentType: payment.paymentType,
                Currency: payment.currency,
                Language: payment.language,
                Amount: payment.amount,
                ExpirationPeriod: payment.expirationPeriod,
                Description: payment.desc,
                MerchantReturnURL: payment.merchantReturnUrl,
                PurchaseID: payment.purchaseId,
                ApiToken: payment.apiToken
            }
        }).then(function successCallback(response) {
            $scope.JsonResult = JSON.stringify(response.data, undefined, 4);
            $cookieStore.put('paymentResult', response.data);
            $scope.issuerUrl = response.data.IssuerAuthenticationURL;
            $scope.isPaying = false;
            $scope.LastedUpdate = dateTimeNow();
        }, function errorCallback(response) {
            $scope.JsonResult = JSON.stringify(response.data, undefined, 4);
            $scope.isPaying = false;
            $scope.LastedUpdate = dateTimeNow();
        });
    };

    reloadDirectories();

    $scope.isReloading = false;

    $scope.isPaying = false;

});