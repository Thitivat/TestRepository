//setup paging number
var bbId;
$('#page-selection').bootpag({
    total: totalpage,
    page: currentpage,
    maxVisible: 10
}).on('page', function (event, num) {

    //when click paging
    window.location = bndAppGlobals.baseUrl + "ApprovalQueue/Index?page=" + num;

});

$('#page-selection-Bban').bootpag({

}).on('page', function (event, num) {
    debugger;
    angular.element(document.getElementById('approveController')).scope().Approve(bbId, num);

});

app.controller('approveController', function ($scope, $http) {

    $scope.Statuses = [
    {
        Desc: '--Select--'
    },
    {
        Desc: 'Approve'
    }, {
        Desc: 'Deny'
    }];
    $scope.Status = $scope.Statuses[0].Desc;
    $scope.Approve = function (bbanfileId, num) {
        if (num == null) num = 1;
        bbId = bbanfileId;
        $scope.Status = '--Select--';
        $scope.BbanFileId = bbanfileId;
        $scope.Remark = "";
        //$scope.BBAN = [];
        debugger;
        $http({
            url: bndAppGlobals.baseUrl + 'ApprovalQueue/GetBbans',
            method: "GET",
            params: { bbanFileId: bbanfileId, page: num }
        }).then(function (d) {

            debugger;
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
            } else {
                $scope.BBAN = d.data.Data;

                $('#page-selection-Bban').bootpag({
                    total: d.data.TotalPage,
                    page: d.data.Currentpage,
                    maxVisible: 10
                });

                $('#bbanListModal').modal('show');

            }
        });
    }

    $scope.Save = function () {


        var url = bndAppGlobals.baseUrl + 'ApprovalQueue/ApproveBBAN';
        if ($scope.Status == 'Deny') {
            url = bndAppGlobals.baseUrl + 'ApprovalQueue/DenyBBAN';
        }
        else if ($scope.Status == "--Select--") {

            return; 
        }

        closeAllModal();

        showLoading('Approving...');
        var param = {};
        param.remark = $scope.Remark;
        param.bbanFileId = $scope.BbanFileId;
        $http({
            url: url,
            method: "POST",
            params: param
        }).then(function (d) {

            closeAllModal();
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
                var millisecondsToWait = 3000;
                setTimeout(function () {
                    window.location.reload();
                }, millisecondsToWait);
            } else {

                $("#approveSuccessModal").modal('show');
            }
        });
    }
    $scope.Success = function (d) {
        window.location.reload();
    }

});