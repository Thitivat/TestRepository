//setup paging number
$('#page-selection').bootpag({
    total: totalpage,
    page: currentpage,
    maxVisible: 10
}).on('page', function (event, num) {

    debugger;
    var bbanfileId = getParameterByName('BbanFileId');

    window.location = bndAppGlobals.baseUrl + "UploadBBAN/BBANList?BbanFileId=" + bbanfileId + "&page=" + num;

});
app.controller('bbanListController', function ($scope, $http) {
    $scope.Verify = function () {
         
        $('#checkingModal').modal('show');
        var bbanfileId = getParameterByName('BbanFileId');

        $http({
            url: bndAppGlobals.baseUrl + 'UploadBBAN/VerifyBbanExist',
            method: "POST",
            params: { bbanFileId: bbanfileId }
        }).then(function (d) {
            $('#checkingModal').modal('hide');
            if (d.data.Success == false) {
                debugger;
                showMessageError(d.data.Error, bndAppGlobals.baseUrl + "BBANFiles/Index");
            } else {

                window.location = bndAppGlobals.baseUrl + 'UploadBBAN/Verification?BbanFileId=' + bbanfileId;
            }
        });

    }
    $scope.CancelVerify = function () {
         
        window.location = bndAppGlobals.baseUrl + "BBANFiles/Index";
    }
});