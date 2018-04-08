angular.module('app.controlPanelControllers', [])

//Subject controllers ========
.controller('SubjectCtrl', SubjectCtrl)
.controller('SubjectCreateCtrl', SubjectCreateCtrl)
.controller('SubjectEditCtrl', SubjectEditCtrl)
.controller('SubjectDetailsCtrl', SubjectDetailsCtrl)

//Sponsor controllers ========
.controller('SponsorCtrl', SponsorCtrl)
.controller('SponsorCreateCtrl', SponsorCreateCtrl)
.controller('SponsorEditCtrl', SponsorEditCtrl)
.controller('SponsorDetailsCtrl', SponsorDetailsCtrl)

//PublishPeriod controllers ========
.controller('PublishPeriodCtrl', PublishPeriodCtrl)
.controller('PublishPeriodCreateCtrl', PublishPeriodCreateCtrl)
.controller('PublishPeriodEditCtrl', PublishPeriodEditCtrl)
.controller('PublishPeriodDetailsCtrl', PublishPeriodDetailsCtrl)

//Scope controllers ========
.controller('ScopeCtrl', ScopeCtrl)
.controller('ScopeCreateCtrl', ScopeCreateCtrl)
.controller('ScopeEditCtrl', ScopeEditCtrl)
.controller('ScopeDetailsCtrl', ScopeDetailsCtrl)

//Journal controllers ========
.controller('JournalCtrl', JournalCtrl)
.controller('JournalCreateCtrl', JournalCreateCtrl)
.controller('JournalEditCtrl', JournalEditCtrl)
.controller('JournalDetailsCtrl', JournalDetailsCtrl)

//JournalVersion controllers ========
.controller('JournalVersionCtrl', JournalVersionCtrl)
.controller('JournalVersionCreateCtrl', JournalVersionCreateCtrl)
.controller('JournalVersionEditCtrl', JournalVersionEditCtrl)
.controller('JournalVersionDetailsCtrl', JournalVersionDetailsCtrl)

//News controllers ========
.controller('NewsCtrl', NewsCtrl)
.controller('NewsCreateCtrl', NewsCreateCtrl)
.controller('NewsEditCtrl', NewsEditCtrl)
.controller('NewsDetailsCtrl', NewsDetailsCtrl)

//Conference controllers ========
.controller('ConferenceCtrl', ConferenceCtrl)
.controller('ConferenceCreateCtrl', ConferenceCreateCtrl)
.controller('ConferenceEditCtrl', ConferenceEditCtrl)
.controller('ConferenceDetailsCtrl', ConferenceDetailsCtrl)


//PublishedArticle controllers ========
.controller('PublishedArticleCtrl', PublishedArticleCtrl)
.controller('PublishedArticleCreateCtrl', PublishedArticleCreateCtrl)
.controller('PublishedArticleEditCtrl', PublishedArticleEditCtrl)
.controller('PublishedArticleDetailsCtrl', PublishedArticleDetailsCtrl)


//Subject functions ========
function SubjectCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SubjectCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'SubjectEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'SubjectDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.SubjectID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function SubjectCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SubjectEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SubjectDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//Sponsor functions ========
function SponsorCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SponsorCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'SponsorEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'SponsorDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.SponsorID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function SponsorCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SponsorEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SponsorDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//PublishPeriod functions ========
function PublishPeriodCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'PublishPeriodCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'PublishPeriodEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'PublishPeriodDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.PublishPeriodID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function PublishPeriodCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function PublishPeriodEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function PublishPeriodDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//Scope functions ========
function ScopeCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'ScopeCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'ScopeEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'ScopeDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.ScopeID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function ScopeCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function ScopeEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function ScopeDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//Journal functions ========
function JournalCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'JournalCreateCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'JournalEditCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'JournalDetailsCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.JournalID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function JournalCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function JournalEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function JournalDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//JournalVersion functions ========
function JournalVersionCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'JournalVersionCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'JournalVersionEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'JournalVersionDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.JournalVersionID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function JournalVersionCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    $scope.showDatePicker = function () {
        $('.form_datetime').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        }).on('changeDate', function (ev) {
            if (ev.date != null) {
                //resert datetime validation
                removeValidationError('IssueDateValidation');
            } else {
                //add required to date time
                setValidationError('IssueDateValidation', requiredFieldValidationMsg);
            }
        });
    }
    //form submit
    $scope.formSubmit = function ($event) {
        var valid = true;
        if ($('#IssueDate').val() == null || $('#IssueDate').val() == '') {
            valid = false;
            setValidationError('IssueDateValidation', requiredFieldValidationMsg);
        }else{
            removeValidationError('IssueDateValidation');
        }
        if (!valid || !$('#form').valid()) {
            $event.preventDefault();
        }
        else {
            showLoading();
        }
    }
}

function JournalVersionEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    $scope.showDatePicker = function () {
        $('.form_datetime').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        }).on('changeDate', function (ev) {
            if (ev.date != null) {
                //resert datetime validation
                removeValidationError('IssueDateValidation');
            } else {
                //add required to date time
                setValidationError('IssueDateValidation', requiredFieldValidationMsg);
            }
        });
    }
    //form submit
    $scope.formSubmit = function ($event) {
        var valid = true;
        if ($('#EditItem_IssueDate').val() == null || $('#EditItem_IssueDate').val() == '') {
            valid = false;
            setValidationError('IssueDateValidation', requiredFieldValidationMsg);
        } else {
            removeValidationError('IssueDateValidation');
        }
        if (!valid || !$('#form').valid()) {
            $event.preventDefault();
        }
        else {
            showLoading();
        }
    }
}

function JournalVersionDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//News functions ========
function NewsCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'NewsCreateCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        console.log(editActionUrl + '/' + id);
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'NewsEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'NewsDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.NewsID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function NewsCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

function NewsEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

function NewsDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

//Conference functions ========
function ConferenceCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'ConferenceCreateCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        console.log(editActionUrl + '/' + id);
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'ConferenceEditCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'ConferenceDetailsCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.ConferenceID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function ConferenceCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    $scope.showDatePicker = function () {
        $('.form_datetime').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });
    }
    //form submit
    $scope.formSubmit = function ($event) {
        var valid = true;
        if (!valid || !$('#form').valid()) {
            $event.preventDefault();
        }
        else {
            showLoading();
        }
    }
}

function ConferenceEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    //form submit
    $scope.formSubmit = function ($event) {
        var valid = true;
        if (!valid || !$('#form').valid()) {
            $event.preventDefault();
        }
        else {
            showLoading();
        }
    }
}

function ConferenceDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
}

//PublishedArticle functions ========
function PublishedArticleCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'PublishedArticleCreateCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'PublishedArticleEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'PublishedArticleDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.PublishedArticleID)
            });
            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });
            hideLoading();
        });
    }

}

