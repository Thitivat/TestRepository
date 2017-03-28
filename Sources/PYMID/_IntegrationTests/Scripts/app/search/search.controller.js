angular.module('search').controller('SearchController', ['$scope', 'SearchService',
    function ($scope, SearchService) {

        var searchMethods = ['BNDIBAN', 'Source IBAN', 'Transaction'];

        $scope.isSearching = false;

        $scope.init = function () {
            $scope.searchMethods = searchMethods;

            $scope.criteria = {};
            $scope.criteria.method = $scope.searchMethods[0];

            SearchService.getFilterTypes().then(function (result) {
                $scope.filterTypes = result.data;
            });
        }

        $scope.search = function (criteria) {
            $scope.errorMesssage = "";
            $scope.isSearching = true;

            var filterTypes = $scope.filterTypes
                .filter(function (filter) {
                    return filter.isSelected;
                })
                .map(function (filter) {
                    return filter.name
                })
                .join(',');

            switch (criteria.method) {
                case searchMethods[0]:
                    SearchService.getByBndIban(criteria.number, filterTypes).then(function (result) {
                        $scope.result = result.data;
                        $scope.isSearching = false;
                    }, searchErrorHandling);
                    break;
                case searchMethods[1]:
                    SearchService.getBySourceIban(criteria.number, filterTypes).then(function (result) {
                        $scope.result = result.data;
                        $scope.isSearching = false;
                    }, searchErrorHandling);
                    break;
                case searchMethods[2]:
                    SearchService.getByTransactionId(criteria.number, filterTypes).then(function (result) {
                        $scope.result = result.data;
                        $scope.isSearching = false;
                    }, searchErrorHandling);
                    break;
                default: alert("search method is not found.");
            }
        }

        function searchErrorHandling(response) {
            $scope.errorMesssage = response.data.Message;
            $scope.isSearching = false;
        }
    }
]);