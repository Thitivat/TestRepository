//setup paging number 
$('#page-selection').bootpag({
    total: totalpage,
    page: currentpage,
    maxVisible: 10
}).on('page', function (event, num) {

    //when click paging
    window.location = bndAppGlobals.baseUrl + "BBANFiles/Index?page=" + num;

});
$('#page-selection-Iban').bootpag({
}).on('page', function (event, num) {

    angular.element(document.getElementById('bbanFileController')).scope().GetIBan(bbFileId, num);

});
var bbFileId;
app.controller('bbanFileController', function ($scope, $http) {

    $scope.History = function (bbanfileId) {
         
        $scope.Histories = [];
        $http({
            url: bndAppGlobals.baseUrl + 'BBANFiles/GetBbanFileHistory',
            method: "GET",
            params: { bbanFileId: bbanfileId }
        }).then(function (d) {
            debugger;
            $('#checkingModal').modal('hide');
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
            } else {
                $scope.Histories = d.data.Data;
                $('#historyModal').modal('show');

            }
        });
    }
    $scope.IBans = [];
    $scope.GetIBan = function (bbanfileId, num) {
        debugger;
        if (num == null) num = 1;
        bbFileId = bbanfileId;
      //  $scope.IBans = [];
        $http({
            url: bndAppGlobals.baseUrl + 'BBANFiles/GetIBanByBBanFileId',
            method: "POST",
            params: { bbanFileId: bbanfileId, page: num }
        }).then(function (d) {
            debugger;

   
            if (d.data.Success == false) {
                showMessageError(d.data.Error);
            } else {
                $scope.IBans = d.data.Data;
                $('#page-selection-Iban').bootpag({
                    total: d.data.TotalPage,
                    page: d.data.Currentpage,
                    maxVisible: 10
                });


                $('#iBanListModal').modal('show');
     


            }
        });
    }
});