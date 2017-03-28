/***********************************************
* ps-validator JavaScript Library
* License:      MIT (http://www.opensource.org/licenses/mit-license.php)
* Author:       Komyos Chongbunwatana (Bank)
* Email:        bank@kobkiat-it.com
* Compiled At:  22/09/2015 17:51 +07:00
* Version:      2.0
***********************************************/
/*
Requires:
- AngularJS by using ngModel for validation per input.
- Bootstrap.

Example:
validator.js
---------------------------------------------------------------------------
var app = angular.module('testApp', ['psValidator']);
app.controller('testController', function ($scope) {
    $scope.ErrorMsg = 'Must be fuck!';
    $scope.$watch('PsValidators', function () {
        $scope.PsValidators['Test1'].AddPredicateFunction('IsFuck', function (value) {
            return value == 'Fuck';
        });
    });

    $scope.Models = [];

    $scope.click = function () {
        $scope.Models.push({ Name: null, Age: null, Gender: null });
        $scope.$watch('PsValidators["Test2' + ($scope.Models.length - 1) + '"]', function () {
            $scope.PsValidators['Test2' + ($scope.Models.length - 1)].AddPredicateFunction('IsFuck', function (value) {
                return value == 'Fuck';
            });
        });
    };

    $scope.click2 = function (index) {
        alert($scope.PsValidators['Test2' + index].IsValid());
    }
});

html page
---------------------------------------------------------------------------
<div ng-app="testApp">
    <div ng-controller="testController">
        {{PsValidators['Test1'].IsValid()}}
        <div ps-validator="Test1">
            <input ng-model="Test2.Name" ps-validator-control-id="testid" ps-validator-number="{{ErrorMsg}}" ps-validator-required="fuck you" class="form-control" />
            <input ng-model="Test.Gender" ps-validator-predicatefx="IsFuck" ps-validator-predicatefx-message="{{ErrorMsg}}" class="form-control" />
            <button ng-click="click()" ps-validator-submit>Click me</button>
        </div>
        <div>
            <div id="DynamicForm" ng-repeat="model in Models" ps-validator="{{'Test2' + $index}}">
                {{PsValidators['Test2' + $index].IsValid()}}
                <input ng-model="model.Name" ps-validator-number="{{ErrorMsg}}" ps-validator-required="fuck you" class="form-control" />
                <input ng-model="model.Age" ps-validator-numeric="{{ErrorMsg}}" ps-validator-required="numeric" class="form-control" />
                <input ng-model="model.Gender" ps-validator-predicatefx="IsFuck" ps-validator-predicatefx-message="{{ErrorMsg}}" class="form-control" />
                <button ng-click="click2($index)" ps-validator-submit>Click me</button>
            </div>
        </div>
    </div>
</div>
*/

// Add new validator to collection of validator as a container.
function addValidator(container, validatorId, scope, element, timeout) {
    if (!validatorId) {
        throwError('ps-validator attribute value could not be null.');
    }

    container[validatorId] = new PsValidator(validatorId, scope, element, timeout);

    // Initializes validator.
    //if (typeof (container[validatorId]) === 'undefined') {
    //    container[validatorId] = new PsValidator(validatorId, scope, element, timeout);
    //}
    //else {
    //    throwError('ps-validator attribute value "' + validatorId + '" could not duplicate.');
    //}
}

// Creates directive for validation by using angularjs on top.
angular.module('psValidator', [])
       .directive('psValidator', function ($compile, $timeout) {
           return {
               restrict: 'A',
               link: function (scope, element, attrs) {
                   // Creates collection of validators.
                   if (typeof (scope.PsValidators) === 'undefined') {
                       scope.PsValidators = {};
                   }

                   addValidator(scope.PsValidators, attrs.psValidator, scope, element, $timeout);
               }
           }
       });

// Error handling.
var errorMessagePrefix = '[PsValidator-Exception] ';
var warningMessagePrefix = '[PsValidator-Warning] ';
function throwError(msg) {
    throw new Error(errorMessagePrefix + msg);
}
function logWarning(msg) {
    console.warn(warningMessagePrefix + msg);
}

