app.controller('uploadController', function ($scope, $upload) {
     

    

    $scope.upload = function () {
        var file = $scope.file;
        hideMessageError();
        $upload.upload({
            url: bndAppGlobals.baseUrl + 'UploadBBAN/UploadBBAN',
            file: file,
        }).progress(function (evt) {
            $('#uploadModal').modal('show');

        }).success(function (data, status, headers, config) {
             
            $('#uploadModal').modal('hide');
            if (data.Success == false) {
                showMessageError(data.Error);
            } else {
                window.location = bndAppGlobals.baseUrl + "UploadBBAN/BBANList?BbanFileId=" + data.Data;
            }
        });
    }

});

$(document).ready(function (d) {
    debugger;
    $("#btnUpload").prop('disabled', true);

});
function upload() {
    $("#btnUpload").attr('disabled', false);
}
