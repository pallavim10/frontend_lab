function SAE_OpenOverideData_ReadOnly(element) {

    var ID = $(element).closest('tr').find('td').eq(0).text().trim();
    var STATUS = $(element).closest('tr').find('td').eq(7).text().trim();
    var QUERYTEXT = $(element).closest('tr').find('td').eq(2).text().trim();

    $('#SAE_txt_Overrideid_ReadOnly').val(ID);
    $('#SAE_txt_OverrideComm_ReadOnly').val("");
    $('#SAE_txtQueryText_ReadOnly').val(QUERYTEXT);

    GetOverrideCommnets_ReadOnly(ID);

    $('#SAE_DivQueryText_ReadOnly').addClass("disp-none");

    $("#SAE_popup_Override_ReadOnly").dialog({
        title: "Action on the query",
        width: 900,
        height: 500,
        modal: true
    });

    return false;
}

function GetOverrideCommnets_ReadOnly(ID) {
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowQueryComment",
        data: '{ID: "' + ID + '"}',
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
                $('#SAE_grdOverrideComments_ReadOnly').html(data.d)
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

function SAE_drpoverrideReason_ReadOnly_Change() {

    if ($("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() == "Re-query") {
        $("#SAE_DivQueryText_ReadOnly").removeClass("disp-none");
    }
    else {
        $("#SAE_DivQueryText_ReadOnly").addClass("disp-none");
    }
}

function SAE_UpdateOverrideData_ReadOnly() {

    if ($("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() == "0") {
        alert("Please select action");
        return false;
    }
    else if ($("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() == "Re-query" && $("#SAE_txtQueryText_ReadOnly")[0].value == "") {
        alert("Please enter querytext");
        return false;
    }
    else if ($("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() != "Re-query" && $("#SAE_txt_OverrideComm_ReadOnly")[0].value == "") {
        var newLine = "\r\n"

        var error_msg = "Do you want to close the query without a comment?";

        error_msg += newLine;
        error_msg += newLine;

        error_msg += "Note : Press OK to Proceed.";

        if (confirm(error_msg)) {

            After_Confirm_Closed_Query();

            return true;

        } else {

            return false;
        }
    }

    // Prepare the data object
    var dataObj = {
        Id: $("#SAE_txt_Overrideid_ReadOnly").val().trim(),
        Comment: $("#SAE_txt_OverrideComm_ReadOnly").val().trim(),   // Preserve special characters
        Reason: $("#SAE_drpoverrideReason_ReadOnly").val().trim(),
        QUERYTEXT: $("#SAE_txtQueryText_ReadOnly").val().trim()
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);


    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Update_Comment_Status_ReadOnly",
        /*data: '{Id: "' + $("#SAE_txt_Overrideid_ReadOnly")[0].value.trim() + '",Comment: "' + $("#SAE_txt_OverrideComm_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",Reason:"' + $("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() + '",QUERYTEXT: "' + $("#SAE_txtQueryText_ReadOnly")[0].value + '"}',*/
        data: jsonData,
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

function After_Confirm_Closed_Query() {



    // Prepare the data object
    var dataObj = {
        Id: $("#SAE_txt_Overrideid_ReadOnly").val().trim(),
        Comment: $("#SAE_txt_OverrideComm_ReadOnly").val().trim(),   // Preserve special characters
        Reason: $("#SAE_drpoverrideReason_ReadOnly").val().trim(),
        QUERYTEXT: $("#SAE_txtQueryText_ReadOnly").val().trim()
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);


    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Update_Comment_Status_ReadOnly",
        data: jsonData,
       /* data: '{Id: "' + $("#SAE_txt_Overrideid_ReadOnly")[0].value.trim() + '",Comment: "' + $("#SAE_txt_OverrideComm_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",Reason:"' + $("#SAE_drpoverrideReason_ReadOnly")[0].value.trim() + '",QUERYTEXT: "' + $("#SAE_txtQueryText_ReadOnly")[0].value + '"}',*/
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

function SAE_CancelOverrideData_ReadOnly() {

    $("#SAE_popup_Override_ReadOnly").dialog('close');
    $("#SAE_drpoverrideReason_ReadOnly").val("0");
    $("#SAE_drpAction_ReadOnly").val("0");
    $("#SAE_txt_OverrideComm_ReadOnly").val("");
    return false;
}

function SAE_OpenOverideData(element) {

    var ID = $(element).closest('tr').find('td').eq(0).text().trim();

    $('#SAE_txt_Overrideid').val(ID);
    $('#SAE_txt_OverrideComm').val("");

    GetOverrideCommnets(ID);

    $('#SAE_OverrideComments').addClass("disp-none");

    $("#SAE_popup_Override").dialog({
        title: "Answer the query",
        width: 900,
        height: 500,
        modal: true
    });

    return false;
}

function GetOverrideCommnets(ID) {
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowQueryComment",
        data: '{ID: "' + ID + '"}',
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

function SAE_drpAction_Change() {

    if ($("#SAE_drpAction")[0].value.trim() == "Other") {
        $("#SAE_OverrideComments").removeClass("disp-none");
    }
    else {
        $("#SAE_OverrideComments").addClass("disp-none");
    }
}

function SAE_UpdateOverrideData() {

    if ($("#SAE_drpAction")[0].value == "0") {
        alert("Please select reason");
        return false;
    }
    else if ($("#SAE_drpAction")[0].value == "Other" && $("#SAE_txt_OverrideComm")[0].value == "") {
        alert("Please enter comment");
        return false;
    }


    // Prepare the data object
    var dataObj = {
        Id: $("#SAE_txt_Overrideid").val().trim(),
        Comment: $("#SAE_txt_OverrideComm").val().trim(),  // Keep special characters intact
        QueryAction: $("#SAE_drpAction").val()
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Update_Comment_Status",
       /* data: '{Id: "' + $("#SAE_txt_Overrideid")[0].value.trim() + '",Comment: "' + $("#SAE_txt_OverrideComm")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",QueryAction: "' + $("#SAE_drpAction")[0].value + '"}',*/

        data: jsonData,
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

function SAE_CancelOverrideData() {
    $("#SAE_popup_Override").dialog('close');
    $("#SAE_drpoverrideReason").val("0");
    $("#SAE_drpAction").val("0");
    $("#SAE_txt_OverrideComm").val("");
    return false;
}