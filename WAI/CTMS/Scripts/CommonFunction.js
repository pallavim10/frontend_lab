//Open Popup
function OpenPopUp(element, title) {

    $("#" + element + "").removeClass("disp-none");

    $("#" + element + "").dialog({
        title: title,
        width: 900,
        height: 500,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });
    return false;
}

function GetOverrideCommnets() {
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/GetOverrideComments",
        data: '{Id: "' + overrideid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                // alert(data.d);
                $('#grdOverrideComments').html(data.d)
            }
        },
        failure: function (response) {
            if (response.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}

$.expr[':'].textEquals = function (a, i, m) {
    var textToFind = m[3].replace(/[-[\]{}(')*+?.[,\\^$|#\s]/g, '\\$&'); // escape special character for regex 
    return $(a).text().match("^" + textToFind + "$");
};

function checkVerifyAll(chk_Verify_All) {
    $('.sdvCheckbox').each(function (index, element) {
        if (!$(element).closest('tr').hasClass("disp-none")) {

            if ($(chk_Verify_All).is(":checked")) {
                $(element).find("input").prop("checked", true);
                $("input[id*='hfSDV']").val("true");
            }
            else {
                $(element).find("input").prop("checked", false);
                $("input[id*='hfSDV']").val("false");
            }
        }
    });
}

function chkVerifyHF(chk) {

    if ($(chk).is(":checked")) {
        $(chk).closest('td').find('input[type=hidden]').val("true");
    }
    else {
        $(chk).closest('td').find('input[type=hidden]').val("false");
    }
}

//on cancel click of update reason for change popup
function CancelOverrideData() {
    $("#popup_Override").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}

//For ajax function call on update data button click
function SAE_saveFieldComments() {

    if ($('#txtSAE_FieldComments').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var variableName = $("#lblSAE_VariableName").text();
    var commnets = $("#txtSAE_FieldComments")[0].value;
    var fieldname = $("#lblSAE_FieldName").text();
    var SITEID = $("#MainContent_hdnSITEID").val();
    var SUBJID = $("#MainContent_hdnSubjectID").val();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var SAE = $("#MainContent_hdnSAE").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULENAME = $("#MainContent_drpModule").find("option:selected").text();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    var STATUS = $("#MainContent_hdnStatus").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SAE_SetFielsComments",
        data: '{VariableName: "' + variableName + '",Comments: "' + commnets + '",FieldName: "' + fieldname + '",ModuleName: "' + MODULENAME + '",INVID: "' + SITEID + '",SUBJECTID: "' + SUBJID + '",SAE: "' + SAE + '",SAEID: "' + SAEID + '",STATUS: "' + STATUS + '",MODULEID: "' + MODULEID + '",RECID:"' + RECID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert(data.d);
                $(location).attr('href', window.location.href);
            }
        },
        failure: function (response) {
            if (response.d == "Object reference not set to an instance of an object.") {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}

function SAE_GetOverrideCommnets() {
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SAE_GetOverrideComments",
        data: '{Id: "' + overrideid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                // alert(data.d);
                $('#SAE_grdOverrideComments').html(data.d)
            }
        },
        failure: function (response) {
            if (response.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}

//on cancel click of update reason for change popup
function SAE_CancelOverrideData() {
    $("#SAE_popup_Override").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}

//For ajax function call on update data button click
function SAE_ADD_PAGE_COMMENTS() {

    if ($('#txtPageCOMMENTS').val() == '') {
        alert('Please enter Comment');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var STATUS = $("#MainContent_hdnStatus").val();
    var COMMENTS = $("#txtPageCOMMENTS").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SAE_ADD_PAGE_COMMENTS",
        data: '{SAEID: "' + SAEID + '", RECID: "' + RECID + '" , COMMENTS: "' + COMMENTS + '",STATUS: "' + STATUS + '" , MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', window.location.href);
            }
            else {
                alert(data.d);
                $(location).attr('href', window.location.href);
            }
        },
        failure: function (response) {
            if (response.d == "Object reference not set to an instance of an object.") {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}

function SAE_ADD_MR_PAGE_COMMENTS() {

    if ($('#txtMR_PAGE_Comments').val() == '') {
        alert('Please enter Comment');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var STATUS = $("#MainContent_hdnStatus").val();
    var COMMENTS = $("#txtMR_PAGE_Comments").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SAE_ADD_MR_PAGE_COMMENTS",
        data: '{SAEID: "' + SAEID + '", RECID: "' + RECID + '" , COMMENTS: "' + COMMENTS + '",STATUS: "' + STATUS + '" , MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', window.location.href);
            }
            else {
                alert(data.d);
                $(location).attr('href', window.location.href);
            }
        },
        failure: function (response) {
            if (response.d == "Object reference not set to an instance of an object.") {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}

function TBLDM_OnSuccess(response) {

    var text = response.d;
    text.includes("/")

    // if (response.d == "Record Updated successfully.")
    if (text.includes("Record Updated successfully.")) {
        console.log(response.d);

        $('#' + CurrentObj + '').val($('#MainContent_TBLtxt_NewValue').val()); //on cancel click set old value to control

        $("#TBLpopup_UpdateData").dialog('close');

        //$("#MainContent_btnSaveDataQuery").click();

        return true;
    }
    if (response.d == 'Object reference not set to an instance of an object.') {
        alert("Session Expired");
        var url = "SessionExpired.aspx";
        $(location).attr('href', url);
    }
    else {
        alert("Some error occured contact administartor");
        console.log(response.d);
        if (CurrentObjType == "checkbox") {
            if ($("#MainContent_TBLtxt_OldValue")[0].value == "True")
                $('#' + CurrentObj + '').attr('checked', true);
            else
                $('#' + CurrentObj + '').attr('checked', false);
        }
        $('#' + CurrentObj + '').val($("#MainContent_TBLtxt_OldValue")[0].value.trim()); //on cancel click set old value to control
        $("#TBLpopup_UpdateData").dialog('close');
        return false;
    }
}