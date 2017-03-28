$('#page-selection').bootpag({
    total: totalpage,
    page: currentpage,
    maxVisible: 10
}).on('page', function (event, num) {
    angular.element(document.getElementById('ibanController')).scope().Search(num);

});
app.controller('ibanController', function ($scope, $http) {

    $scope.Statuses = [
        {
            Desc: '--Select--'
        }, {
            Desc: 'Available'
        }, {
            Desc: 'Assigned'
        }, {
            Desc: 'Canceled'
        }, {
            Desc: 'Active'
        }
    ];
    $scope.IBans = model;
    $scope.Status = $scope.Statuses[0].Desc;

    $scope.GetHistory = function (id) {
        $scope.Histories = [];
        $http({
            url: bndAppGlobals.baseUrl + 'ApprovalQueue/GetHistories',
            method: "GET",
            params: { ibanId: id }
        }).then(function (d) {
            debugger;
            closeAllModal();
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
            } else {
                $('#historyModal').modal('show');
                $scope.Histories = d.data.Data;

            }
        });
    }
    $scope.Search = function (num) {

        //$scope.IBans = [];
        //showLoading();
        var param = {};
        if (num == null) num = 1;
        param.status = $scope.Status;
        param.page = num;
        param.iban = $scope.Iban;
        if (param.status == '--Select--') param.status = '';
        $http({
            url: bndAppGlobals.baseUrl + 'IBan/GetIban',
            method: "GET",
            params: param
        }).then(function (d) {

            closeAllModal();
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
            } else {
                $scope.IBans = d.data.Data;
                $('#page-selection').bootpag({
                    total: d.data.TotalPage,
                    page: d.data.Currentpage,
                    maxVisible: 10
                });

            }
        });
    }

});
