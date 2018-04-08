angular.module('app.adminControllers', [])

//Branch controllers ========
.controller('BranchCtrl', BranchCtrl)
.controller('BranchCreateCtrl', BranchCreateCtrl)
.controller('BranchEditCtrl', BranchEditCtrl)
.controller('BranchDetailsCtrl', BranchDetailsCtrl)

//SIMCardProvider controllers ========
.controller('SIMCardProviderCtrl', SIMCardProviderCtrl)
.controller('SIMCardProviderCreateCtrl', SIMCardProviderCreateCtrl)
.controller('SIMCardProviderEditCtrl', SIMCardProviderEditCtrl)
.controller('SIMCardProviderDetailsCtrl', SIMCardProviderDetailsCtrl)

//SIMCardContract controllers ========
.controller('SIMCardContractCtrl', SIMCardContractCtrl)
.controller('SIMCardContractCreateCtrl', SIMCardContractCreateCtrl)
.controller('SIMCardContractEditCtrl', SIMCardContractEditCtrl)
.controller('SIMCardContractDetailsCtrl', SIMCardContractDetailsCtrl)

//TechniqueCompany controllers ========
.controller('TechniqueCompanyCtrl', TechniqueCompanyCtrl)
.controller('TechniqueCompanyCreateCtrl', TechniqueCompanyCreateCtrl)
.controller('TechniqueCompanyEditCtrl', TechniqueCompanyEditCtrl)
.controller('TechniqueCompanyDetailsCtrl', TechniqueCompanyDetailsCtrl)

//TechniqueSystem
.controller('TechniqueSystemCtrl', TechniqueSystemCtrl)
.controller('TechniqueSystemCreateCtrl', TechniqueSystemCreateCtrl)
.controller('TechniqueSystemEditCtrl', TechniqueSystemEditCtrl)
.controller('TechniqueSystemDetailsCtrl', TechniqueSystemDetailsCtrl)

//CustomerServerStatus
.controller('CustomerServerStatusCtrl', CustomerServerStatusCtrl)
.controller('CustomerServerStatusCreateCtrl', CustomerServerStatusCreateCtrl)
.controller('CustomerServerStatusEditCtrl', CustomerServerStatusEditCtrl)
.controller('CustomerServerStatusDetailsCtrl', CustomerServerStatusDetailsCtrl)

//Branch functions ========
function BranchCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'BranchCreateCtrl',
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
            controller: 'BranchEditCtrl',
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
            controller: 'BranchDetailsCtrl',
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
                selectedIds.push(item.BranchId)
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

function BranchCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BranchEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BranchDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//SIMCardProvider functions ========
function SIMCardProviderCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SIMCardProviderCreateCtrl',
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
            controller: 'SIMCardProviderEditCtrl',
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
            controller: 'SIMCardProviderDetailsCtrl',
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
                selectedIds.push(item.ProviderId)
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

function SIMCardProviderCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardProviderEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardProviderDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//SIMCardContract functions ========
function SIMCardContractCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SIMCardContractCreateCtrl',
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
            controller: 'SIMCardContractEditCtrl',
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
            controller: 'SIMCardContractDetailsCtrl',
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
                selectedIds.push(item.SIMCardContractId)
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

function SIMCardContractCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardContractEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardContractDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//TechniqueCompany functions ========
function TechniqueCompanyCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'TechniqueCompanyCreateCtrl',
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
            controller: 'TechniqueCompanyEditCtrl',
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
            controller: 'TechniqueCompanyDetailsCtrl',
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
                selectedIds.push(item.CompanyId)
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

function TechniqueCompanyCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function TechniqueCompanyEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function TechniqueCompanyDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//TechniqueSystem functions ========
function TechniqueSystemCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'TechniqueSystemCreateCtrl',
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
            controller: 'TechniqueSystemEditCtrl',
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
            controller: 'TechniqueSystemDetailsCtrl',
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
                selectedIds.push(item.SystemId)
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

function TechniqueSystemCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function TechniqueSystemEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function TechniqueSystemDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//CustomerServerStatus functions ========
function CustomerServerStatusCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {
    ctrlService.initCtrl($scope);
    gridService.initGrid($scope);
    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'CustomerServerStatusCreateCtrl',
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
            controller: 'CustomerServerStatusEditCtrl',
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
            controller: 'CustomerServerStatusDetailsCtrl',
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
                selectedIds.push(item.CustomerServerStatusId)
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

function CustomerServerStatusCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function CustomerServerStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function CustomerServerStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}