var sanctionListsSearchApp = angular.module('sanctionListsSearchApp', ['ngAnimate', 'ui.bootstrap', 'blockUI', 'ngMessages', 'ngSanitize']);

sanctionListsSearchApp.service('alertManager', $alertManager);
sanctionListsSearchApp.controller('AlertsController', $alertController);

sanctionListsSearchApp.controller('sanctionListsSearchController', [
    '$scope', '$http', 'blockUI', '$modal', 'alertManager', function ($scope, $http, blockUI, $modal, alertManager) {
        $scope.searchEntity = searchEntity;
        $scope.SanctionListTypes = listTypeItems;

        // override the javascript function to angularjs.
        $scope.DateTimeDisplay = DateTimeDisplay;
        $scope.DateDisplay = DateDisplay;
        $scope.DMYDisplay = DMYDisplay;

        $scope.showList = null;
        $scope.today = new Date();

        $scope.clear = function () {
            $scope.searchEntity.BirthDate = null;
        };
        $scope.clear();

        $scope.toggleMin = function () {
            $scope.minDate = $scope.minDate ? null : new Date(1800, 5, 22);
            $scope.maxDate = $scope.maxDate ? null : new Date();
        };
        $scope.toggleMin();

        $scope.openDatePicker = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.status = {
            opened: false
        };

        $scope.maxSize = 5;
        $scope.currentPage = [1, 1, 1];
        $scope.ITEM_PER_PAGE = 25;

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.Invalid = false;

        $scope.ResultVisible = false;

        $scope.TabCount = 3;
        $scope.currentTab = 0;

        $scope.SearchCriteria = {};

        // search 
        $scope.SearchResult = [];
        $scope.SearchResult.push({});
        $scope.SearchResult.push({});
        $scope.SearchResult.push({});

        $scope.animationsEnabled = true;

        // search all (by click on search button)
        $scope.SearchAll = function () {
            //debugger;

            // hide search result.
            $scope.ResultVisible = false;
            $scope.TabCount = 1;
            $scope.currentTab = 0;

            $scope.setSearchCriteria();
            if (!!$scope.searchEntity.LastName && !!$scope.searchEntity.FirstName && !!$scope.searchEntity.BirthDate) {
                $scope.TabCount = 3;

                $scope.SearchTab(0);
                $scope.SearchTab(1);
                $scope.SearchTab(2);
                $scope.showList = 'show3';
            } else if ((!!$scope.searchEntity.LastName && !!$scope.searchEntity.FirstName) ||
            (!!$scope.searchEntity.LastName && !!$scope.searchEntity.BirthDate)) {
                $scope.TabCount = 2;

                $scope.SearchTab(0);
                $scope.SearchTab(1);
                $scope.showList = 'show2';

            } else if (!!$scope.searchEntity.LastName) {
                $scope.TabCount = 1;

                $scope.SearchTab(0);
                $scope.showList = 'show1';
            }
            $scope.currentPage = [1, 1, 1];

            for (var tabnumber = 0; tabnumber < 3 ; tabnumber++) {
                // set serach criteria for the tab
                if ($scope.SearchCriteria[tabnumber].LastName) {
                    $scope.criteriaLastName[tabnumber] = ($scope.SearchCriteria[tabnumber].LastName.split(' ')).join(', ');
                }
                if ($scope.SearchCriteria[tabnumber].FirstName) {
                    $scope.criteriaFirstName[tabnumber] = ($scope.SearchCriteria[tabnumber].FirstName.split(' ')).join(', ');
                }
                $scope.criteriaBirthDate[tabnumber] = $scope.SearchCriteria[tabnumber].BirthDate;
                $scope.criteriaSanctionType = $scope.SanctionListTypes[$scope.SearchCriteria[tabnumber].SanctionListType - 1].Name;

                $scope.entityList[tabnumber] = $scope.SearchResult[tabnumber].entityList;
                $scope.totalItems[tabnumber] = $scope.SearchResult[tabnumber].totalItems;
            }
        };
        $scope.criteriaLastName = [];
        $scope.criteriaFirstName = [];
        $scope.criteriaBirthDate = [];
        $scope.entityList = [];
        $scope.totalItems = [];

        $scope.setSearchCriteria = function () {
            $scope.SearchCriteria = [];

            if (!!$scope.searchEntity.LastName && $scope.searchEntity.FirstName && !!$scope.searchEntity.BirthDate) {
                // search all tab

                //tab 0            
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: null, BirthDate: null, SanctionListType: $scope.searchEntity.SanctionListType });

                //tab 1
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: $scope.searchEntity.FirstName, BirthDate: null, SanctionListType: $scope.searchEntity.SanctionListType });

                //tab 2
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: $scope.searchEntity.FirstName, BirthDate: $scope.searchEntity.BirthDate, SanctionListType: $scope.searchEntity.SanctionListType });

            } else if ((!!$scope.searchEntity.LastName && !!$scope.searchEntity.FirstName) || (!!$scope.searchEntity.LastName && !!$scope.searchEntity.BirthDate)) {
                // search 2 tab

                //tab 0            
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: null, BirthDate: null, SanctionListType: $scope.searchEntity.SanctionListType });

                //tab 1
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: $scope.searchEntity.FirstName, BirthDate: null, SanctionListType: $scope.searchEntity.SanctionListType });
            } else if (!!$scope.searchEntity.LastName) {

                //tab 0            
                $scope.SearchCriteria.push({ LastName: $scope.searchEntity.LastName, FirstName: null, BirthDate: null, SanctionListType: $scope.searchEntity.SanctionListType });

            }
        };

        $scope.pageChange = function (tabnumber, pageNo) {
            $scope.getResult($scope.SearchCriteria[tabnumber], pageNo, tabnumber,function (entities, entitiesCount, tabnumber) {
                $scope.ResultVisible = true;
                $scope.SearchResult[tabnumber].entityList = entities;
                $scope.SearchResult[tabnumber].totalItems = entitiesCount;

                $scope.entityList[tabnumber] = $scope.SearchResult[tabnumber].entityList;
                $scope.totalItems[tabnumber] = $scope.SearchResult[tabnumber].totalItems;

                $scope.currentPage[tabnumber] = pageNo;
            });
        }

        // search per tab (by click on pagination button)
        $scope.SearchTab = function (tabnumber) {

            // calls to api controller to get data.
            $scope.getResult($scope.SearchCriteria[tabnumber], 1, tabnumber, function (entities, entitiesCount, tabnumber) {
                $scope.ResultVisible = true;
                $scope.SearchResult[tabnumber].entityList = entities;
                $scope.SearchResult[tabnumber].totalItems = entitiesCount;

                $scope.entityList[tabnumber] = $scope.SearchResult[tabnumber].entityList;
                $scope.totalItems[tabnumber] = $scope.SearchResult[tabnumber].totalItems;
            });
        };

        $scope.getResult = function (searchCriteria, currentPage, tabnumber, callback) {
            blockUI.start();
            $http({
                method: 'GET',
                url: '/api/SanctionLists/SearchEntities',
                params: {
                    lastName: searchCriteria.LastName,
                    firstname: searchCriteria.FirstName,
                    birthDate: searchCriteria.BirthDate,
                    listTypeId: searchCriteria.SanctionListType,
                    offset: (currentPage - 1) * $scope.ITEM_PER_PAGE,
                    limit: $scope.ITEM_PER_PAGE
                },
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            })
                .then(function (response) {
                    callback(response.data.Entities, response.data.EntitiesCount, tabnumber);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error !!");
                    //alert('error');
                })
                .finally(function () {
                    blockUI.stop();
                });
        };

        $scope.exportSanctionList = function (numeric) {
            if (searchEntity.SanctionListType == null) {
                alert('Please select SanctionListType');
                return;
            }
            if (searchEntity.LastName == null) {
                alert('Please input LastName');
                return;
            }
            var url = '/SanctionLists/Export?lastName=' + $scope.SearchCriteria[numeric].LastName + '&listTypeId=' + $scope.SearchCriteria[numeric].SanctionListType;
            if ($scope.SearchCriteria[numeric].FirstName != null) {
                url = url + '&firstname=' + $scope.SearchCriteria[numeric].FirstName;
            }
            if ($scope.SearchCriteria[numeric].BirthDate != null) {
                var dt = $scope.SearchCriteria[numeric].BirthDate;
                url = url + '&birthDate=' + new Date(dt).getFullYear() + '-' + (new Date(dt).getMonth() + 1) + '-' + new Date(dt).getDate();
            }

            location.href = url;
        };

        $scope.showEntityDetails = function (entityId) {

            $http({
                method: 'GET',
                url: '/SanctionLists/SearchEntities/' + entityId,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            })
                .then(function (response) {
                    $modal.open({
                        animation: true,
                        templateUrl: 'myModalContent.html',
                        controller: 'ModalInstanceCtrl',
                        size: 'lg',
                        resolve: {
                            jsonContent: function () {
                                return response.data;
                            }
                        }
                    });
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error !!");
                })
                .finally(function () {
                    blockUI.stop();
                });
        }
    }
]);

sanctionListsSearchApp.controller('ModalInstanceCtrl', function ($scope, $modalInstance, $sce, jsonContent) {
    $scope.content = $sce.trustAsHtml(jsonContent);

    $scope.close = function () {
        $modalInstance.dismiss('close');
    };
});