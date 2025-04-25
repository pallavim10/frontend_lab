
function FillOPENQUERIES() {
    var Count = 0;
    $("#MainContent_grdOpenQuers tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".AQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function ShowOpenQuery(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowOpenQuery",
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
                $('#divOpenQuery').html(data.d)

                $("#popup_OpenQuery").dialog({
                    title: "Open Query",
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

function FillAnsQUERIES() {
    var Count = 0;
    $("#MainContent_grdAnsQueries tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".AWSQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function ShowAnsQuery(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowAnsQuery",
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
                $('#divAnsQuery').html(data.d)

                $("#popup_AnsQuery").dialog({
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

function FillCLOSEQUERIES() {
    var Count = 0;
    $("#MainContent_grdcloseQueries tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".CQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function ShowClosedQuery(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowClosedQuery",
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
                $('#DivClosedQuery').html(data.d)

                $("#popup_ClosedQuery").dialog({
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

function ShowQueryComment(element) {

    var ID = $(element).closest('tr').find('td').eq(0).text().trim();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowQueryComment",
        data: '{ID: "' + element + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#divShowQryComment').html(data.d)

                $("#Popup_ShowQryComment").dialog({
                    title: "Query Comment",
                    width: 700,
                    height: 350,
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

function OpenManualQuery(element) {

    var VARIABLENAME = $(element).closest('tr').find('td').eq(0).text().trim();
    var TABLENAME = $('#MainContent_hfTablename').val()
    var MODULENAME = $('#MainContent_lblModuleName').text()
    var FIELDNAME = $(element).closest('tr').find('td').eq(2).text().trim();

    $("#txtMQ_TableName").text(TABLENAME);
    $("#txtMQ_VariableName").text(VARIABLENAME);
    $("#lblMQModuleName").text(MODULENAME);
    $("#lblMQFieldName").text(FIELDNAME);

    $("#popup_MQ").dialog({
        title: "Raise Manual Query",
        width: 600,
        height: 250,
        modal: true,
    });

    return false;
}

function Cancel_MQ() {
    $("#popup_MQ").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}

function Save_MQ() {
    
    if ($('#txtMQComment').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var VARIABLENAME = $("#txtMQ_VariableName").text();
    var TABLENAME = $("#txtMQ_TableName").text();
    var MODULENAME = $("#lblMQModuleName").text();
    var FIELDNAME = $("#lblMQFieldName").text();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    let VISITNUM = params.VISITID;
    let VISIT = params.VISIT;
    let MODULEID = params.MODULEID;
    let INVID = params.INVID;

    // Prepare the data object
    var dataObj = {
        PVID: PVID,
        RECID: RECID,
        SUBJID: SUBJID,
        QUERYTEXT: $("#txtMQComment").val().trim(),   // Preserves special characters
        MODULENAME: MODULENAME,
        FIELDNAME: FIELDNAME,
        TABLENAME: TABLENAME,
        VARIABLENAME: VARIABLENAME,
        VISITNUM: VISITNUM,
        VISIT: VISIT,
        MODULEID: MODULEID,
        INVID: INVID
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Save_MQ",
/*        data: '{PVID: "' + PVID + '",RECID: "' + RECID + '",SUBJID: "' + SUBJID + '",QUERYTEXT: "' + $("#txtMQComment")[0].value + '",MODULENAME:"' + MODULENAME + '",FIELDNAME: "' + FIELDNAME + '",TABLENAME:"' + TABLENAME + '",VARIABLENAME: "' + VARIABLENAME + '",VISITNUM:"' + VISITNUM + '",VISIT:"' + VISIT + '",MODULEID:"' + MODULEID + '",INVID:"' + INVID + '"}',*/
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

function ShowOpenQuery_PVID_RECID(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowOpenQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function ShowOpenQuery_PVID_RECID_DELETED(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowOpenQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function ShowAnsQuery_PVID_RECID(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowAnsQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function ShowAnsQuery_PVID_RECID_DELETED(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowAnsQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function ShowClosedQuery_PVID_RECID(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowClosedQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function ShowClosedQuery_PVID_RECID_DELETED(element) {

    var PVID = ""; var RECID = "";

    var PVID = $(element).closest('tr').find('td:eq(4)').html();
    var RECID = $(element).closest('tr').find('td:eq(5)').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowClosedQuery_PVID_RECID",
        data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',

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

function Show_MM_QueryHistory(element) {

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Show_MM_QueryHistory",
        data: '{ID: "' + element + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#divShow_MM_QryHistory').html(data.d)

                $("#Popup_Show_MM_QryHistory").dialog({
                    title: "MM Query History",
                    width: 850,
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