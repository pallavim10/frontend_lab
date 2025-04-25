
function showAuditTrail_All(element) {
    //var PVID = "383-61-61001-28-10-1";
    //var RECID = "0";
    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Get_ALL_AUDIT_BY_PVID_RECID",
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