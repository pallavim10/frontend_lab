
var TABLENAME;
function showAuditTrail(TABLENAME, element) {

    var ID = $(element).closest('tr').find('td').eq(0).text().trim();
    var TABLE_NAME = TABLENAME;

    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/IWRS_showAuditTrail",
        data: '{TABLENAME: "' + TABLE_NAME + '",ID: "' + ID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {

                if (TABLENAME == 'NIWRS_NAV_PARENT') {
                    $('#DivAuditTrail_NEW').html(data.d)
                }
                else {
                    $('#DivAuditTrail').html(data.d)
                }
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

    if (TABLENAME == 'NIWRS_NAV_PARENT') {
        $("#popup_AuditTrail_NEW").dialog({
            title: "Audit Trail",
            width: 900,
            height: 450,
            modal: true,
            buttons: {
                "Close": function () { $(this).dialog("close"); }
            }
        });
    }
    else {
        $("#popup_AuditTrail").dialog({
            title: "Audit Trail",
            width: 900,
            height: 450,
            modal: true,
            buttons: {
                "Close": function () { $(this).dialog("close"); }
            }
        });
    }

    return false;
}