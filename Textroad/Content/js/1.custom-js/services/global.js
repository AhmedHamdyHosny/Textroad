function global ($http) {
    //holds the data got from the server, to be used by controllers 
    //that needs data to be loaded before itself
    var resolvedData = null;
    //var N_Error = {Status: '',Message: '',Message_no: ''};
    //var g_reqs = null;

    return {
        post: function (url, data, success, faliure,showCustLoading, hideCustLoading, json) {
            //console.log('<== posting data \r\n'+url +'\r\n'+JSON.stringify(data));

            var config = {}; //headers: { 'Content-Type': 'application/json' }
            
            if (json == false) {
                config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }};
            }
            //show loading page
            if (showCustLoading != undefined && showCustLoading != null) {
                showCustLoading();
            } else {
                showLoading();
            }

            $http.post(url, data, config).then(
                function (resp) {
                    //hide loading page
                    if (hideCustLoading != undefined && hideCustLoading != null) {
                        hideCustLoading();
                    } else {
                        hideLoading();
                    }
                    //console.log('==> success response \r\n' + JSON.stringify(resp.data));
                    success(resp);
                },
                function (resp) {
                    //hide loading page
                    if (hideCustLoading != undefined && hideCustLoading != null) {
                        hideCustLoading();
                    } else {
                        hideLoading();
                    }
                    console.log('==> faliure response \r\n' + JSON.stringify(resp.data));
                    //show error
                });
        },
        //=======
        get: function (url, success, faliure,showCustLoading,hideCustLoading) {
            //console.log('<== getting data \r\n'+ url);
            //show loading page
            if (showCustLoading != undefined && showCustLoading != null) {
                showCustLoading();
            } else {
                showLoading();
            }
            
            $http.get(url).then(
                function (resp) {
                    //hide loading page
                    if (hideCustLoading != undefined && hideCustLoading != null) {
                        hideCustLoading();
                    } else {
                        hideLoading();
                    }
                    //console.log('==> success response \r\n' + JSON.stringify(resp.data));
                    success(resp);
                },
                function (resp) {
                    //hide loading page
                    if (hideCustLoading != undefined && hideCustLoading != null) {
                        hideCustLoading();
                    } else {
                        hideLoading();
                    }
                    console.log('==> faliure response \r\n' + JSON.stringify(resp.data));
                    //show error
                });
        },
        //======
        toast: function(msg){
            //$ionicLoading.show({ template: msg, noBackdrop: true, duration: 2000 });
        },
        //======
        infoMsg: function (title, msg, then) {
            //$ionicPopup.alert({
            //    title: title,
            //    template: msg,
            //    okText: 'OK',
            //    okType:'button-calm'
            //}).then(function (ok) { if(then){ then(ok);} });
        },
        //======
        confirmMsg: function (title, msg, then) {
            //$ionicPopup.confirm({
            //    title: title,
            //    template: msg,
            //    okText: 'Yes',
            //    okType: 'button-calm',
            //    cancelText: 'No',
            //    cancelType:'button-stable'
            //}).then(function (yes) { if(then){ then(yes);} });
        },
        //======
        //store: function (name, object) {
        //    $localstorage.setObject(name, object);
        //},
        //======
        retrieve: function (name) {
            return $localstorage.getObject(name);
        },
        //======
        getResolvedData: function () {
            return resolvedData;
        },
        //======
        setResolvedData: function (data) {
            resolvedData = data;
            
        }
    }
}