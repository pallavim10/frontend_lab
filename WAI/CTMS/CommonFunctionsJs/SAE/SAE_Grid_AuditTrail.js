function FillAuditDetails() {
    var count = 0;
    $("#MainContent_SAE_grdAUDITTRAILDETAILS tr").each(function () {
        if (count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val().trim();
            var REASON = $(this).find('td:eq(1)').find('input').val().trim();

            if (REASON == "Initial Entry") {

                $(".AD_" + variableName).removeClass("disp-none");
                $(".AD_" + variableName).find("i").attr('style', 'color: green;font-size: 17px;');
            }
            else {

                $(".AD_" + variableName).removeClass("disp-none");
                $(".AD_" + variableName).find("i").attr('style', 'color: red;font-size: 17px;');

            }
        }
        count++;
    });

}

function SAE_showAuditTrail(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_showAuditTrail",
        data: '{Variablename: "' + Variablename + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrail').html(data.d);

                $("#SAE_popup_AuditTrail").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}

function SAE_showAuditTrail_MR(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_showAuditTrail_MR",
        data: '{Variablename: "' + Variablename + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrail').html(data.d);

                $("#SAE_popup_AuditTrail").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}

function SAE_showAuditTrail_All_Deleted(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(9)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_SAEID_RECID",
        data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrailALL').html(data.d);

                $("#SAE_popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}

function SAE_showAuditTrail_All_Deleted_MR(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(9)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_SAEID_RECID_MR",
        data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrailALL').html(data.d);

                $("#SAE_popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}

function SAE_showAuditTrail_All(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(7)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_SAEID_RECID",
        data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrailALL').html(data.d);

                $("#SAE_popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}

function SAE_showAuditTrail_All_MR(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(7)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_SAEID_RECID_MR",
        data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivAuditTrailALL').html(data.d);

                $("#SAE_popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 1200,
                    height: 450,
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
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

    return false;
}