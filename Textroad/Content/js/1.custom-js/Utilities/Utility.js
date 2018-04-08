

//function DeleteConfirm(successCallBack, cancelCallBack)
//{
//    bootbox.dialog({
//        message: 'Are you sure?',
//        title: "Delete Confirm",
//        className: "modal-darkorange",
//        buttons: {
//            "Cancel": {
//                callback: cancelCallBack
//            },
//            success: {
//                label: "Ok",
//                className: "btn-danger",
//                callback: successCallBack
//            }
//        }
//    });

//    //bootbox.confirm("Are you sure?", function (result) {
//    //    if (result) {
//    //        alert("delete");
//    //    }
//    //});
//}

function showAlert() {
    
    if ($('#alert') != undefined && $('#alert') != null && $('#alert').children().length > 0 )
    {
        $('#alert').slideDown(300);
        setTimeout(function () {
            $('#alert').slideUp(300);
            $('#alert').html('');
        }, 5000);
    }
}

function showLoading() {
    //document.getElementById('loadingContainer').className = 'loading-container loading-active';

    $('.loading-container').removeClass('loading-inactive');
    $('.loading-container').addClass('loading-active');
}

function hideLoading() {
    //document.getElementById('loadingContainer').className = 'loading-container loading-inactive';

    $('.loading-container').removeClass('loading-active');
    $('.loading-container').addClass('loading-inactive');
}

function showImportDilogFile() {
    $('#upload').click();
}

function fileChange(obj) {
    var fileName = $(obj).val();
    showLoading();
    $('#btnImportExcel').click();
}

function getFormatDate(datetime) {
    if (datetime != null) {
        var mm = datetime.getMonth() + 1;
        var dd = datetime.getDate();
        var yy = datetime.getFullYear();
        return (dd > 9 ? '' : '0') + dd + '/' + (mm > 9 ? '' : '0') + mm + '/' + yy;
    } else {
        return null;
    }
}

function fromStringToDate(dateTimeStr, formate) {
    var datetime = null;
    if (dateTimeStr != null && dateTimeStr != '') {
        var parts = dateTimeStr.split('/');
        datetime = new Date(parts[2], parts[1] - 1, parts[0]);
    }
    return datetime;
}

function gridChckSelectAll(checked, gridData, prop) {
    for (var i = 0; i < gridData.length; i++) {
        gridData[i][prop] = checked;
    }
}

function setValidationError(validationId, msg) {
    //Removes validation from input-fields
    $('#' + validationId + ' .input-validation-error').addClass('input-validation-error');
    $('#' + validationId + ' .input-validation-error').removeClass('input-validation-valid');
    //Removes validation message after input-fields
    $('#' + validationId).addClass('field-validation-error');
    $('#' + validationId).removeClass('field-validation-valid');
    $('#' + validationId).empty();
    $('#' + validationId).append('<span for="' + validationId + '" class="">' + msg + '</span>');
    //Removes validation summary
    $('#' + validationId + ' .validation-summary-errors').addClass('validation-summary-errors');
    $('#' + validationId + ' .validation-summary-errors').removeClass('validation-summary-valid');
}

function removeValidationError(validationId) {
    //Removes validation from input-fields
    $('#' + validationId + ' .input-validation-error').addClass('input-validation-valid');
    $('#' + validationId + ' .input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('#' + validationId + ' .field-validation-error').addClass('field-validation-valid');
    $('#' + validationId + ' .field-validation-error').removeClass('field-validation-error');
    $('#' + validationId).empty();
    //Removes validation summary
    $('#' + validationId + ' .validation-summary-errors').addClass('validation-summary-valid');
    $('#' + validationId + ' .validation-summary-errors').removeClass('validation-summary-errors');
}
