angular.module('app.storeControllers', [])

//Device controllers ========
.controller('DeviceCtrl', DeviceCtrl)
.controller('DeviceCreateCtrl', DeviceCreateCtrl)
.controller('DeviceEditCtrl', DeviceEditCtrl)
.controller('DeviceDetailsCtrl', DeviceDetailsCtrl)

//DeviceStatus controllers ========
.controller('DeviceStatusCtrl', DeviceStatusCtrl)
.controller('DeviceStatusEditCtrl', DeviceStatusEditCtrl)
.controller('DeviceStatusDetailsCtrl', DeviceStatusDetailsCtrl)

//DeviceModelType controllers ========
.controller('DeviceModelTypeCtrl', DeviceModelTypeCtrl)
.controller('DeviceModelTypeCreateCtrl', DeviceModelTypeCreateCtrl)
.controller('DeviceModelTypeEditCtrl', DeviceModelTypeEditCtrl)
.controller('DeviceModelTypeDetailsCtrl', DeviceModelTypeDetailsCtrl)

//Device functions ========
function DeviceCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeviceCreateCtrl',
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
            controller: 'DeviceEditCtrl',
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
            controller: 'DeviceDetailsCtrl',
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
                selectedIds.push(item.DeviceId)
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

function DeviceCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeviceStatus functions ========
function DeviceStatusCtrl($scope, $uibModal, gridService, ctrlService) {
    
    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'DeviceStatusEditCtrl',
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
            controller: 'DeviceStatusDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function DeviceStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeviceModelType functions ========
function DeviceModelTypeCtrl($scope, $uibModal, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeviceModelTypeCreateCtrl',
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
            controller: 'DeviceModelTypeEditCtrl',
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
            controller: 'DeviceModelTypeDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function DeviceModelTypeCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceModelTypeEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceModelTypeDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}