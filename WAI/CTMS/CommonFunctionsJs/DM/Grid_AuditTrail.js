function FillAuditDetails() {
    var count = 0;
    $("#MainContent_grdAUDITTRAILDETAILS tr").each(function () {
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

function showAuditTrail(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();


    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/showAuditTrail",
        data: '{Variablename: "' + Variablename + '",PVID: "' + PVID + '",RECID:"' + RECID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#DivAuditTrail').html(data.d)
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

    $("#popup_AuditTrail").dialog({
        title: "Audit Trail",
        width: 900,
        height: 450,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}

function showAuditTrail_All(element) {

    var PVID = $("#MainContent_hdnPVID").val()
    var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();

    if (RECID == "") {
        RECID = $(element).closest('tr').find('td:eq(7)').find('span').html();
    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Get_ALL_AUDIT_BY_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID: "' + RECID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#DivAuditTrailALL').html(data.d)

                $("#popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 950,
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

function showAuditTrail_All_LIST_ENTRY(element) {

    var PVID = $("#MainContent_hdnPVID").val()
    var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Get_ALL_AUDIT_BY_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID: "' + RECID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#DivAuditTrailALL').html(data.d)

                $("#popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 950,
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

function showAuditTrail_All_LIST_ENTRY_DELETED(element) {

    var PVID = $("#MainContent_hdnPVID").val()
    var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Get_ALL_AUDIT_BY_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID: "' + RECID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#DivAuditTrailALL').html(data.d)

                $("#popup_AuditTrailALL").dialog({
                    title: "Audit Trail",
                    width: 950,
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