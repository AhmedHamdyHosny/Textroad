angular.module('app.serverControllers', [])

//Brand controllers ========
.controller('BrandCtrl', BrandCtrl)
.controller('BrandCreateCtrl', BrandCreateCtrl)
.controller('BrandEditCtrl', BrandEditCtrl)
.controller('BrandDetailsCtrl', BrandDetailsCtrl)

//SystemServerIP controllers ========
.controller('SystemServerIPCtrl', SystemServerIPCtrl)
.controller('SystemServerIPCreateCtrl', SystemServerIPCreateCtrl)
.controller('SystemServerIPEditCtrl', SystemServerIPEditCtrl)
.controller('SystemServerIPDetailsCtrl', SystemServerIPDetailsCtrl)

//BrandServerPort controllers ========
.controller('BrandServerPortCtrl', BrandServerPortCtrl)
.controller('BrandServerPortCreateCtrl', BrandServerPortCreateCtrl)
.controller('BrandServerPortEditCtrl', BrandServerPortEditCtrl)
.controller('BrandServerPortDetailsCtrl', BrandServerPortDetailsCtrl)

//SIMCard controllers ========
.controller('SIMCardCtrl', SIMCardCtrl)
.controller('SIMCardCreateCtrl', SIMCardCreateCtrl)
.controller('SIMCardEditCtrl', SIMCardEditCtrl)
.controller('SIMCardDetailsCtrl', SIMCardDetailsCtrl)

//SIMCardStatus controllers ========
.controller('SIMCardStatusCtrl', SIMCardStatusCtrl)
.controller('SIMCardStatusEditCtrl', SIMCardStatusEditCtrl)
.controller('SIMCardStatusDetailsCtrl', SIMCardStatusDetailsCtrl)

//Brand functions ========
function BrandCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {
    ctrlService.initCtrl($scope);
    gridService.initGrid($scope);
    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'BrandCreateCtrl',
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
            controller: 'BrandEditCtrl',
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
            controller: 'BrandDetailsCtrl',
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
                selectedIds.push(item.BrandId)
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

function BrandCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BrandEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BrandDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//SystemServerIP functions ========
function SystemServerIPCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {
    ctrlService.initCtrl($scope);
    gridService.initGrid($scope);
    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SystemServerIPCreateCtrl',
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
            controller: 'SystemServerIPEditCtrl',
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
            controller: 'SystemServerIPDetailsCtrl',
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
                selectedIds.push(item.SystemServerId)
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

function SystemServerIPCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SystemServerIPEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SystemServerIPDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//BrandServerPort functions ========
function BrandServerPortCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {
    ctrlService.initCtrl($scope);
    gridService.initGrid($scope);
    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'BrandServerPortCreateCtrl',
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
            controller: 'BrandServerPortEditCtrl',
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
            controller: 'BrandServerPortDetailsCtrl',
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
                selectedIds.push(item.BrandPortId)
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

function BrandServerPortCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BrandServerPortEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BrandServerPortDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//SIMCard functions ========
function SIMCardCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SIMCardCreateCtrl',
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
            controller: 'SIMCardEditCtrl',
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
            controller: 'SIMCardDetailsCtrl',
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
                selectedIds.push(item.SIMCardId)
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

function SIMCardCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//SIMCardStatus functions ========
function SIMCardStatusCtrl($scope, $uibModal, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'SIMCardStatusEditCtrl',
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
            controller: 'SIMCardStatusDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function SIMCardStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}