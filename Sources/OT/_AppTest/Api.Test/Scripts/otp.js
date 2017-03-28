var app = angular.module("OtpApp", ['ngAnimate', 'ngAria', 'ngMaterial', 'ngMessages', 'ngRoute']);

app.controller("RequestCtrl", function ($scope, $location, $window, $http) {

    // can remove initial data from this line.
    var mobile = "66812345678";
    var email = "mr.x@googgai.com";
    $scope.otpRequest = {
        ApiKey: "1phM4nLk14tefH8cntFJfuINtH0w_POg1zdKO9EPiu28TYjwLH0mOWzvcFiD0h3pPvf9wlhxhYk5hA6Ur0BHg8InK91GwhfCbW4kQU_6KkbKTb1H9gkOqTnFZxY4lPyl",
        Suid: generateGuid(),
        Email: email,
        Mobile: mobile,
        Message: "This is code: {Otp}, \r\nand this is refcode: {RefCode}.",
        AccountId: "5861F73F-CAD5-419D-96D4-56BD07211297",
        Sender: "BrandNewDay",
        Payload: "This is sample payload.",
    };
    // to this line.
    GetChannels();

    $scope.update = function (val) {
        $scope.otpRequest.Address = val;
    }

    function GetChannels() {
        $http({
            method: 'GET',
            url: '/Home/GetChannel',
            datatype: 'HTML',
        }).success(function (data) {
            $scope.channels = data;
        })
    }


    $scope.onChangeSelect = function (channel) {
        if (channel.toUpperCase() == "EMAIL") {
            $('#mobileTxtBox').hide();
            $('#mobileAddressTxtBox').hide();
            $scope.otpRequest.Mobile = mobile;
            $('#emailTxtBox').show();
            $('#emailAddressTxtBox').show();
        }
        else if (channel.toUpperCase() == "SMS") {
            $('#emailTxtBox').hide();
            $('#emailAddressTxtBox').hide();
            $scope.otpRequest.Email = email;
            $('#mobileTxtBox').show();
            $('#mobileAddressTxtBox').show();
        }
    }
});

app.controller("VerifyCtrl", function ($scope) {
    $scope.otpVerify = {};
});

app.controller("ResultCtrl", function ($scope, $window) {
    $scope.otpSuccess = {};
    $scope.reTest = function () {
        var landingUrl = "http://" + $window.location.host + "/";
        $window.location.href = landingUrl;
    }
});

function generateGuid() {
    var d = new Date().getTime();
    if (window.performance && typeof window.performance.now === "function") {
        d += performance.now();
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid.toUpperCase();
}
