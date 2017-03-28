var app = angular.module('entityApp', ['ngAnimate', 'ui.bootstrap', 'ngMessages', 'psValidator']);

app.service('alertManager', $alertManager);
app.controller('AlertsController', $alertController);


app.controller('entityController', ['$scope', '$http', '$modal', '$element', 'alertManager', function ($scope, $http, $modal, $element, alertManager) {

    // validation message
    $scope.RequiredErrMsg = 'Required';
    $scope.NumberErrMsg = 'Enter only digits';
    $scope.StringRange4ErrMsg = "4 digits";
    $scope.NumberRange1_12ErrMsg = '1-12';
    $scope.NumberRange1_31ErrMsg = '1-31';
    

    $scope.entity = entity;

    // for new entity we do not know id
    if ($scope.entity.EntityId == 0) {
        $scope.entity = {
            "EntityId": null,
            "OriginalEntityId": null,
            "RegulationId": 1,
            "SubjectTypeId": null,
            "StatusId": null,
            "RemarkId": null,
            "ListTypeId": null,
            "ListArchiveId": null,
            "NameAliases": [
            ],
            "Addresses": [
            ],
            "Identifications": [
            ],
            "ContactInfo": [
            ],
            "Births": [
            ],
            "Banks": [
            ],
            "Citizenships": [
            ]
        };
    };
    // generate the validation name for dynamic control.
    $scope.GlobalRunning = 0;
    $scope.GetNextValidateName = function () {
        $scope.GlobalRunning++;
        return 'validate' + $scope.GlobalRunning;
    };

    // add ValidationName property to NameAliases object.
    $scope.NameAliasesLength =0;
    $scope.$watch('entity.NameAliases.length', function () {
        if($scope.entity.NameAliases.length > $scope.NameAliasesLength && $scope.entity.NameAliases.length>0){
            if ($scope.NameAliasesLength == 0) {
                for(i=0; i < $scope.entity.NameAliases.length; i++){
                    $scope.entity.NameAliases[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.NameAliases[$scope.entity.NameAliases.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.NameAliasesLength = $scope.entity.NameAliases.length;
    });

    // add ValidationName property to Citizenships object.
    $scope.CitizenshipsLength = 0;
    $scope.$watch('entity.Citizenships.length', function () {
        if ($scope.entity.Citizenships.length > $scope.CitizenshipsLength && $scope.entity.Citizenships.length > 0) {
            if ($scope.CitizenshipsLength == 0) {
                for (i = 0; i < $scope.entity.Citizenships.length; i++) {
                    $scope.entity.Citizenships[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.Citizenships[$scope.entity.Citizenships.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.CitizenshipsLength = $scope.entity.Citizenships.length;
    });

    // add ValidationName property to Addresses object.
    $scope.AddressesLength = 0;
    $scope.$watch('entity.Addresses.length', function () {
        if ($scope.entity.Addresses.length > $scope.AddressesLength && $scope.entity.Addresses.length > 0) {
            if ($scope.AddressesLength == 0) {
                for (i = 0; i < $scope.entity.Addresses.length; i++) {
                    $scope.entity.Addresses[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.Addresses[$scope.entity.Addresses.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.AddressesLength = $scope.entity.Addresses.length;
    });

    // add ValidationName property to Identifications object.
    $scope.IdentificationsLength = 0;
    $scope.$watch('entity.Identifications.length', function () {
        if ($scope.entity.Identifications.length > $scope.IdentificationsLength && $scope.entity.Identifications.length > 0) {
            if ($scope.IdentificationsLength == 0) {
                for (i = 0; i < $scope.entity.Identifications.length; i++) {
                    $scope.entity.Identifications[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.Identifications[$scope.entity.Identifications.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.IdentificationsLength = $scope.entity.Identifications.length;
    });

    // add ValidationName property to ContactInfo object.
    $scope.ContactInfoLength = 0;
    $scope.$watch('entity.ContactInfo.length', function () {
        if ($scope.entity.ContactInfo.length > $scope.ContactInfoLength && $scope.entity.ContactInfo.length > 0) {
            if ($scope.ContactInfoLength == 0) {
                for (i = 0; i < $scope.entity.ContactInfo.length; i++) {
                    $scope.entity.ContactInfo[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.ContactInfo[$scope.entity.ContactInfo.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.ContactInfoLength = $scope.entity.ContactInfo.length;
    });

    // add ValidationName property to Births object.
    $scope.BirthsLength = 0;
    $scope.$watch('entity.Births.length', function () {
        if ($scope.entity.Births.length > $scope.BirthsLength && $scope.entity.Births.length > 0) {
            if ($scope.BirthsLength == 0) {
                for (i = 0; i < $scope.entity.Births.length; i++) {
                    $scope.entity.Births[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.Births[$scope.entity.Births.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.BirthsLength = $scope.entity.Births.length;
    });

    // add ValidationName property to Banks object.
    $scope.BanksLength = 0;
    $scope.$watch('entity.Banks.length', function () {
        if ($scope.entity.Banks.length > $scope.BanksLength && $scope.entity.Banks.length > 0) {
            if ($scope.BanksLength == 0) {
                for (i = 0; i < $scope.entity.Banks.length; i++) {
                    $scope.entity.Banks[i].ValidationName = $scope.GetNextValidateName();
                }
            } else {
                $scope.entity.Banks[$scope.entity.Banks.length - 1].ValidationName = $scope.GetNextValidateName();
            }
        }
        $scope.BanksLength = $scope.entity.Banks.length;
    });


    $scope.DatePickerSettings = {
        'format': 'yyyy-MM-dd',
        'opened': false,
        'min-date': new Date(1800, 5, 22),
        'max-date': new Date(2020, 5, 22)
    };

    //todo do this only when adding, not editing
    $scope.entity.ListType = { ListTypeId: listTypeId };

    $scope.myscope = null;

    var modalInstance = null;

    $scope.regulation = { "PublicationDate": "43efgqergfqerG" };

    $scope.RegulationsOptions = {
        availableOptions: regulations
    };

    $scope.SubjectTypesOptions = {
        availableOptions: subjectTypes
    };

    $scope.StatusesOptions = {
        availableOptions: statuses
    };

    $scope.IdentificationTypesOptions = {
        availableOptions: identificationTypes
    };

    $scope.GetIdentificationTypeName = function (identificationTypeId) {

        if (typeof (identificationTypeId) != "undefined" && identificationTypeId != "" && $scope.IdentificationTypesOptions.availableOptions != null) {
            for (var i = 0; i < $scope.IdentificationTypesOptions.availableOptions.length; i++) {
                if ($scope.IdentificationTypesOptions.availableOptions[i].IdentificationTypeId == identificationTypeId) {
                    return $scope.IdentificationTypesOptions.availableOptions[i].Name;
                }
            }
        }
        return "";
    };

    $scope.CountriesOptions = {
        availableOptions: countriesEnum
    };

    $scope.GetCountryNiceName = function (iso3) {

        if (typeof (iso3) != "undefined" && iso3 != "" && $scope.CountriesOptions.availableOptions != null) {
            for (var i = 0; i < $scope.CountriesOptions.availableOptions.length; i++) {
                if ($scope.CountriesOptions.availableOptions[i].Iso3 == iso3) {
                    return $scope.CountriesOptions.availableOptions[i].NiceName;
                }
            }
        }
        return "";
    };

    $scope.ContactInfoTypesOptions = {
        availableOptions: contactInfoTypes
    };

    $scope.GetContactInfoTypeName = function (contactInfoTypeId) {

        if (typeof (contactInfoTypeId) != "undefined" && contactInfoTypeId != "" && $scope.ContactInfoTypesOptions.availableOptions != null) {
            for (var i = 0; i < $scope.ContactInfoTypesOptions.availableOptions.length; i++) {
                if ($scope.ContactInfoTypesOptions.availableOptions[i].ContactInfoTypeId == contactInfoTypeId) {
                    return $scope.ContactInfoTypesOptions.availableOptions[i].Name;
                }
            }
        }
        return "";
    };

    $scope.GendersOptions = {
        availableOptions: genders
    };

    $scope.LanguagesOptions = {
        availableOptions: languages
    };

    $scope.AddNameAlias = function () {
        $scope.entity.NameAliases.push({
            "NameAliasId": null,
            "OriginalNameAliasId": null,
            "EntityId": $scope.entity.EntityId,
            "RegulationId": null,
            "LastName": null,
            "FirstName": null,
            "MiddleName": null,
            "WholeName": null,
            "PrefixName": null,
            "Gender": null,
            "Title": null,
            "Language": null,
            "RemarkId": null,
            "Quality": null,
            "Function": null,
        });
    };

    $scope.AddAddress = function () {
        $scope.entity.Addresses.push({
            "AddressId": null,
            "OriginalAddressId": null,
            "EntityId": null,
            "RegulationId": null,
            "Number": null,
            "Street": null,
            "Zipcode": null,
            "City": null,
            "CountryId": null,
            "RemarkId": null,
        });
    };

    $scope.AddIdentification = function () {
        $scope.entity.Identifications.push({
            "IdentificationId": null,
            "OriginalIdentificationId": null,
            "EntityId": null,
            "RegulationId": null,
            "IdentificationTypeId": null,
            "DocumentNumber": null,
            "IssueCity": null,
            "IssueCountry": null,
            "IssueDate": null,
            "ExpiryDate": null,
            "RemarkId": null
        });
    };

    $scope.AddContactInfo = function () {
        $scope.entity.ContactInfo.push({
            "ContactInfoId": null,
            "OriginalContactInfoId": null,
            "EntityId": $scope.entity.EntityId,
            "RegulationId": null,
            "ContactInfoTypeId": null,
            "Value": null,
            "RemarkId": null,
        });
    };

    $scope.AddBirth = function () {
        $scope.entity.Births.push({
            "BirthId": null,
            "OriginalBirthId": null,
            "EntityId": null,
            "RegulationId": null,
            "Date": null,
            "Place": null,
            "CountryId": null,
            "RemarkId": null,
        });
    };

    $scope.AddCitizenship = function () {
        $scope.entity.Citizenships.push({});
    };

    $scope.AddBank = function () {
        $scope.entity.Banks.push({
            "BankId": null,
            "EntityId": null,
            "BankName": null,
            "Swift": null,
            "AccountHolderName": null,
            "AccountNumber": null,
            "Iban": null,
            "RemarkId": null,
        });
    };

    $scope.toggleSectionElement = function ($event) {
        if ($($event.target).hasClass("panel-heading")) {
            rootNode = $($event.target).parent();
        } else {
            rootNode = $($event.target).parents(":eq(1)");
        }

        elemToToggle = $(rootNode).children(".panel-body");
        panelHeading = $(rootNode).find(".panel-heading").first();
        chevron = $(panelHeading).find("span").first();


        if ($(elemToToggle).is(":visible")) {
            $(elemToToggle).slideUp(100);
            $(chevron).removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            $(panelHeading).children(".collapsedMode").each(function (i) { $(this).fadeIn(100); });
        } else {
            $(elemToToggle).slideDown(100);
            $(chevron).removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            $(panelHeading).children(".collapsedMode").each(function (i) { $(this).fadeOut(100); });
        }
    };

    $scope.toggleSection = function ($event) {
        if ($($event.target).hasClass("panel-group-header")) {
            rootNode = $($event.target).parent();
        } else {
            rootNode = $($event.target).parents(":eq(3)");
        }

        elemToToggle = $(rootNode).children(".panel");
        panelHeading = $(rootNode).find(".panel-group-header").first();
        chevron = $(panelHeading).find("span").first();


        if ($(elemToToggle).is(":visible")) {
            $(elemToToggle).slideUp(100);
            $(chevron).removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            $(panelHeading).find(".collapsedMode").each(function (i) { $(this).fadeIn(100); });
        } else {
            $(elemToToggle).slideDown(100);
            $(chevron).removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            $(panelHeading).find(".collapsedMode").each(function (i) { $(this).fadeOut(100); });
        }
    };

    $scope.removeNameAlias = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.NameAliases[index].NameAliasId) {
                //remove from list only
                $scope.entity.NameAliases.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/NameAliases/' + $scope.entity.NameAliases[index].NameAliasId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.NameAliases.splice(index, 1);

                    alertManager.addMessage("success", "Name alias has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing NameAlias.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeCitizenship = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.Citizenships[index].CitizenshipId) {
                //remove from list only
                $scope.entity.Citizenships.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Citizenships/' + $scope.entity.Citizenships[index].CitizenshipId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.Citizenships.splice(index, 1);

                    alertManager.addMessage("success", "Citizenship has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing citizenship");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeAddress = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.Addresses[index].AddressId) {
                //remove from list only
                $scope.entity.Addresses.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Addresses/' + $scope.entity.Addresses[index].AddressId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.Addresses.splice(index, 1);

                    alertManager.addMessage("success", "Address has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing address.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeIdentification = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.Identifications[index].IdentificationId) {
                //remove from list only
                $scope.entity.Identifications.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Identifications/' + $scope.entity.Identifications[index].IdentificationId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.Identifications.splice(index, 1);

                    alertManager.addMessage("success", "Identification has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing Identification.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeContactInfo = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.ContactInfo[index].ContactInfoId) {
                //remove from list only
                $scope.entity.ContactInfo.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Contactinfos/' + $scope.entity.ContactInfo[index].ContactInfoId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.ContactInfo.splice(index, 1);

                    alertManager.addMessage("success", "Contact info has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing contactInfo.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeBirth = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.Births[index].BirthId) {
                //remove from list only
                $scope.entity.Births.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Births/' + $scope.entity.Births[index].BirthId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.Births.splice(index, 1);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing birth.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.removeBank = function (index) {
        if (confirm("Remove this element?")) {
            if (!$scope.entity.Banks[index].BankId) {
                //remove from list only
                $scope.entity.Banks.splice(index, 1);
            } else {
                // remove from DB. 
                // if success, remove from list
                // if not, show message

                $http({
                    method: 'DELETE',
                    url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Banks/' + $scope.entity.Banks[index].BankId,
                    // TODO check if should be JSON type
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
                })
                .then(function (data) {
                    $scope.entity.Banks.splice(index, 1);

                    alertManager.addMessage("success", "Bank has been removed from database.", 3000);
                })
                .catch(function (data) {
                    alertManager.addMessage("danger", "Error - removing bank.");
                })
                .finally(function () {
                });
            }
        }
    };

    $scope.addRegulationOption = function (result) {
        $scope.RegulationsOptions.availableOptions.push(result);
        // TODO maybe set current regid to recently added one: entity.RegulationId = result.id;
    };

    $scope.openModal = function () {
        modalInstance = $modal.open({
            templateUrl: "addRegulationTemplate.html",
            controller: 'modalRegulationCtrl',
            size: "lg",
            resolve: {
                newRegulation: function () {
                    return $scope.newRegulation;
                }
            }
        });

        modalInstance.result.then(function (result) {
            $scope.addRegulationOption(result);
        }, function () {
            console.log('Modal dismissed at: ' + new Date());
        });
    };

    $scope.processEntityForm = function () {
        // validation
        if ($scope.PsValidators['validateEntity'].IsValid()) {

            if ($scope.entity.EntityId == null) {
                $scope.processAddEntity();
            } else {
                $scope.processEditEntity();
            }
        }
    };

    $scope.processAddEntity = function () {

        $http({
            method: 'POST',
                url: '/api/SanctionLists/' + listTypeId + '/entities',
                data: $.param($scope.entity),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
            .then(function (response) {
            $scope.entity.EntityId = response.data.EntityId;
            $scope.entity.OriginalEntityId = response.data.OriginalEntityId;

            alertManager.addMessage("success", "New entity has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding entity.");
        })
        .finally(function () {
        });
    };

    $scope.processEditEntity = function () {

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId,
            data: $.param($scope.entity),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            alertManager.addMessage("success", "Entity has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating entity.");
        })
        .finally(function () {
        });

    };

    $scope.processNameAliasForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.NameAliases[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.NameAliases[index].NameAliasId) {
            $scope.processAddNameAlias(index)
        } else {
            $scope.processUpdateNameAlias(index)
        }
    };

    $scope.processAddNameAlias = function (index) {

        var validationName = $scope.entity.NameAliases[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/NameAliases',
            data: $.param($scope.entity.NameAliases[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.NameAliases[index] = response.data;

            // set the old validation name to NameAliases object.
            $scope.entity.NameAliases[index].ValidationName = validationName;

            alertManager.addMessage("success", "New name alias has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding NameAlias.");
        })
        .finally(function () {
        });

    };

    $scope.processUpdateNameAlias = function (index) {

        var validationName = $scope.entity.NameAliases[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/NameAliases/' + $scope.entity.NameAliases[index].NameAliasId,
            data: $.param($scope.entity.NameAliases[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.NameAliases[index] = response.data;

            // set the old validation name to NameAliases object.
            $scope.entity.NameAliases[index].ValidationName = validationName;

            alertManager.addMessage("success", "NameAlias has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating NameAlias.");
        })
        .finally(function () {
        });

    };

    $scope.processCitizenshipForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.Citizenships[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.Citizenships[index].CitizenshipId) {
            $scope.processAddCitizenship(index);
        } else {
            $scope.processEditCitizenship(index);
        }
    };

    $scope.processAddCitizenship = function (index) {

        var validationName = $scope.entity.NameAliases[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Citizenships',
            data: $.param($scope.entity.Citizenships[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Citizenships[index] = response.data;

            // set the old validation name to Citizenships object.
            $scope.entity.Citizenships[index].ValidationName = validationName;

            alertManager.addMessage("success", "New citizenship has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding citizenship.");
        })
        .finally(function () {
        });

    };

    $scope.processEditCitizenship = function (index) {

        var validationName = $scope.entity.NameAliases[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Citizenships/' + $scope.entity.Citizenships[index].OriginalCitizenshipId,
            data: $.param($scope.entity.Citizenships[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Citizenships[index] = response.data;

            // set the old validation name to Citizenships object.
            $scope.entity.Citizenships[index].ValidationName = validationName;

            alertManager.addMessage("success", "Citizenship has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating citizenship.");
        })
        .finally(function () {
        });

    };

    $scope.processAddressForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.Addresses[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.Addresses[index].AddressId) {
            $scope.processAddAddress(index);
        } else {
            $scope.processEditAddress(index);
        }
    };

    $scope.processAddAddress = function (index) {

        var validationName = $scope.entity.Addresses[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Addresses',
            data: $.param($scope.entity.Addresses[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Addresses[index] = response.data;

            // set the old validation name to Addresses object.
            $scope.entity.Addresses[index].ValidationName = validationName;

            alertManager.addMessage("success", "New address has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding address.");
        })
        .finally(function () {
        });

    };

    $scope.processEditAddress = function (index) {

        var validationName = $scope.entity.Addresses[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Addresses/' + $scope.entity.Addresses[index].AddressId,
            data: $.param($scope.entity.Addresses[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Addresses[index] = response.data;

            // set the old validation name to Addresses object.
            $scope.entity.Addresses[index].ValidationName = validationName;

            alertManager.addMessage("success", "Address has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating address.");
        })
        .finally(function () {
        });

    };

    $scope.processIdentificationForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.Identifications[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.Identifications[index].IdentificationId) {
            $scope.processAddIdentification(index);
        } else {
            $scope.processEditIdentification(index);
        }
    };

    $scope.processAddIdentification = function (index) {

        var validationName = $scope.entity.Identifications[index].ValidationName;
        var parseIdentification = $scope.entity.Identifications[index];
        // have to convert datetime format for controller.
        if (!!$scope.entity.Identifications[index].IssueDate) {
            parseIdentification.IssueDate = $scope.entity.Identifications[index].IssueDate.toISOString();
        }
        if (!!$scope.entity.Identifications[index].ExpiryDate) {
            parseIdentification.ExpiryDate = $scope.entity.Identifications[index].ExpiryDate.toISOString();
        }

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Identifications',
            data: $.param(parseIdentification),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Identifications[index] = response.data;

            // set the old validation name to Identifications object.
            $scope.entity.Identifications[index].ValidationName = validationName;

            alertManager.addMessage("success", "New identification has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding identification.");
        })
        .finally(function () {
        });
    };

    function TryToIsoString(date) {
        try{
            // the date value that come from controller already in Iso format and cannot convert to call toISOString.
            // but date value that come from datepicker should convert to Iso string.
            // Iso format : 2015-09-29T00:00:00 , javascript : Sat Sep 19 2015 00:00:00 GMT+0700 (SE Asia Standard Time)
            return date.toISOString();
        }
        catch(e){
            return date;
        }
    };

    $scope.processEditIdentification = function (index) {

        var validationName = $scope.entity.Identifications[index].ValidationName;
        var parseIdentification = $scope.entity.Identifications[index];
        // have to convert datetime format for controller.
        if (!!$scope.entity.Identifications[index].IssueDate) {
            parseIdentification.IssueDate = TryToIsoString($scope.entity.Identifications[index].IssueDate);
        }
        if (!!$scope.entity.Identifications[index].ExpiryDate) {
            parseIdentification.ExpiryDate = TryToIsoString($scope.entity.Identifications[index].ExpiryDate);
        }

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Identifications/' + $scope.entity.Identifications[index].IdentificationId,
            data: $.param(parseIdentification),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Identifications[index] = response.data;

            // set the old validation name to Identifications object.
            $scope.entity.Identifications[index].ValidationName = validationName;

            alertManager.addMessage("success", "Identification has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating identification.");
        })
        .finally(function () {
        });

    };

    $scope.processContactInfosForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.ContactInfo[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.ContactInfo[index].ContactInfoId) {
            $scope.processAddContactInfo(index)
        } else {
            $scope.processUpdateContactInfo(index)
        }
    };

    $scope.processAddContactInfo = function (index) {

        var validationName = $scope.entity.ContactInfo[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Contactinfos',
            data: $.param($scope.entity.ContactInfo[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.ContactInfo[index] = response.data;

            // set the old validation name to ContactInfo object.
            $scope.entity.ContactInfo[index].ValidationName = validationName;

            alertManager.addMessage("success", "New contact info has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding contactInfo.");
        })
        .finally(function () {
        });

    };

    $scope.processUpdateContactInfo = function (index) {

        var validationName = $scope.entity.ContactInfo[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Contactinfos/' + $scope.entity.ContactInfo[index].ContactInfoId,
            data: $.param($scope.entity.ContactInfo[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.ContactInfo[index] = response.data;

            // set the old validation name to ContactInfo object.
            $scope.entity.ContactInfo[index].ValidationName = validationName;

            alertManager.addMessage("success", "ContactInfo changes are saved.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating contactInfo.");
        })
        .finally(function () {
        });
    };

    $scope.processBirthsForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.Births[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.Births[index].BirthId) {
            $scope.processAddBirth(index)
        } else {
            $scope.processEditBirth(index)
        }
    };

    $scope.processAddBirth = function (index) {

        var validationName = $scope.entity.Births[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Births',
            data: $.param($scope.entity.Births[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Births[index] = response.data;

            // set the old validation name to Births object.
            $scope.entity.Births[index].ValidationName = validationName;

            alertManager.addMessage("success", "New birth has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding Birth.");
        })
        .finally(function () {
        });
    };

    $scope.processEditBirth = function (index) {

        var validationName = $scope.entity.Births[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Births/' + $scope.entity.Births[index].BirthId,
            data: $.param($scope.entity.Births[index]),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Births[index] = response.data;

            // set the old validation name to Births object.
            $scope.entity.Births[index].ValidationName = validationName;

            alertManager.addMessage("success", "Birth has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating Birth.");
        })
        .finally(function () {
        });
    };

    $scope.processBanksForm = function (index) {

        // validate from if got error will do not thing.
        if (!$scope.PsValidators[$scope.entity.Banks[index].ValidationName].IsValid()) {
            return;
        }

        if (!$scope.entity.Banks[index].BankId) {
            $scope.processAddBank(index)
        } else {
            $scope.processEditBank(index)
        }
    };

    $scope.processAddBank = function (index) {

        var validationName = $scope.entity.Banks[index].ValidationName;

        $http({
            method: 'POST',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Banks',
            data: $.param($scope.entity.Banks[index]),
            // TODO check if should be JSON type
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Banks[index] = response.data;

            // set the old validation name to Banks object.
            $scope.entity.Banks[index].ValidationName = validationName;

            alertManager.addMessage("success", "New bank has been added.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding Bank.");
        })
        .finally(function () {
        });
    };

    $scope.processEditBank = function (index) {

        var validationName = $scope.entity.Banks[index].ValidationName;

        $http({
            method: 'PUT',
            url: '/api/SanctionLists/' + listTypeId + '/entities/' + $scope.entity.EntityId + '/Banks/' + $scope.entity.Banks[index].BankId,
            data: $.param($scope.entity.Banks[index]),
            // TODO check if should be JSON type
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $scope.entity.Banks[index] = response.data;

            // set the old validation name to Banks object.
            $scope.entity.Banks[index].ValidationName = validationName;

            alertManager.addMessage("success", "Bank has been updated.", 3000);
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - updating Bank.");
        })
        .finally(function () {
        });
    };

    $scope.openDatePicker = function ($event, section, index, datepickerId) {
        $event.preventDefault();
        $event.stopPropagation();
        if ($scope.entity[section][index][datepickerId] == null) {
            $scope.entity[section][index][datepickerId] = {
            };
            $scope.entity[section][index][datepickerId].opened = true;
        } else {
            $scope.entity[section][index][datepickerId].opened = !$scope.entity[section][index][datepickerId].opened;
        }
    };

}]);


app.controller('modalRegulationCtrl', function ($scope, $http, $modalInstance, newRegulation) {

    // validation message
    $scope.RequiredErrMsg = 'Required';

    $scope.newRegulation = {};
    $scope.newRegulation.PublicationDate = ''; // declare for validate. (cannot validate undefine field).

    $scope.newRegulation.ListType = { ListTypeId: listTypeId };

    $scope.newRegulationDate = {
        'format': 'yyyy-MM-dd',
        'opened': false,
        'min-date': new Date(1800, 5, 22),
        'max-date': new Date(2020, 5, 22)
    };

    $scope.newPublicationDate = {
        'format': 'yyyy-MM-dd',
        'opened': false,
        'min-date': new Date(1800, 5, 22),
        'max-date': new Date(2020, 5, 22)
    };


    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.open = function ($event, index) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope[index].opened = !$scope[index].opened;
    };

    $scope.processForm = function () {
        // call api to add new regulation
        
        // validate from if got error will do not thing.
        if (!$scope.PsValidators['validateRegulation'].IsValid()) {
            return;
        }

        var parseRegulation = $scope.newRegulation;
            // have to convert datetime format for controller.
        if (typeof ($scope.newRegulation.RegulationDate) != "undefined") {
            parseRegulation.RegulationDate = $scope.newRegulation.RegulationDate.toISOString();
        }
        if (typeof ($scope.newRegulation.PublicationDate) != "undefined") {
            parseRegulation.PublicationDate = $scope.newRegulation.PublicationDate.toISOString();
        }

        $http({
                method: 'POST',
                url: '/api/SanctionLists/' + listTypeId + '/regulations',
                data: $.param(parseRegulation),  // pass in data as strings
                headers: { 'Content-Type': 'application/x-www-form-urlencoded'
        }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(function (response) {
            $modalInstance.close({ "RegulationId": response.data.RegulationId, "PublicationTitle": response.data.PublicationTitle
        });
        })
        .catch(function (data) {
            alertManager.addMessage("danger", "Error - adding regulation.");
        })
        .finally(function () {
        });
        // TODO add error handling
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});


