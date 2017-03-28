angular.module('search').factory('SearchService', ['$resource', '$http',
    function ($resource, $http) {
        return {
            getFilterTypes: function () {
                return $http({
                    method: 'GET',
                    url: 'api/PaymentIdInfo/GetEnumFilterType'
                });
            },
            getByBndIban: function (id, filterTypes) {
                return $http({
                    method: 'GET',
                    url: 'api/PaymentIdInfo/GetByBndIban/' + id + "?filterTypes=" + filterTypes
                });
            },
            getBySourceIban: function (id, filterTypes) {
                return $http({
                    method: 'GET',
                    url: 'api/PaymentIdInfo/GetBySourceIban/' + id + "?filterTypes=" + filterTypes
                });
            },
            getByTransactionId: function (id, filterTypes) {
                return $http({
                    method: 'GET',
                    url: 'api/PaymentIdInfo/GetByTransactionId/' + id + "?filterTypes=" + filterTypes
                });
            },
        };
    }
]);