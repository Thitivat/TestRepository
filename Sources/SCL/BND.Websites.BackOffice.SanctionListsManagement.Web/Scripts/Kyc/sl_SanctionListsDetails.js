var sanctionListsDetailsApp = angular.module('sanctionListsDetailsApp', ['ngAnimate', 'ui.bootstrap', 'ngMessages']);

sanctionListsDetailsApp.service('alertManager', $alertManager);
sanctionListsDetailsApp.controller('AlertsController', $alertController);

sanctionListsDetailsApp.controller('sanctionListsDetailsController', function ($scope) {
    $scope.listDetails = listDetails;
});

sanctionListsDetailsApp.controller('informationController', function ($scope) {
    $scope.listDetails = listDetails;

    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;
});


sanctionListsDetailsApp.controller('sanctionListsDetailsRssController', ['$scope', '$http', 'alertManager', function ($scope, $http, alertManager) {
    $scope.RssList = [];
    $scope.ListTypeId = "";
    $scope.Rss = { Title: '', PubDate: '', Link: '' };
    $scope.RssList = [[]];

    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;

    $scope.refreshRss = function () {
        if ($scope.ListTypeId != "") {
            // call api to get Rss information
            $http({
                method: 'GET',
                url: '/api/SanctionLists/' + $scope.ListTypeId + '/Rss',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
            })
            .then(function (response) {
                $scope.Rss.Title = response.data.Title;
                $scope.Rss.PubDate = response.data.PubDate;
                $scope.Rss.Link = response.data.Link;

                $scope.RssList = response.data.Items;
              })
            .catch(function (response) {
                alertManager.addMessage("danger", "Error while downloading RSS.");
            })
            .finally(function () {
            });
        }
    };

    $scope.$watch('varListTypeId', function () {
        $scope.ListTypeId = varListTypeId;
        $scope.refreshRss();
    });
}]);

sanctionListsDetailsApp.controller('entitiesListController', ['$scope', '$http', 'alertManager', function ($scope, $http, alertManager) {

    $scope.entityList = entities;

    $scope.maxSize = 5;
    $scope.totalItems = entitiesCount;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 25;

    $scope.AutoUpdate = 0;
    $scope.ManualUpdate = 0;

    // override the javascript function to angularjs.
    $scope.DateTimeDisplay = DateTimeDisplay;
    $scope.DateDisplay = DateDisplay;
    $scope.DMYDisplay = DMYDisplay;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
        alert("change to " + pageNo);
    };

    $scope.$watch("varAutoUpdate", function () {
        $scope.AutoUpdate = varAutoUpdate;
    });
    $scope.$watch("varManualUpdate", function () {
        $scope.ManualUpdate = varManualUpdate;
    });

    $scope.pageChanged = function () {
        //console.log('Page changed to: ' + $scope.currentPage); alert("change to " + $scope.currentPage);

        $http({
            method: 'GET',
            url: '/api/SanctionLists/' + currentList + '/Entities?limit=' + $scope.itemsPerPage + '&offset=' + (($scope.currentPage - 1) * $scope.itemsPerPage),
            data: { "limit": $scope.itemsPerPage, "offset": ($scope.currentPage - 1) * $scope.itemsPerPage },
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entityList = response.data.Entities;
            $scope.totalItems = response.data.EntitiesCount;
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error while changing Sanction List.");
        })
        .finally(function () {
        });
    };

    $scope.EntityEdit = function (entityId) {
        
        window.location = "/SanctionLists/" + currentList + "/EntityEdit/" + entityId;
    };
}]);

function selectListChanged(elem) {
    window.location = "/SanctionLists/" + $(elem).val();
}