function PublishedArticleCreateCtrl($scope, $uibModalInstance, $filter, commonPartialService, global, gridService, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
    commonPartialService.SubjectJournalJournalVersionCtrl().BindJournal($scope, $filter);
    commonPartialService.SubjectJournalJournalVersionCtrl().BindJournalVersion($scope, $filter);
    $scope.bindData = function () {
        $scope.model = {};
        //bind Article Types
        $scope.Articletypes = articleTypes;
        if (articleTypes.length > 0) {
            $scope.ArticleTypeID = articleTypes[0].Value.toString();
        }
        //bind scopes
        $scope.Scopes = scopes;
        //bind Authors
        $scope.Authors = authors;
    }
    $scope.showRecievedDatePicker = function () {
        $('#recievedDateDiv').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            endDate: new Date()
        });
    }
    $scope.showAcceptDatePicker = function () {
        $('#acceptDateDiv').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            endDate: new Date()
        });
    }
    $scope.showPublishDatePicker = function () {
        $('#publishDateDiv').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0,
            endDate: new Date()
        });
    }
    $scope.bindGirdData = function () {
        gridService.configGrid($scope, null, "PubArticleScopeGridOptions", "PubArticleScopeGridApi",
            pubArticleScope_GridColumnDefs, null, null, false, false, false, null);
        gridService.configGrid($scope, null, "PubArticleAuthorGridOptions","PubArticleAuthorGridApi",
            pubArticleAuthor_GridColumnDefs, null, null, false, false, false, null);
    }
    $scope.createPublishArticleScope = function () {
        var gridRowData = { };
        $scope.PubArticleScopeGridOptions.data.push(gridRowData);
        if ($scope.PubArticleScopeGridOptions.data.length > 15) {
            resizeGrid($scope, $scope.PubArticleScopeGridOptions.data.length);
        }
    }
    $scope.DeletePublishArticleScope = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.PubArticleScopeGridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                var itemIndex = $scope.PubArticleScopeGridOptions.data.indexOf(item);
                $scope.PubArticleScopeGridOptions.data.splice(itemIndex, 1);
            });
            hideLoading();
        });
    }
    $scope.createPubArticleAuthor = function () {
        //get Author Order
        var AuthorOrder = 1;
        var gridLen = $scope.PubArticleAuthorGridOptions.data.length;

        if (gridLen > 0) {
            AuthorOrder = $scope.PubArticleAuthorGridOptions.data[gridLen - 1].AuthorOrder + 1;
        }
        var gridRowData = { AuthorOrder: AuthorOrder };
        $scope.PubArticleAuthorGridOptions.data.push(gridRowData);
        if ($scope.PubArticleAuthorGridOptions.data.length > 15) {
            resizeGrid($scope, $scope.PubArticleAuthorGridOptions.data.length);
        }
    }
    $scope.DeletePubArticleAuthor = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.PubArticleAuthorGridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                var itemIndex = $scope.PubArticleAuthorGridOptions.data.indexOf(item);
                $scope.PubArticleAuthorGridOptions.data.splice(itemIndex, 1);
            });
            hideLoading();
        });
    }
    $scope.formSubmit = function ($event) {
        $event.preventDefault();
        var valid = true;
        if ($scope.model.JournalVersionID == null) {
            //set validation error
            setValidationError("JournalVersionID_Validation", requiredFieldValidationMsg);
            valid = false;
        } else {
            //reset validation error
            removeValidationError("JournalVersionID_Validation");
        }
        if ($scope.model.ArticleTypeID == null) {
            //set validation error
            setValidationError("ArticleTypeID_Validation", requiredFieldValidationMsg);
            valid = false;
        } else {
            //reset validation error
            removeValidationError("ArticleTypeID_Validation");
        }
        if ($('#RecievedDate').val() == null || $('#RecievedDate').val() == '') {
            //set validation error
            setValidationError("RecievedDate_Validation", requiredFieldValidationMsg);
            valid = false;
        } else {
            //reset validation error
            removeValidationError("RecievedDate_Validation");
        }
        if ($('#AcceptDate').val() == null || $('#AcceptDate').val() == '') {
            //set validation error
            setValidationError("AcceptDate_Validation", requiredFieldValidationMsg);
            valid = false;
        }else {
            //reset validation error
            removeValidationError("AcceptDate_Validation");
        }
        if ($('#PublishDate').val() == null || $('#PublishDate').val() == '') {
            //set validation error
            setValidationError("PublishDate_Validation", requiredFieldValidationMsg);
            valid = false;
        } else {
            //reset validation error
            removeValidationError("PublishDate_Validation");
        }
        //validate Scope Grid
        if ($scope.PubArticleScopeGridOptions.data.length > 0) {
            for (var i = 0; i < $scope.PubArticleScopeGridOptions.data.length; i++) {
                var gridRow = $scope.PubArticleScopeGridOptions.data[i];
                if (gridRow.ScopeID == null) {
                    alert('Please set all scopes.');
                    valid = false;
                    break;
                }
            }
        }
        //validate Author Grid
        if ($scope.PubArticleAuthorGridOptions.data.length == 0) {
            alert('Please add 1 Author at least.');
            valid = false;
        } else {
            for (var i = 0; i < $scope.PubArticleAuthorGridOptions.data.length; i++) {
                var gridRow = $scope.PubArticleAuthorGridOptions.data[i];
                if (gridRow.AuthorID == null) {
                    alert('Please set all authors.');
                    valid = false;
                    break;
                }
            }
        }
        
        if (valid && $('#form').valid()) {
            //set dates
            $scope.model.RecievedDate = $('#RecievedDate').val();
            $scope.model.AcceptDate = $('#AcceptDate').val();
            $scope.model.PublishDate = $('#PublishDate').val();
            $scope.model.PublishArticleAuthor = $scope.PubArticleAuthorGridOptions.data;
            $scope.model.PublishArticleScope = $scope.PubArticleScopeGridOptions.data;
            var data = $scope.model;
            console.log(data);
            var url = createActionUrl;
            showLoading();
            global.post(url, data, function (resp) {
                //redirect to index
                window.location.href = indexActionUrl;
                hideLoading();
            }, function (resp) {
                console.log("Error: " + error);
                hideLoading();
            });
            
        }
    }
}

function PublishedArticleEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function PublishedArticleDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}