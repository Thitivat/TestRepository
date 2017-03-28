idealApp.controller('paymentController', function ($scope, $location) {

    $scope.process = 1;

    $scope.payment = {
        amount: 50,
        desc: 'deposit',
        product: 'Product',
        acc: 'ABN1234',

        purchaseId: '0000000001',
        bndiban: 'BND123456',
        currency: 'EUR',
        language: 'nl',
        expirationPeriod: 60,
        merchantReturnUrl: $location.$$absUrl + 'paymentresult',
        apiToken: 'BND123456'
    };

    $scope.updateProcess = function (process) {
        setProcess(process);
    }

    $scope.setPaymentType = function (paymentType) {
        $scope.payment.paymentType = paymentType;
        $scope.process = 3;
    };

    function setProcess(process) {
        $scope.process = process;
    }
});