// Creates class component for validation.
var PsValidator = function (validatorId, $scope, $element, $timeout) {
    // Declares variables.
    var submit_selector = '[ps-validator-submit]';
    var control_id_attr_name = 'ps-validator-control-id';
    var model_attr_name = 'ng-model';
    var error_container_css = 'has-error';
    var error_message_block_attr_name = 'ps-validator-message-id';
    var error_message_block_css = 'text-danger';
    var error_message_placeHolder_attr_name = 'ps-validator-error-message-for';
    var custom_feature_name = 'ps-validator-custom-message';

    var numberRegexp = /^\d+$/;
    var numberRangeRegexp = /^\d+,\d+$/;
    var numericRegexp = /^[-+]?[0-9]*\.?[0-9]+$/;
    var numericRangeRegexp = /^[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+$/;
    var emailRegexp = /\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b/;
    var phoneRegexp = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    var ipV4Regexp = /\b(?:\d{1,3}\.){3}\d{1,3}\b/;

    var validators;
    var isIntializing;
    var errorMessages = [];
    var customErrorMessages = [];
    var predicateFunctions = [];


    // All features that provide.
    var features = [
        {
            // For required input
            // Example:
            // <input ng-model="Model.Name" ps-validator-required="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-required',
            Function: function (element) {
                var value = getScopeValue(element);
                return (value == null) ? false : value != '';
            }
        },
        {
            // For validate number.
            // Example:
            // <input ng-model="Model.Name" ps-validator-number="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-number',
            Function: function (element) {
                return singleValidation(element, numberRegexp);
            }
        },
        {
            // For validate numeric.
            // Example:
            // <input ng-model="Model.Name" ps-validator-numeric="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-numeric',
            Function: function (element) {
                return singleValidation(element, numericRegexp);
            }
        },
        {
            // For validate email.
            // Example:
            // <input ng-model="Model.Name" ps-validator-email="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-email',
            Function: function (element) {
                return singleValidation(element, emailRegexp);
            }
        },
        {
            // For validate phone number.
            // Example:
            // <input ng-model="Model.Name" ps-validator-phone="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-phone',
            Function: function (element) {
                return singleValidation(element, phoneRegexp);
            }
        },
        {
            // For validate ipv4.
            // Example:
            // <input ng-model="Model.Name" ps-validator-ip-v4="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-ip-v4',
            Function: function (element) {
                return singleValidation(element, ipV4Regexp);
            }
        },
        {
            // For validate by yourself by custom predicate function.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-predicatefx="Put the predicate function name that you added by using $scope.PsValidator.AddPredicateFunction method"
            //        ps-validator-predicatefx-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-predicatefx-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var predicateFx_attr_name = 'ps-validator-predicatefx';
                    var functionName = element.attributes[predicateFx_attr_name].value;
                    if (!!functionName) {
                        var index = predicateFunctions.map(function (p) { return p.Name; }).indexOf(functionName);
                        if (index > -1) {
                            return predicateFunctions[index].Function(value);
                        }
                        else {
                            throwError('The predicate function: "' + value + '" could not be found. ' +
                                       'Please add to object by using AddPredicateFunction function.');
                        }
                    }
                    else {
                        throwError('The "' + predicateFx_attr_name + '" is required.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For validate by match with another input such as confirm password.
            // Example:
            // <input ng-model="Model.Password" ps-validator-control-id="Password"></input>
            // <!-- Put validator control id that got from ps-validator-control-id attribute of target input in ps-validator-match attribute -->
            // <input ng-model="Model.ConfirmPassword"
            //        ps-validator-match="Password"
            //        ps-validator-match-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-match-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var match_attr_name = 'ps-validator-match';
                    var validatorControlId = element.attributes[match_attr_name].value;
                    if (!!validatorControlId) {
                        var targetElements = getElementsByValidatorControlId(validatorControlId);
                        if (targetElements.length > 0) {
                            var isMatch = false;
                            for (var i = 0; i < targetElements.length; i++) {
                                if (value == getScopeValue(targetElements[i])) {
                                    isMatch = true;
                                }
                                else {
                                    isMatch = false;
                                    break;
                                }
                            }

                            return isMatch;
                        }
                        else {
                            throwError('The ps-validator-control-id: "' + validatorControlId +
                                       '" could not be found.');
                        }
                    }
                    else {
                        throwError('The "' + match_attr_name + '" is required.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For validate by yourself by using regular expression.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-regexp="^\d+$"
            //        ps-validator-regexp-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-regexp-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var regexp_attr_name = 'ps-validator-regexp';
                    var pattern = element.attributes[regexp_attr_name].value;
                    if (!!pattern) {
                        return new RegExp(pattern).test(value);
                    }
                    else {
                        throwError('The "' + regexp_attr_name + '" is required.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking minimum length of data.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-string-min="3"
            //        ps-validator-string-min-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-string-min-message',
            Function: function (element) {
                var value = getScopeValue(element).toString();
                if (!!value) {
                    var string_min_attr_name = 'ps-validator-string-min';
                    var min = element.attributes[string_min_attr_name].value;
                    if (numberRegexp.test(min)) {
                        return value.length >= min;
                    }
                    else {
                        throwError('The "' + string_min_attr_name + '" is required or must be number.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking maximum length of data.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-string-max="3"
            //        ps-validator-string-max-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-string-max-message',
            Function: function (element) {
                var value = getScopeValue(element).toString();
                if (!!value) {
                    var string_max_attr_name = 'ps-validator-string-max';
                    var max = element.attributes[string_max_attr_name].value;
                    if (numberRegexp.test(max)) {
                        return value.length <= max;
                    }
                    else {
                        throwError('The "' + string_max_attr_name + '" is required or must be number.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking range of data.
            // Example: This is a demonstrate for checking range of data, at least 3 but not over than 5.
            // <input ng-model="Model.Name"
            //        ps-validator-string-range="3,5"
            //        ps-validator-string-range-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-string-range-message',
            Function: function (element) {
                var value = getScopeValue(element).toString();
                if (!!value) {
                    var string_range_attr_name = 'ps-validator-string-range';
                    var range = element.attributes[string_range_attr_name].value;
                    if (numberRangeRegexp.test(range)) {
                        var rangeParts = range.split(',');
                        return value.length >= rangeParts[0] && value.length <= rangeParts[1];
                    }
                    else {
                        throwError('The "' + string_range_attr_name +
                                   '" is required or must follow format "min,max".');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking minimum of numeric value.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-numeric-min="3"
            //        ps-validator-numeric-min-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-numeric-min-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var numeric_min_attr_name = 'ps-validator-numeric-min';
                    var min = element.attributes[numeric_min_attr_name].value;
                    if (numericRegexp.test(min)) {
                        return numericRegexp.test(value) && parseFloat(value) >= parseFloat(min);
                    }
                    else {
                        throwError('The "' + numeric_min_attr_name + '" is required or must be numeric.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking maximum of numeric value.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-numeric-max="3"
            //        ps-validator-numeric-max-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-numeric-max-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var numeric_max_attr_name = 'ps-validator-numeric-max';
                    var max = element.attributes[numeric_max_attr_name].value;
                    if (numericRegexp.test(max)) {
                        return numericRegexp.test(value) && parseFloat(value) <= parseFloat(max);
                    }
                    else {
                        throwError('The "' + numeric_max_attr_name + '" is required or must be numeric.');
                    }
                }
                else {
                    return true;
                }
            }
        },
        {
            // For checking range of numeric value.
            // Example:
            // <input ng-model="Model.Name"
            //        ps-validator-numeric-range="3.95,5.78"
            //        ps-validator-numeric-range-message="Put the error message here, can support AngularJS scope variable such as {{ErrMsg}}"></input>
            Name: 'ps-validator-numeric-range-message',
            Function: function (element) {
                var value = getScopeValue(element);
                if (!!value) {
                    var numeric_range_attr_name = 'ps-validator-numeric-range';
                    var range = element.attributes[numeric_range_attr_name].value;
                    if (numericRangeRegexp.test(range)) {
                        var rangeParts = range.split(',');
                        return parseFloat(value) >= parseFloat(rangeParts[0]) && parseFloat(value) <= parseFloat(rangeParts[1]);
                    }
                    else {
                        throwError('The "' + numeric_range_attr_name +
                                   '" is required or must follow format "min,max".');
                    }
                }
                else {
                    return true;
                }
            }
        }
    ];


    // Initialize.
    validators = $element;
    if (validators.length == 1) {
        isIntializing = true;
        intialize();

        // Use jquery to add click listener on submit.
        validators.find(submit_selector).click(function () {
            if (!GetValidStatus()) {
                updateErrorMessageBlockView();
            }
        });
    }
    else {
        throwError('The "ps-validator" attribute could not be found.');
    }


    /* All private functions */
    function intialize() {
        for (var i = 0; i < validators.length; i++) {
            // Initialize custom error message block.
            var custom_error_message_elements = validators[i].querySelectorAll('[' + error_message_placeHolder_attr_name + ']');
            for (var j = 0; j < custom_error_message_elements.length; j++) {
                var validator_id = custom_error_message_elements[j].attributes[error_message_placeHolder_attr_name].value;
                var validator_elements = validators[i].querySelectorAll('[' + control_id_attr_name + '="' + validator_id + '"]');

                // Checks validator elements.
                if (validator_elements.length < 1) {
                    logWarning('[' + error_message_placeHolder_attr_name + ']: "' + control_id_attr_name + ' = ' + validator_id + '" could not be found.');
                }
                else {
                    // Adds to collection of custom error messages.
                    if (customErrorMessages.indexOf(validator_id) < 0) {
                        customErrorMessages.push({ Id: validator_id, CustomErrorElement: custom_error_message_elements[j] });
                    }
                }
            }


            for (var j = 0; j < features.length; j++) {
                // Scans all features of all elements.
                var validator_elements = validators[i].querySelectorAll('[' + features[j].Name + ']');
                for (var k = 0; k < validator_elements.length; k++) {
                    // Initialize error messages.
                    var errorMessageObject;
                    var index = errorMessages.map(function (m) { return m.Model; })
                                             .indexOf(validator_elements[k].attributes[model_attr_name].value)
                    if (index > -1) {
                        var subIndex = errorMessages[index].MessageObjects.map(function (m) { return m.FeatureName; })
                                                                          .indexOf(features[j].Name);
                        if ((subIndex > -1)) {
                            errorMessages[index].MessageObjects[subIndex] = {
                                FeatureName: features[j].Name,
                                Message: ''
                            };
                        }
                        else {
                            errorMessages[index].MessageObjects.push({
                                FeatureName: features[j].Name,
                                Message: ''
                            });
                        }
                    }
                    else {
                        errorMessageObject = {};
                        errorMessageObject.Model = validator_elements[k].attributes[model_attr_name].value;
                        errorMessageObject.MessageObjects = [];
                        errorMessageObject.MessageObjects.push({ FeatureName: features[j].Name, Message: '' });
                        errorMessages.push(errorMessageObject);
                    }

                    // Creates error message block to element.
                    addErrorMessageBlockToElement(
                        validator_elements[k],
                        validator_elements[k].attributes[model_attr_name].value
                    );

                    // Initialize validators.
                    genValidator(features[j], validator_elements[k]);
                }
            }
        }
    }

    function singleValidation(element, regexp) {
        var value = getScopeValue(element);
        if (!!value) {
            return regexp.test(value);
        }
        else {
            return true;
        }
    }

    function genValidator(feature, validatorElement) {
        if (typeof (feature.Function) === 'function') {
            $scope.$watch(validatorElement.attributes[model_attr_name].value, function () {
                // Validates value by using feature.
                if (!feature.Function(validatorElement)) { // Invalid.
                    setErrorMessageBlock(
                        validatorElement,
                        feature.Name,
                        validatorElement.attributes[feature.Name].value
                    );
                }
                else { // Valid.
                    setErrorMessageBlock(validatorElement, feature.Name, '');
                }

                if (!isIntializing) {
                    // Updates error message block view on specified model.
                    updateErrorMessageBlockView(validatorElement.attributes[model_attr_name].value);
                }
                else {
                    // Waits for initialization has finished.
                    $timeout(function () {
                        isIntializing = false;
                    });
                }
            });
        }
        else {
            throwError('The featureFunction is not predicate function.');
        }
    }

    function createErrorMessageBlock(element, modelName) {
        // Checks duplicate error message block.
        if (angular.element(element).next().attr(error_message_block_attr_name) != modelName) {
            // Creates error message block.
            var errBlock = document.createElement('div');
            errBlock.setAttribute('class', error_message_block_css);
            errBlock.setAttribute(error_message_block_attr_name, modelName);

            // Adds error message block next to input element.
            angular.element(element).after(errBlock);
        }
    }

    function addErrorMessageBlockToElement(element, modelName) {
        // Puts the control into the wrapper.
        angular.element(element).wrap('<div />');

        // Checks custom error message block.
        if (!!element.attributes[control_id_attr_name])  { // Uses custom error message block.
            var index = customErrorMessages.map(function (m) { return m.Id; })
                                           .indexOf(element.attributes[control_id_attr_name].value);
            if (index > -1) {
                customErrorMessages[index].CustomErrorElement.setAttribute('class', error_message_block_css);
                customErrorMessages[index].CustomErrorElement.setAttribute(error_message_block_attr_name, modelName);
            }
            else { // Creates error message block.
                createErrorMessageBlock(element, modelName);
            }
        }
        else { // Creates error message block.
            createErrorMessageBlock(element, modelName);
        }
    }

    function setErrorMessageBlock(element, featureName, errorMessage) {
        var index = errorMessages.map(function (m) { return m.Model; })
                                 .indexOf(element.attributes[model_attr_name].value);
        if (index > -1) {
            var subIndex = errorMessages[index].MessageObjects.map(function (m) { return m.FeatureName; })
                                                              .indexOf(featureName);
            if (subIndex > -1) {
                errorMessages[index].MessageObjects[subIndex].Message = getErrorMessage(errorMessage);
            }
            else {
                if (featureName == custom_feature_name) {
                    errorMessages[index].MessageObjects.push({
                        FeatureName: featureName,
                        Message: getErrorMessage(errorMessage)
                    });
                }
            }
        }
    }

    function updateErrorMessageBlockView(modelName) {
        var messages = [];
        if (typeof (modelName) == 'undefined') {
            messages = errorMessages;
        }
        else {
            var index = errorMessages.map(function (m) { return m.Model; }).indexOf(modelName);
            if (index > -1) {
                messages.push(errorMessages[index]);
            }
        }

        for (var i = 0; i < messages.length; i++) {
            var error_message_element = validators[0].querySelector('[' + error_message_block_attr_name +
                                                                    '="' + messages[i].Model + '"]');
            if (!!error_message_element) {
                var error_message = '';
                for (var j = 0; j < messages[i].MessageObjects.length; j++) {
                    if (messages[i].MessageObjects[j].Message != '') {
                        error_message += messages[i].MessageObjects[j].Message;
                        if (j < messages[i].MessageObjects.length - 1) {
                            error_message += '<br/>';
                        }
                    }
                }

                angular.element(error_message_element).html(error_message);

                if (error_message == '') {
                    // Hides element when no error message.
                    showHideErrorMessageBlock(error_message_element, false);
                }
                else {
                    // Shows element when has error message.
                    showHideErrorMessageBlock(error_message_element, true);
                }
            }
        }
    }

    function visibleErrorMessageBlock(validatorElement, isVisible, errorMessageElement) {
        var validatorAngularElement = angular.element(validatorElement);
        var errorMessageAngularElement = (typeof (errorMessageElement) === 'object')
                                         ? angular.element(errorMessageElement)
                                         : null;

        if (isVisible) {
            // Adds css error to parent.
            validatorAngularElement.parent().addClass(error_container_css);

            // Shows error message block.
            if (!!errorMessageAngularElement) {
                errorMessageAngularElement.css('display', '');
            }
            else {
                validatorAngularElement.css('display', '');
            }
        }
        else {
            // Removes css error from parent.
            validatorAngularElement.parent().removeClass(error_container_css);

            // Hide error message block.
            if (!!errorMessageAngularElement) {
                errorMessageAngularElement.css('display', 'none');
            }
            else {
                validatorAngularElement.css('display', 'none');
            }
        }
    }

    function showHideErrorMessageBlock(element, isShow) {
        // Checks custom error message block.
        if (!!element.attributes[error_message_placeHolder_attr_name]) {
            var validator_elements =
                $element.find('[' + control_id_attr_name + '="' + element.attributes[error_message_placeHolder_attr_name].value + '"]');
            if (validator_elements.length > 0) {
                for (var i = 0; i < validator_elements.length; i++) {
                    visibleErrorMessageBlock(validator_elements[i], isShow, element);
                }
            }
            else {
                visibleErrorMessageBlock(element, isShow);
            }
        }
        else {
            visibleErrorMessageBlock(element, isShow);
        }
    }

    function getErrorMessage(errorMessage) {
        return errorMessage;
    }

    function getScopeValue(element) {
        try {
            var value = eval('$scope.' + element.attributes[model_attr_name].value);
            return (typeof (value) == 'undefined') ? null : value;
        } catch (e) {
            logWarning('The ngModel: "' + element.attributes[model_attr_name].value + '" was undefined.');
            return null;
        }
    }

    function getElementsByValidatorControlId(validatorControlId) {
        return validators.find('[' + control_id_attr_name + ' = ' + validatorControlId + ']');
    }

    function GetValidStatus() {
        var result = true;
        for (var i = 0; i < errorMessages.length; i++) {
            for (var j = 0; j < errorMessages[i].MessageObjects.length; j++) {
                if (!!errorMessages[i].MessageObjects[j].Message) {
                    return false;
                }
            }
        }

        return result;
    }


    /* All public functions */
    this.AddErrorMessage = function (validatorControlId, errorMessage) {
        var elements = getElementsByValidatorControlId(validatorControlId);
        if (elements.length > 0) {
            // Sets message all controls by control id.
            for (var i = 0; i < elements.length; i++) {
                setErrorMessageBlock(elements[i], custom_feature_name, errorMessage);

                // Updates error message block view on specified model.
                updateErrorMessageBlockView(elements[i].attributes[model_attr_name].value);
            }
        }
        else {
            throwError('The validatorControlId: "' + validatorControlId + '" could not be found.');
        }
    };

    this.ClearErrorMessage = function (validatorControlId) {
        var elements = getElementsByValidatorControlId(validatorControlId);
        if (elements.length > 0) {
            // Sets message all controls by control id.
            for (var i = 0; i < elements.length; i++) {
                setErrorMessageBlock(elements[i], custom_feature_name, '');

                // Updates error message block view on specified model.
                updateErrorMessageBlockView(elements[i].attributes[model_attr_name].value);
            }
        }
        else {
            throwError('The validatorControlId: "' + validatorControlId + '" could not be found.');
        }
    };

    this.GetValueByValidatorControlId = function (validatorControlId) {
        var result = [];
        var elements = getElementsByValidatorControlId(validatorControlId);
        for (var i = 0; i < elements.length; i++) {
            result.push(elements[i].value);
        }

        return result;
    }

    this.AddPredicateFunction = function (name, predicate) {
        var index = predicateFunctions.map(function (p) { return p.Name; }).indexOf(name);
        if (index > -1) {
            throwError('The name: "' + name + '" is duplicate.');
        }
        else {
            predicateFunctions.push({ Name: name, Function: predicate });
        }
    };

    this.IsValid = function () {
        return GetValidStatus();
    };
}