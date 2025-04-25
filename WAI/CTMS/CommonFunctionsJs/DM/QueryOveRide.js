function OpenOverideData_ReadOnly(element, ID) {

    var QUERYTEXT = $(element).closest('tr').find('td').eq(8).text().trim();
    //var MM_QUERYID = $(element).closest('tr').find('td').eq(2).text().trim();

    //  Check if MM_QUERYID exists in <a> with fa-history icon**
    var historyIcon = $(element).closest('tr').find("a[id='lnk'] i.fa-history").closest("a"); // Find history <a>
    if (historyIcon.length > 0) {
        var onclickValue = historyIcon.attr("onclick");
        var match = onclickValue ? onclickValue.match(/\d+/) : null;
        var MM_QUERYID = match ? match[0] : ""; // Assign MM_QUERYID if found
    }

    $('#txt_Overrideid_ReadOnly').val(ID);
    $('#txt_OverrideComm_ReadOnly').val("");
    $('#txtQueryText_ReadOnly').val(QUERYTEXT);

    if (MM_QUERYID != "" && MM_QUERYID != "undefined" && MM_QUERYID != null) {
        $("#drpoverrideReason_ReadOnly").children('option:gt(2)').show();
    }
    else {
        $("#drpoverrideReason_ReadOnly").children('option:gt(2)').hide();
    }

    GetOverrideCommnets_ReadOnly(ID);

    $('#DivQueryText_ReadOnly').addClass("disp-none");

    $("#popup_Override_ReadOnly").dialog({
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
        url: "AjaxFunction_DM.aspx/ShowQueryComment",
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
                $('#grdOverrideComments_ReadOnly').html(data.d)
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

function drpoverrideReason_ReadOnly_Change() {

    if ($("#drpoverrideReason_ReadOnly")[0].value.trim() == "Re-query") {
        $("#DivQueryText_ReadOnly").removeClass("disp-none");
    }
    else {
        $("#DivQueryText_ReadOnly").addClass("disp-none");
    }
}

function UpdateOverrideData_ReadOnly() {

    if ($("#drpoverrideReason_ReadOnly")[0].value.trim() == "0") {
        alert("Please select action");
        return false;
    }
    else if ($("#drpoverrideReason_ReadOnly")[0].value.trim() == "Re-query" && $("#txtQueryText_ReadOnly")[0].value == "") {
        alert("Please enter querytext");
        return false;
    }
    else if ($("#drpoverrideReason_ReadOnly")[0].value.trim() != "Re-query" && $("#txt_OverrideComm_ReadOnly")[0].value == "") {
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

    // Create JSON object with special characters handled properly
    var dataObj = {
        Id: $("#txt_Overrideid_ReadOnly").val().trim(),
        Comment: $("#txt_OverrideComm_ReadOnly").val().trim(),        // Preserve special characters
        Reason: $("#drpoverrideReason_ReadOnly").val().trim(),
        QUERYTEXT: $("#txtQueryText_ReadOnly").val().trim()            // Preserve special characters
    };

    // Convert to JSON string
    var jsonData = JSON.stringify(dataObj);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Update_Comment_Status_ReadOnly",
        /* data: '{Id: "' + $("#txt_Overrideid_ReadOnly")[0].value.trim() + '",Comment: "' + $("#txt_OverrideComm_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",Reason:"' + $("#drpoverrideReason_ReadOnly")[0].value.trim() + '",QUERYTEXT: "' + $("#txtQueryText_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '"}',*/
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

    // Create JSON object to preserve special characters
    var dataObj = {
        Id: $("#txt_Overrideid_ReadOnly").val().trim(),
        Comment: $("#txt_OverrideComm_ReadOnly").val().trim(),         // Keeps special characters
        Reason: $("#drpoverrideReason_ReadOnly").val().trim(),
        QUERYTEXT: $("#txtQueryText_ReadOnly").val().trim()             // Keeps special characters
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Update_Comment_Status_ReadOnly",
        /*  data: '{Id: "' + $("#txt_Overrideid_ReadOnly")[0].value.trim() + '",Comment: "' + $("#txt_OverrideComm_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",Reason:"' + $("#drpoverrideReason_ReadOnly")[0].value.trim() + '",QUERYTEXT: "' + $("#txtQueryText_ReadOnly")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '"}',*/

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

function CancelOverrideData_ReadOnly() {

    $("#popup_Override_ReadOnly").dialog('close');
    $("#drpoverrideReason_ReadOnly").val("0");
    $("#drpAction_ReadOnly").val("0");
    $("#txt_OverrideComm_ReadOnly").val("");
    return false;
}

//function OpenOverideData(element, ID) {

//    $('#hdnQuery_OverrideId').val('');

//    var QueryType = $(element).closest('tr').find('td').eq(10).text().trim();
//    var PVID, RECID = "";

//    if (QueryType == 'Automatic') {
//        PVID = $(element).closest('tr').find('td').eq(2).text().trim();
//        VARIABLENAME = $(element).closest('tr').find('td').eq(3).text().trim();
//    }
//    else {
//        PVID = $(element).closest('tr').find('td').eq(3).text().trim();
//        VARIABLENAME = $(element).closest('tr').find('td').eq(4).text().trim();
//    }

//    var newLine = "\r\n"

//    var error_msg = "Do you want to update and answer this query?"

//    error_msg += newLine;
//    error_msg += newLine;

//    error_msg += "Note : Press OK to proceed or Cancel to answer the query.";

//    if (confirm(error_msg)) {

//        if (!$("#popup_OpenQuery").hasClass('disp-none')) {
//            $("#popup_OpenQuery").addClass('disp-none')
//            $("#popup_OpenQuery").dialog("close");
//        }
//        else if (!$("#popup_AnsQuery").hasClass('disp-none')) {
//            $("#popup_AnsQuery").addClass('disp-none')
//            $("#popup_AnsQuery").dialog("close");
//        }

//        $('#hdnQuery_OverrideId').val(ID);
//        $('#hdnQueryVariableName').val(VARIABLENAME);
//        ResolveHighlightControl(PVID, VARIABLENAME);

//        return true;

//    } else {

//        $('#txt_Overrideid').val(ID);
//        $('#txt_OverrideComm').val("");

//        GetOverrideCommnets(ID);

//        $('#OverrideComments').addClass("disp-none");

//        $("#popup_Override").dialog({
//            title: "Answer the query",
//            width: 900,
//            height: 500,
//            modal: true
//        });

//        return false;
//    }

//}


function OpenOverideData(element, ID) {
    $('#hdnQuery_OverrideId').val('');
    $('#hdnQueryVariableName').val('');

    var QueryType = $(element).closest('tr').find('td').eq(10).text().trim();
    var PVID, VARIABLENAME = "";

    PVID = $(element).closest('tr').find('td').eq(3).text().trim();
    VARIABLENAME = $(element).closest('tr').find('td').eq(4).text().trim();

    // Ensure Swal.fire is available
    if (typeof Swal !== "undefined") {
        Swal.fire({
            title: "Do you want to update and answer this query?",
            text: "Note: Press Yes to proceed or No to answer the query.",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Yes",
            cancelButtonText: "No",
            allowOutsideClick: false,  // Prevent closing when clicking outside
            allowEscapeKey: false,     // Prevent closing on "Esc" key
            allowEnterKey: false,      // Prevent closing on "Enter" key
            backdrop: "rgba(0, 0, 0, 0.6)", // Blackish background blur effect
            customClass: {
                popup: "large-swal-popup",
                confirmButton: "swal-confirm-btn",
                cancelButton: "swal-cancel-btn",
                icon: "small-icon"  // Custom class for the icon
            }
        }).then((result) => {
            if (result.isConfirmed) {
                // User clicked "Yes"
                if (!$("#popup_OpenQuery").hasClass('disp-none')) {
                    $("#popup_OpenQuery").addClass('disp-none').dialog("close");
                } else if (!$("#popup_AnsQuery").hasClass('disp-none')) {
                    $("#popup_AnsQuery").addClass('disp-none').dialog("close");
                }

                $('#hdnQuery_OverrideId').val(ID);
                $('#hdnQueryVariableName').val(VARIABLENAME);
                ResolveHighlightControl(PVID, VARIABLENAME);
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                // User clicked "No"
                $('#txt_Overrideid').val(ID);
                $('#txt_OverrideComm').val("");
                GetOverrideCommnets(ID);
                $('#OverrideComments').addClass("disp-none");

                $("#popup_Override").dialog({
                    title: "Answer the query",
                    width: 900,
                    height: 500,
                    modal: true,
                    open: function () {
                        $(this).removeAttr("aria-hidden"); // ✅ Ensure modal is accessible
                    }
                });
            }
        });
    }
}



function GetOverrideCommnets(ID) {
    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowQueryComment",
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

function drpAction_Change() {

    if ($("#drpAction")[0].value.trim() == "Other") {
        $("#OverrideComments").removeClass("disp-none");
    }
    else {
        $("#OverrideComments").addClass("disp-none");
    }
}

//function UpdateOverrideData() {

//    if ($("#drpAction")[0].value == "0") {
//        alert("Please select reason");
//        return false;
//    }
//    else if ($("#drpAction")[0].value == "Other" && $("#txt_OverrideComm")[0].value == "") {
//        alert("Please enter comment");
//        return false;
//    }

//    $.ajax({
//        type: "POST",
//        url: "AjaxFunction_DM.aspx/Update_Comment_Status",
//        data: '{Id: "' + $("#txt_Overrideid")[0].value.trim() + '",Comment: "' + $("#txt_OverrideComm")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",QueryAction: "' + $("#drpAction")[0].value + '"}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            if (data.d == 'Object reference not set to an instance of an object.') {
//                alert("Session Expired");
//                var url = "SessionExpired.aspx";
//                $(location).attr('href', url);
//            }
//            else {
//                alert(data.d);
//                $(location).attr('href', window.location.href);
//            }
//        },
//        failure: function (response) {
//            if (response.d == 'Object reference not set to an instance of an object.') {
//                alert("Session Expired");
//                var url = "SessionExpired.aspx";
//                $(location).attr('href', url);
//            }
//            else {
//                alert("Contact administrator not suceesfully updated")
//            }
//        }
//    });
//}




function UpdateOverrideData() {

    if ($("#drpAction")[0].value == "0") {
        alert("Please select reason");
        return false;
    }
    else if ($("#drpAction")[0].value == "Other" && $("#txt_OverrideComm")[0].value == "") {
        alert("Please enter comment");
        return false;
    }

    // Create JSON object to handle special characters properly
    var dataObj = {
        Id: $("#txt_Overrideid").val().trim(),
        Comment: $("#txt_OverrideComm").val().trim(),  // Preserve special characters
        QueryAction: $("#drpAction").val()
    };

    // Convert to JSON string
    var jsonData = JSON.stringify(dataObj);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Update_Comment_Status",
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
function CancelOverrideData() {
    $("#popup_Override").dialog('close');
    $("#drpoverrideReason").val("0");
    $("#drpAction").val("0");
    $("#txt_OverrideComm").val("");
    return false;
}