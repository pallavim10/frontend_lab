
function SAE_ShowOpenQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery_SAEID_RECID",
        data: '{SAEID: "' + SAEID + '",RECID:"' + RECID + '",MODULEID:"' + MODULEID + '"}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#divOpenAllQuery').html(data.d)

                $("#popup_OpenAllQuery").dialog({
                    title: "Query Details",
                    width: 1200,
                    height: 500,
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

function SAE_ShowAnsQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/ShowAnsQuery_SAEID_RECID",
        data: '{SAEID: "' + SAEID + '",RECID:"' + RECID + '",MODULEID:"' + MODULEID + '"}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#divAnsAllQuery').html(data.d)

                $("#popup_AnsAllQuery").dialog({
                    title: "Answered Query",
                    width: 1200,
                    height: 500,
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

function SAE_ShowClosedQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowClosedQuery_SAEID_RECID",
        data: '{SAEID: "' + SAEID + '",RECID:"' + RECID + '",MODULEID:"' + MODULEID + '"}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#DivClosedAllQuery').html(data.d)

                $("#popup_ClosedAllQuery").dialog({
                    title: "Closed Query",
                    width: 1200,
                    height: 500,
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
