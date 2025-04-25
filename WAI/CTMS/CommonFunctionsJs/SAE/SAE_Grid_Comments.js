function FillCommentsDetails() {
    var count = 0;

    $("#MainContent_SAE_grdComments tr").each(function () {
        if (count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val().trim();

            $(".CM_" + variableName).removeClass("disp-none");

            $(".CM_" + variableName).find("i").attr('style', 'color: darkcyan;font-size: 17px;');
        }
        count++;
    });

}

function SAE_ShowComments(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var FIELDNAME = $(element).closest('tr').find('td').eq(2).text().trim();
    var SAEID = $("#MainContent_hdnSAEID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();

    $("#lblSAE_VariableName").text(Variablename);
    $("#lblSAE_FieldName").text(FIELDNAME);

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let DELETED = params.DELETED;

    if (DELETED == "1") {
        $("#SAE_divAddComment").attr("style", "display: disp-none");
        $("#SAE_divAddComment").addClass("disp-none");
    }
    else {
        $("#SAE_divAddComment").removeClass("disp-none");
        $("#SAE_divAddComment").attr("style", "display: -webkit-inline-box;");
    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_ShowComments",
        data: '{Variablename: "' + Variablename + '",SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '",FIELDNAME:"' + FIELDNAME + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                $('#SAE_grdfieldComments').html(data.d);

                $("#SAE_popup_FieldComments").dialog({
                    title: "Comments",
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

function SAE_saveFieldComments() {

    if ($('#txtSAE_FieldComments').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var SAE = $("#MainContent_hdnSAE").val();
    var RECID = $("#MainContent_hdnRECID").val();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    let INVID = params.INVID;

    var variableName = $("#lblSAE_VariableName").text();
    var commnets = $("#txtSAE_FieldComments")[0].value;
    var fieldname = $("#lblSAE_FieldName").text();
    var MODULENAME = $("#MainContent_drpModule").find("option:selected").text();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    var STATUS = $("#MainContent_hdnStatus").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_SetFielsComments",
        data: '{VariableName: "' + variableName + '",SAEID: "' + SAEID + '",RECID:"' + RECID + '",SAE: "' + SAE + '",Comments: "' + commnets + '",FieldName: "' + fieldname + '",ModuleName: "' + MODULENAME + '",INVID: "' + INVID + '",SUBJID: "' + SUBJID + '",STATUS: "' + STATUS + '",MODULEID: "' + MODULEID + '"}',
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

function SAE_Show_Page_Comments(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val()

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let DELETED = params.DELETED;

    if (DELETED == "1") {

        $("#SAE_DivPageComment").attr("style", "display: disp-none");
        $("#SAE_DivPageComment").addClass("disp-none");

    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Show_Page_Comments",
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
                $('#SAE_grdPage_Comments').html(data.d);

                $("#SAE_popup_Page_Comments").dialog({
                    title: "Module Comments",
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

function SAE_Show_Page_Comments_All(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var MODULEID = $(element).closest('tr').find('td').find("span[id*='MODULEID']").text();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Show_Page_Comments",
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
                $('#SAE_grdPage_Comments_All').html(data.d);

                $("#SAE_popup_Page_Comments_All").dialog({
                    title: "Module Comments",
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

function SAE_SavePageComments() {

    if ($('#txtSAE_PageComments').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var SAE = $("#MainContent_hdnSAE").val();
    var RECID = $("#MainContent_hdnRECID").val();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    let INVID = params.INVID;

    var commnets = $("#txtSAE_PageComments")[0].value;
    var MODULENAME = $("#MainContent_drpModule").find("option:selected").text();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    var STATUS = $("#MainContent_hdnStatus").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/INSERT_PAGE_COMMENT",
        data: '{SAEID: "' + SAEID + '",RECID:"' + RECID + '",SAE: "' + SAE + '",Comments: "' + commnets + '",ModuleName: "' + MODULENAME + '",INVID: "' + INVID + '",SUBJID: "' + SUBJID + '",STATUS: "' + STATUS + '",MODULEID: "' + MODULEID + '"}',
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

function SAE_Show_Internal_Comments(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var MODULEID = "";

    if ($("#MainContent_drpModule").find("option:selected").val() != undefined) {
        MODULEID = $("#MainContent_drpModule").find("option:selected").val()
    } else {

        MODULEID = $(element).closest('tr').find('td').find("span[id*='MODULEID']").text();

        $("#SAE_DivInternalComment").attr("style", "display: disp-none");
        $("#SAE_DivInternalComment").addClass("disp-none");
    }

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let DELETED = params.DELETED;

    if (DELETED == "1") {

        $("#SAE_DivInternalComment").attr("style", "display: disp-none");
        $("#SAE_DivInternalComment").addClass("disp-none");

    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Show_Internal_Comments",
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
                $('#SAE_grdInternal_Comments').html(data.d);

                $("#SAE_popup_Internal_Comments").dialog({
                    title: "Internal Comments",
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

function SAE_Show_Internal_Comments_All(element) {

    var SAEID = $("#MainContent_hdnSAEID").val();
    var MODULEID = $(element).closest('tr').find('td').find("span[id*='MODULEID']").text();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/SAE_Show_Internal_Comments",
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
                $('#SAE_grdInternal_Comments_All').html(data.d);

                $("#SAE_popup_Internal_Comments_All").dialog({
                    title: "Internal Comments",
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

function SAE_SaveInternalComments() {

    if ($('#txtSAE_InternalComments').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var SAEID = $("#MainContent_hdnSAEID").val();
    var SAE = $("#MainContent_hdnSAE").val();
    var RECID = $("#MainContent_hdnRECID").val();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    let INVID = params.INVID;

    var commnets = $("#txtSAE_InternalComments")[0].value;
    var MODULENAME = $("#MainContent_drpModule").find("option:selected").text();
    var MODULEID = $("#MainContent_drpModule").find("option:selected").val();
    var STATUS = $("#MainContent_hdnStatus").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_SAE.aspx/INSERT_INTERNAL_COMMENT",
        data: '{SAEID: "' + SAEID + '",RECID:"' + RECID + '",SAE: "' + SAE + '",Comments: "' + commnets + '",ModuleName: "' + MODULENAME + '",INVID: "' + INVID + '",SUBJID: "' + SUBJID + '",STATUS: "' + STATUS + '",MODULEID: "' + MODULEID + '"}',
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

