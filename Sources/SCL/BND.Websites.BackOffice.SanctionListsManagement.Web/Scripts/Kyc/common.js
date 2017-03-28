// Alert manager service
$alertManager = function () {
    var observerAddCallbacks = [];
    var observerRemoveAllCallbacks = [];

    this.registerAddCallback = function (callback) {
        observerAddCallbacks.push(callback);
    };

    this.registerRemoveAllCallback = function (callback) {
        observerRemoveAllCallbacks.push(callback);
    };

    var notifyAddObservers = function (value) {
        angular.forEach(observerAddCallbacks, function (callback) {
            callback(value);
        });
    };

    var notifyRemoveAllObservers = function (value) {
        angular.forEach(observerRemoveAllCallbacks, function (callback) {
            callback(value);
        });
    };

    return {
        addMessage: function (type, msg, timeout) {
            message = {
                type: type,
                msg: msg,
                timeout: timeout
            };
            notifyAddObservers(message);

        },

        removeMessages: function () {
            notifyRemoveAllObservers();
        },

        registerAddCallback: function (callback) {
            observerAddCallbacks.push(callback);
        },

        registerRemoveAllCallback: function (callback) {
            observerRemoveAllCallbacks.push(callback);
        }
    }
}

// Alert Controller
$alertController = function ($scope, alertManager) {

    $scope.alerts = [];

    $scope.addAlert = function (alert) {
        $scope.alerts.push(alert);
        /*
        $('html,body').animate({
            scrollTop: $("#alertManager").offset().top -10
        },
        'slow');*/
    };

    $scope.removeAllAlerts = function (alert) {
        $scope.alerts = [];
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    alertManager.registerAddCallback($scope.addAlert);
    alertManager.registerRemoveAllCallback($scope.removeAllAlerts);
};


// >> Date Time Display Format
// Constant
var DATE_FORMAT_DISPLAY = "yyyy-MM-dd";
var DATE_TIME_FORMAT_DISPLAY = "yyyy-MM-dd HH:mm:ss";

// Methods
function LeftPad(number, targetLength) {
    var output = number + '';
    while (output.length < targetLength) {
        output = '0' + output;
    }
    return output;
}

function DateTimeDisplay(date) {
    if (!date) {
        return "";
    }

    var display = "";
    try {
        var dt = new Date(date);
        
        var day = dt.getDate();
        //An integer between 0 and 11, representing the months January through December.
        var month = dt.getMonth() + 1;
        var year = dt.getFullYear();
        var hours = dt.getHours();
        var minute = dt.getMinutes();
        var seconds = dt.getUTCSeconds();

        display = DATE_TIME_FORMAT_DISPLAY.replace("yyyy", year);
        display = display.replace("MM", LeftPad(month, 2));
        display = display.replace("dd", LeftPad(day, 2));
        display = display.replace("HH", LeftPad(hours, 2));
        display = display.replace("mm", LeftPad(minute, 2));
        display = display.replace("ss", LeftPad(seconds, 2));
    }
    catch (err) {
        display = "-";
    }
    return display;

}

function DateDisplay(date) {
    if (!date) {
        return "";
    }

    var display = "";
    try {
        var dt = new Date(date);

        var day = dt.getDate();
        //An integer between 0 and 11, representing the months January through December.
        var month = dt.getMonth() + 1;
        var year = dt.getFullYear();

        display = DATE_FORMAT_DISPLAY.replace("yyyy", year);
        display = display.replace("MM", LeftPad(month, 2));
        display = display.replace("dd", LeftPad(day, 2));
    }
    catch (err) {
        display = "-";
    }
    return display;
}

function DMYDisplay(day, month, year) {
   
    var display = "";
    try {
        display = DATE_FORMAT_DISPLAY.replace("yyyy", (!year ? "?" : year));
        display = display.replace("MM", (!month ? "?" : LeftPad(month, 2)));
        display = display.replace("dd", (!day ? "?" : LeftPad(day, 2)));
    }
    catch (err) {
        display = "?";
    }
    return display;
}

// << Date Time Display Format


