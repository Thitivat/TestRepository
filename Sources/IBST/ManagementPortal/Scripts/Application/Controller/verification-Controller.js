app.controller('verificationController', function ($scope, $http) {
    $scope.sendToApprove = function () {
         
        showLoading('Sending...');
        var bbanfileId = getParameterByName('BbanFileId');
        var param = {};
        param.bbanFileId = bbanfileId;
        param.emailReceiver = $scope.emailReceiver;
        param.emailMessage = $scope.emailMessage;
        $('#btnSendApprove').prop('disabled', true);
        $http({
            url: bndAppGlobals.baseUrl + 'UploadBBAN/SendToApprove',
            method: "POST",
            params: param
        }).then(function (d) {
             
            closeAllModal();
            if (d.data.Success == false) {
                $('#btnSendApprove').prop('disabled', false);
                showMessageError(d.data.Error);
            } else {

                window.location = bndAppGlobals.baseUrl + 'BBANFiles/Index';
            }
        });

    }
    $scope.cancelBbanFile = function (d) {
        var r = confirm("Are you sure you want to cancel this file?");
        if (r == false) {
            return;
        }
        var bbanfileId = getParameterByName('BbanFileId');
        var param = {};
        param.bbanFileId = bbanfileId;
        $('#btnCancelBban').prop('disabled', true);
        $http({
            url: bndAppGlobals.baseUrl + 'UploadBBAN/CancelBbanFile',
            method: "POST",
            params: param
        }).then(function (d) {
             
            if (d.data.Success == false) {
                $('#btnCancelBban').prop('disabled', false);
                showMessageError(d.data.Error);
            } else {
                window.location = bndAppGlobals.baseUrl + 'BBANFiles/Index';
            }
        });
    }
});