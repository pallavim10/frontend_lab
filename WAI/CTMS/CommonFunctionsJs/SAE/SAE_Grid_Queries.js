function FillOPENQUERIES() {
    var Count = 0;
    $("#MainContent_SAE_grdOpenQuers tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".AQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function FillAnsQUERIES() {
    var Count = 0;
    $("#MainContent_SAE_grdAnsQueries tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".AWSQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function FillCLOSEQUERIES() {
    var Count = 0;
    $("#MainContent_SAE_grdcloseQueries tr").each(function () {
        if (Count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();

            $(".CQ_" + variableName).removeClass("disp-none");
        }
        Count++;
    });
}

function SAE_ShowOpenQuery(element) {

    var VARIABLENAME = $(element).closest('tr').find('td').eq(0).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery",
        data: '{VARIABLENAME: "' + VARIABLENAME + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_divOpenQuery').html(data.d)

                $("#SAE_popup_OpenQuery").dialog({
                    title: "Open Query",
                    width: 900,
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

function SAE_ShowAnsQuery(element) {

    var VARIABLENAME = $(element).closest('tr').find('td').eq(0).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowAnsQuery",
        data: '{VARIABLENAME: "' + VARIABLENAME + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_divAnsQuery').html(data.d)

                $("#SAE_popup_AnsQuery").dialog({
                    title: "Answered Query",
                    width: 900,
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

function SAE_ShowClosedQuery(element) {

    var VARIABLENAME = $(element).closest('tr').find('td').eq(0).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowClosedQuery",
        data: '{VARIABLENAME: "' + VARIABLENAME + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_DivClosedQuery').html(data.d)

                $("#SAE_popup_ClosedQuery").dialog({
                    title: "Closed Query",
                    width: 1100,
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

function SAE_ShowQueryComment(element) {

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowQueryComment",
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
                $('#SAE_divShowQryComment').html(data.d)

                $("#SAE_Popup_ShowQryComment").dialog({
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

function SAE_ShowOpenQuery_SAEID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var MODULEID = "";

    if ($("#MainContent_drpModule").find("option:selected").val() != undefined) {
        MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    }
    else {
        MODULEID = $(element).closest('tr').find('td:eq(0)').find('span').html();
    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery_SAEID",
        data: '{SAEID: "' + SAEID + '",MODULEID: "' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_divOpenAllQuery').html(data.d)

                $("#SAE_OpenAllQuery").dialog({
                    title: "Open Query",
                    width: 1100,
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

function ShowOpenQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(7)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery_SAEID_RECID",
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
                $('#SAE_divOpenAllQuery').html(data.d)

                $("#SAE_OpenAllQuery").dialog({
                    title: "Open Query",
                    width: 1100,
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

function ShowOpenQuery_SAEID_RECID_DELETED(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowOpenQuery_SAEID_RECID",
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
                $('#SAE_divOpenAllQuery').html(data.d)

                $("#SAE_OpenAllQuery").dialog({
                    title: "Open Query",
                    width: 1100,
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

function ShowAnsQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();

    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowAnsQuery_SAEID_RECID",
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
                $('#SAE_divAnsAllQuery').html(data.d)

                $("#SAE_popup_AnsAllQuery").dialog({
                    title: "Answered Query",
                    width: 1100,
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

function ShowAnsQuery_SAEID_RECID_DELETED(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(8)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowAnsQuery_SAEID_RECID",
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
                $('#SAE_divAnsAllQuery').html(data.d)

                $("#SAE_popup_AnsAllQuery").dialog({
                    title: "Answered Query",
                    width: 1100,
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

function SAE_ShowAnsQuery_SAEID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();

    if ($("#MainContent_drpModule").find("option:selected").val() != undefined) {
        MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    }
    else {
        MODULEID = $(element).closest('tr').find('td:eq(0)').find('span').html();
    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowAnsQuery_SAEID",
        data: '{SAEID: "' + SAEID + '",MODULEID:"' + MODULEID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_divAnsAllQuery').html(data.d)

                $("#SAE_popup_AnsAllQuery").dialog({
                    title: "Answered Query",
                    width: 1100,
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

function ShowClosedQuery_SAEID_RECID(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();

    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowClosedQuery_SAEID_RECID",
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
                $('#SAE_DivClosedAllQuery').html(data.d)

                $("#SAE_popup_ClosedAllQuery").dialog({
                    title: "Closed Query",
                    width: 1100,
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

function ShowClosedQuery_SAEID_RECID_DELETED(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $(element).closest('tr').find('td:eq(9)').find('span').html();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowClosedQuery_SAEID_RECID",
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
                $('#SAE_DivClosedAllQuery').html(data.d)

                $("#SAE_popup_ClosedAllQuery").dialog({
                    title: "Closed Query",
                    width: 1100,
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

function OpenManualQuery(element) {

    var VARIABLENAME = $(element).closest('tr').find('td').eq(0).text().trim();
    var TABLENAME = $('#MainContent_hfTablename').val()
    var MODULENAME = $('#MainContent_lblModuleName').text()
    var FIELDNAME = $(element).closest('tr').find('td').eq(2).text().trim();

    $("#txtSAE_MQ_TableName").text(TABLENAME);
    $("#txtSAE_MQ_VariableName").text(VARIABLENAME);
    $("#lblSAEMQModuleName").text(MODULENAME);
    $("#lblSAEMQFieldName").text(FIELDNAME);

    $("#popup_SAE_MQ").dialog({
        title: "Raise Manual Query",
        width: 600,
        height: 250,
        modal: true,
    });

    return false;
}

function Save_SAE_MQ() {

    if ($('#txtSAEMQComment').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var VARIABLENAME = $("#txtSAE_MQ_VariableName").text();
    var TABLENAME = $("#txtSAE_MQ_TableName").text();
    var MODULENAME = $("#lblSAEMQModuleName").text();
    var FIELDNAME = $("#lblSAEMQFieldName").text();
    var MODULEID = "";

    if ($("#MainContent_drpModule").find("option:selected").val() != undefined) {
        MODULEID = $("#MainContent_drpModule").find("option:selected").val()
    } else {
        MODULEID = $(element).closest('tr').find('td').find("span[id*='MODULEID']").text();
    }

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    /*let MODULEID = params.MODULEID;*/

    const data = {
        SAEID: SAEID,
        RECID: RECID,
        SUBJID: SUBJID,
        QUERYTEXT: $("#txtSAEMQComment").val(),
        MODULENAME: MODULENAME,
        MODULEID: MODULEID,
        FIELDNAME: FIELDNAME,
        TABLENAME: TABLENAME,
        VARIABLENAME: VARIABLENAME
    };

    // Convert the object to a JSON string
    const jsonData = JSON.stringify(data);


    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/Save_SAE_MQ",
        /* data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",SUBJID: "' + SUBJID + '",QUERYTEXT: "' + $("#txtSAEMQComment")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",MODULENAME:"' + MODULENAME + '",MODULEID:"' + MODULEID + '",FIELDNAME: "' + FIELDNAME + '",TABLENAME:"' + TABLENAME + '",VARIABLENAME: "' + VARIABLENAME + '"}',*/

        data: jsonData,  // Send the JSON string
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

function Cancel_SAE_MQ() {
    $("#popup_SAE_MQ").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}

