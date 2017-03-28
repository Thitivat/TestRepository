var app = angular.module('IBANStore', ['angularFileUpload']);
app.filter("dateFilter", function () {
     
    return function(item) {
        if (item != null) {
            return moment(new Date(moment(item).toDate())).format("DD/MM/YYYY hh:mm").toString();
            //  return moment.utc(new Date(moment(item).toDate())).format("DD/MM/YYYY hh:mm").toString();
        }
        return "";
    }
});
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers
                 .common['X-Requested-With'] = 'XMLHttpRequest';
}]);