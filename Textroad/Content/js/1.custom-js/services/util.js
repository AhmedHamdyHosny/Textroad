
function confirmService($uibModal) {
    var modalDefaults = {
        backdrop: true,
        keyboard: true,
        modalFade: true,
        templateUrl: '/Templates/Confirm.html'
    };

    var modalOptions = {
        closeButtonText: 'Close',
        actionButtonText: 'OK',
        headerText: 'Proceed?',
        bodyText: 'Perform this action?'
    };

    this.showModal = function (customModalDefaults, customModalOptions) {
        if (!customModalDefaults) customModalDefaults = {};
        customModalDefaults.backdrop = 'static';
        return this.show(customModalDefaults, customModalOptions);
    };

    this.show = function (customModalDefaults, customModalOptions) {
        //Create temp objects to work with since we're in a singleton service
        var tempModalDefaults = {};
        var tempModalOptions = {};

        //Map angular-ui modal custom defaults to modal defaults defined in service
        angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

        //Map modal.html $scope custom properties to defaults defined in service
        angular.extend(tempModalOptions, modalOptions, customModalOptions);

        if (!tempModalDefaults.controller) {
            tempModalDefaults.controller = function ($scope, $uibModalInstance) {
                $scope.modalOptions = tempModalOptions;
                $scope.modalOptions.ok = function (result) {
                    $uibModalInstance.close(result);
                };
                $scope.modalOptions.close = function (result) {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        }

        return $uibModal.open(tempModalDefaults).result;
    }
}

function gridService(uiGridConstants, $interval, $q, uiGridExporterConstants, uiGridExporterService, $filter, global) {
    return {
        initGrid: function ($scope, postBind, gridSort, gridFilters) {
            var fakeI18n = function (title) {
                var deferred = $q.defer();
                $interval(function () {
                    deferred.resolve('col: ' + title);
                }, 1000, 1);
                return deferred.promise;
            };
            var gridOptions = {};
            //Pagination
            $scope.pagination = {
                paginationPageSizes: [15, 25, 50, 75, 100], //, "All"
                ddlpageSize: 15,
                pageNumber: 1,
                pageSize: 15,
                totalItems: 0,

                getTotalPages: function () {
                    return Math.ceil(this.totalItems / this.pageSize);
                },
                pageSizeChange: function () {
                    if (this.ddlpageSize == "All")
                        this.pageSize = $scope.pagination.totalItems;
                    else
                        this.pageSize = this.ddlpageSize

                    this.pageNumber = 1
                    $scope.GetItems();
                },
                firstPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber = 1
                        $scope.GetItems();
                    }
                },
                nextPage: function () {
                    if (this.pageNumber < this.getTotalPages()) {
                        this.pageNumber++;
                        $scope.GetItems();
                    }
                },
                previousPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber--;
                        $scope.GetItems();
                    }
                },
                lastPage: function () {
                    if (this.pageNumber >= 1) {
                        this.pageNumber = this.getTotalPages();
                        $scope.GetItems();
                    }
                }
            };
            //ui-Grid Call
            $scope.GetItems = function () {
                $scope.result = "color-green";
                $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };
                $scope.gridOptions = {
                    useExternalPagination: true,
                    useExternalSorting: false,
                    enableFiltering: true,
                    //filterOptions: $scope.filterOptions,
                    //keepLastSelected: true,
                    //showColumnMenu: true,
                    //showFilter: true,
                    //showFooter: true,
                    //enableSorting: true,
                    enableRowSelection: true,
                    enableSelectAll: true,
                    enableGridMenu: true,
                    gridMenuTitleFilter: fakeI18n,
                    exporterMenuCsv: false,
                    exporterMenuPdf: false,
                    columnDefs: gridColumnDefs,
                    onRegisterApi: function (gridApi) {
                        $scope.gridApi = gridApi;
                        gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                            $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                        });
                        gridApi.core.on.filterChanged($scope,
                            function () {
                                var grid = this.grid;
                                //reset filters
                                gridOptions.Filters = [];
                                if (gridFilters != null) {
                                    gridOptions.Filters = gridFilters;
                                }
                                for (var i = 0; i < grid.columns.length; i++) {
                                    term = grid.columns[i].filters[0].term;
                                    field = grid.columns[i].field;
                                    if (field != null && term != undefined && term != null && term != '') {
                                        gridOptions.Filters.push({ Property: field, Operation: 'Like', Value: term, LogicalOperation: 'And' });
                                    }
                                }
                                //console.log(JSON.stringify(gridOptions));
                                //rebind grid data
                                $scope.GetItems();
                            });
                        gridApi.core.on.sortChanged($scope, function () {
                            var grid = this.grid;
                            //reset sort
                            gridOptions.Sorts = [];
                            for (var i = 0; i < grid.columns.length; i++) {
                                direction = grid.columns[i].sort.direction;
                                field = grid.columns[i].field;
                                priority = grid.columns[i].sort.priority
                                if (direction != undefined && direction != null && direction != '') {
                                    gridOptions.Sorts.push({ Property: field, SortType: direction, Priority: priority });
                                }
                            }
                            //console.log(JSON.stringify(gridOptions));
                            //rebind grid data
                            $scope.GetItems();
                        });
                    }
                };

                var NextPage;
                if (isNaN($scope.pagination.pageNumber)) {
                    NextPage = 1;
                } else {
                    NextPage = $scope.pagination.pageNumber;
                }
                var NextPageSize = $scope.pagination.pageSize;
                gridOptions.Paging = { PageNumber: NextPage, PageSize: NextPageSize };
                if (gridSort != null) {
                    gridOptions.Sorts = gridSort;
                } else {
                    gridOptions.Sorts = [{ Property: 'CreateDate', SortType: 'Desc', Priority: 1 }]
                }
                var url = getGridViewActionUrl; //+ '/' + NextPage + '/' + NextPageSize;
                global.post(url, gridOptions, function (resp) {
                    //console.log(JSON.stringify("hi--------------:"+resp.data));
                    $scope.pagination.totalItems = resp.data.TotalItemsCount;
                    $scope.gridOptions.data = resp.data.PageItems;
                    //console.log(JSON.stringify(resp.data.PageItems));
                    $scope.gridOptions.selectedItems = [];
                    if (postBind != null) {
                        postBind();
                    }

                }, function (resp) {
                    console.log("Error: " + error);
                }, function () {
                    $scope.loaderMore = true;
                }, function () {
                    $scope.loaderMore = false;
                });
            }
            //Default Load
            $scope.GetItems();
            $scope.GetTotalItems = function (exportAllData, postBind) {
                $scope.totalGridOptions = {
                    enableFiltering: true,
                    gridMenuTitleFilter: fakeI18n,
                    exporterMenuCsv: false,
                    exporterMenuPdf: false,
                    columnDefs: gridColumnDefs,
                    onRegisterApi: function (totalGridApi) {
                        $scope.totalGridApi = totalGridApi;
                        totalGridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                            $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                        });
                    }
                };

                $scope.totalGridOptions.data = [];
                if (exportAllData) {
                    gridOptions.Paging = null;
                    var url = getGridViewActionUrl;
                    global.post(url, gridOptions, function (resp) {
                        $scope.totalGridOptions.data = resp.data.PageItems;
                        //console.log(JSON.stringify(resp.data.PageItems));
                        if (postBind != null) {
                            postBind();
                        }
                    }, function (resp) {
                        console.log("Error: " + error);
                        hideLoading();
                    }, function () {
                        showLoading();
                    }, function () {
                    });
                }
            }
            //Default Load All Data
            $scope.GetTotalItems(false);

            $scope.ToggoleGridFilter = function ($event) {
                $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
                $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
            }
        },
        configureExport: function ($scope) {
            $scope.ExportAllDataAsPdf = function (e) {
                //bind total grid
                //var grid = $scope.totalGridApi.grid;
                //var rowTypes = uiGridExporterConstants.ALL;
                //var colTypes = uiGridExporterConstants.ALL;
                //uiGridExporterService.pdfExport(grid, rowTypes, colTypes);

                $scope.GetTotalItems(true, function () {
                    setTimeout(function () {
                        var rowTypes = uiGridExporterConstants.ALL;
                        var colTypes = uiGridExporterConstants.ALL;
                        uiGridExporterService.pdfExport($scope.totalGridApi.grid, rowTypes, colTypes);
                    }, 3000);
                });
            }
            $scope.ExportVisibleDataAsPdf = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.VISIBLE;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.pdfExport(grid, rowTypes, colTypes);
            }
            $scope.ExportAllDataAsCsv = function () {
                //var grid = $scope.totalGridApi.grid;
                //var rowTypes = uiGridExporterConstants.ALL;
                //var colTypes = uiGridExporterConstants.ALL;
                //uiGridExporterService.csvExport(grid, rowTypes, colTypes);

                //bind total grid
                $scope.GetTotalItems(true, function () {
                    setTimeout(function () {
                        var rowTypes = uiGridExporterConstants.ALL;
                        var colTypes = uiGridExporterConstants.ALL;
                        uiGridExporterService.csvExport($scope.totalGridApi.grid, rowTypes, colTypes);
                        hideLoading();
                    }, 3000);
                });

            }
            $scope.ExportVisibleDataAsCsv = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.VISIBLE;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.csvExport(grid, rowTypes, colTypes);
            }
        },
        configGrid: function ($scope, postBind, uiGrid, uiGridApi, gridColumnDefs, gridData, getGridDataActionUrl, withFilter, withSort, withPaging, gridSort) {
        var fakeI18n = function (title) {
            var deferred = $q.defer();
            $interval(function () {
                deferred.resolve('col: ' + title);
            }, 1000, 1);
            return deferred.promise;
        };
        var gridOpts = {};
        uiGridApi != null ? uiGridApi : "gridApi";
        uiGrid != null ? uiGrid : "gridOptions";
        //get item
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };

        $scope[uiGrid] = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: withFilter,
            enableRowSelection: true,
            enableSelectAll: true,
            enableGridMenu: true,
            gridMenuTitleFilter: fakeI18n,
            exporterMenuCsv: false,
            exporterMenuPdf: false,
            columnDefs: gridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope[uiGridApi] = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
                if (withFilter == true) {
                    gridApi.core.on.filterChanged($scope,
                        function () {
                            var grid = this.grid;
                            //reset filters
                            gridOpts.Filters = [];
                            if (gridFilters != null) {
                                gridOpts.Filters = gridFilters;
                            }
                            for (var i = 0; i < grid.columns.length; i++) {
                                term = grid.columns[i].filters[0].term;
                                field = grid.columns[i].field;
                                if (field != null && term != undefined && term != null && term != '') {
                                    gridOpts.Filters.push({ Property: field, Operation: 'Like', Value: term, LogicalOperation: 'And' });
                                }
                            }
                        });
                }
                if (withSort == true) {
                    gridApi.core.on.sortChanged($scope, function () {
                        var grid = this.grid;
                        //reset sort
                        gridOpts.Sorts = [];
                        for (var i = 0; i < grid.columns.length; i++) {
                            direction = grid.columns[i].sort.direction;
                            field = grid.columns[i].field;
                            priority = grid.columns[i].sort.priority
                            if (direction != undefined && direction != null && direction != '') {
                                gridOpts.Sorts.push({ Property: field, SortType: direction, Priority: priority });
                            }
                        }
                    });
                }
            },
        };
        //Pagination
        if (withPaging == true) {
            $scope.pagination = {
                paginationPageSizes: [15, 25, 50, 75, 100],
                ddlpageSize: 15,
                pageNumber: 1,
                pageSize: 15,
                totalItems: 0,
                getTotalPages: function () {
                    return Math.ceil(this.totalItems / this.pageSize);
                },
                pageSizeChange: function () {
                    if (this.ddlpageSize == "All")
                        this.pageSize = $scope.pagination.totalItems;
                    else
                        this.pageSize = this.ddlpageSize

                    this.pageNumber = 1
                    //rebind grid data
                    bindGridData($scope, postBind, uiGrid, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort);
                },
                firstPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber = 1
                        //rebind grid data
                        bindGridData($scope, postBind, uiGrid, gridData, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort);
                    }
                },
                nextPage: function () {
                    if (this.pageNumber < this.getTotalPages()) {
                        this.pageNumber++;
                        //rebind grid data
                        bindGridData($scope, postBind, uiGrid, gridData, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort);
                    }
                },
                previousPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber--;
                        //rebind grid data
                        bindGridData($scope, postBind, uiGrid, gridData, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort);
                    }
                },
                lastPage: function () {
                    if (this.pageNumber >= 1) {
                        this.pageNumber = this.getTotalPages();
                        //rebind grid data
                        bindGridData($scope, postBind, uiGrid, gridData, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort);
                    }
                }
            };
            var NextPage;
            if (isNaN($scope.pagination.pageNumber)) {
                NextPage = 1;
            } else {
                NextPage = $scope.pagination.pageNumber;
            }
            var NextPageSize = $scope.pagination.pageSize;
        }

        if (gridData != null) {
            $scope[uiGrid].data = gridData;
        } else {
            $scope[uiGrid].data = [];
        }
        },
        bindGridData: function ($scope, postBind, uiGrid, gridData, getGridDataActionUrl, gridOpts, withFilter, withSort, withPaging, gridSort) {
        if (gridData != null) {
            $scope[uiGrid].data = gridData;
        } else {
            if (withPaging == true) {
                gridOpts.Paging = { PageNumber: NextPage, PageSize: NextPageSize };
            }
            if (withSort == true) {
                if (gridSort != null) {
                    gridOpts.Sorts = gridSort;
                } else {
                    gridOpts.Sorts = [{ Property: 'CreateDate', SortType: 'Desc', Priority: 1 }]
                }
            }
            var url = getGridDataActionUrl;
            global.post(url, gridOpts, function (resp) {
                //console.log(JSON.stringify("hi--------------:"+resp.data));
                $scope.pagination.totalItems = resp.data.TotalItemsCount;
                $scope[uiGrid].data = resp.data.PageItems;
                //console.log(JSON.stringify(resp.data.PageItems));
                $scope[uiGrid].selectedItems = [];
                if (postBind != null) {
                    postBind();
                }
            }, function (resp) {
                console.log("Error: " + error);
            }, function () {
                $scope.loaderMore = true;
            }, function () {
                $scope.loaderMore = false;
            });
        }
    }
    }

}

function ctrlService() {
    return {
        initCtrl: function ($scope) {
            showAlert();
        }
    }
}
function popupExpand(expandIcon) {
    if ($(expandIcon).find($('i')).hasClass("fa-expand")) {
        //expand
        $('.modal-dialog').width('100%');
        $('.modal-dialog').css('margin', '0 0 0 0');
        $(expandIcon).find($('i')).addClass('fa-compress').removeClass('fa-expand');
    } else {
        //compress
        $('.modal-dialog').width('');
        $('.modal-dialog').css('margin', '');
        $(expandIcon).find($('i')).addClass('fa-expand').removeClass('fa-compress');
    }

}

function resizeGrid($scope, totalItems) {
    setTimeout(function () {
        $scope.gridStyle = { height: (totalItems * 30 + 60) + "px" };
    }, 400);
}
                