function FillCommentsDetails() {
    var count = 0;

    $("#MainContent_grdComments tr").each(function () {
        if (count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val().trim();

            $(".CM_" + variableName).removeClass("disp-none");

            $(".CM_" + variableName).find("i").attr('style', 'color: darkcyan;font-size: 17px;');
        }
        count++;
    });

}

function ShowComments(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var FieldName = $(element).closest('tr').find('td').eq(2).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    $("#lblVariableName").text(Variablename);
    $("#lblFieldName").text(FieldName);

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let DELETED = params.DELETED;

    // Get the full path of the URL
    let path = window.location.pathname;

    // Extract the page name
    let pageName = path.substring(path.lastIndexOf('/') + 1);

    if (DELETED == "1" || $('#MainContent_hdnFreezeStatus').val() == '1' || $('#MainContent_hdnLockStatus').val() == '1' || pageName == "DM_DataEntry_INV_Read_Only.aspx" || pageName == "DM_DataEntry_MultipleData_INV_Read_Only.aspx") {
        $("#divAddComment").attr("style", "display: disp-none");
        $("#divAddComment").addClass("disp-none");
    }
    else {
        $("#divAddComment").removeClass("disp-none");
        $("#divAddComment").attr("style", "display: -webkit-inline-box;");
    }

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowComments",
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
                $('#grdfieldComments').html(data.d)
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

    $("#popup_FieldComments").dialog({
        title: "Comments",
        width: 900,
        height: 500,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}

function saveFieldComments() {

    if ($('#txtFieldComments').val() == '') {
        alert('Please enter comments');
        return false;
    }

    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let MODULEID = params.MODULEID;
    let VISITID = params.VISITID;
    let VISIT = params.VISIT;
    let MODULENAME = params.MODULENAME;
    let SUBJID = params.SUBJID;
    let INVID = params.INVID;


    // Create JSON object
    var dataObj = {
        VariableName: $("#lblVariableName").text().trim(),
        Comments: $("#txtFieldComments").val().trim(),   // Keep special characters
        FieldName: $("#lblFieldName").text().trim(),
        PVID: PVID,
        RECID: RECID,
        SUBJID: SUBJID,
        VISITID: VISITID,
        MODULEID: MODULEID,
        MODULENAME: MODULENAME,
        VISIT: VISIT,
        INVID: INVID
    };

    // Convert object to JSON string
    var jsonData = JSON.stringify(dataObj);


    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/SetFielsComments",
        //data: '{VariableName: "' + $("#lblVariableName").text() + '",Comments: "' + $("#txtFieldComments")[0].value.replace(/[^a-z0-9\s.]/gi, '') + '",FieldName: "' + $("#lblFieldName").text() + '",PVID: "' + PVID + '",RECID: "' + RECID + '",SUBJID: "' + SUBJID + '",VISITID: "' + VISITID + '",MODULEID: "' + MODULEID + '",MODULENAME:"' + MODULENAME + '",VISIT:"' + VISIT + '",INVID:"' + INVID + '"}',
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

function DM_SDV_ShowComments(element) {

    var Variablename = $(element).closest('tr').find('td').eq(0).text().trim();
    var FieldName = $(element).closest('tr').find('td').eq(1).text().trim();
    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();

    $("#lblVariableName").text(Variablename);
    $("#lblFieldName").text(FieldName);

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/ShowComments",
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
                $('#grdfieldComments').html(data.d)
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

    $("#popup_FieldComments").dialog({
        title: "Comments",
        width: 900,
        height: 500,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}