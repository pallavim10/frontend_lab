$(document).ready(function () {
    $("#MainContent_lblContextualNotes").click(function () {

        ShowNotesPopUP();
        if ($('#MainContent_pop_Notes').hasClass("disp-none")) {
            $('#MainContent_pop_Notes').removeClass("disp-none");
            $('#MainContent_pop_Notes').addClass("disp-block");
            $('#MainContent_gridNotes').removeClass("disp-none");
            $('#MainContent_gridNotes').addClass("disp-block");
        }
        else {
            $('#MainContent_pop_Notes').removeClass("disp-block");
            $('#MainContent_pop_Notes').addClass("disp-none");
            $('#MainContent_gridNotes').removeClass("disp-none");
            $('#MainContent_gridNotes').addClass("disp-block");
        }
    });
});

$(document).ready(function () {
    $("#MainContent_lblNotes1").click(function () {

        ShowNotesPopUP();
        if ($('#MainContent_pop_Notes').hasClass("disp-none")) {
            $('#MainContent_pop_Notes').removeClass("disp-none");
            $('#MainContent_pop_Notes').addClass("disp-block");
            $('#MainContent_gridNotes').removeClass("disp-none");
            $('#MainContent_gridNotes').addClass("disp-block");
        }
        else {
            $('#MainContent_pop_Notes').removeClass("disp-block");
            $('#MainContent_pop_Notes').addClass("disp-none");
            $('#MainContent_gridNotes').removeClass("disp-none");
            $('#MainContent_gridNotes').addClass("disp-block");
        }
    });
});

function ShowNotesPopUP() {
    $("#MainContent_pop_Notes").dialog({
        title: "Add Contextual Notes",
        width: 850,
        height: 540,
        modal: true,
        top: "70px",
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });
}

function Update_Notes() {
    if ($("#MainContent_txtContextualNotes").val() == '') {
        alert("Please enter Contextual Notes");
        return false;
    }

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    var PVID = $("#MainContent_hdnPVID").val();
    var RECID = $("#MainContent_hdnRECID").val();
    var VISITNUM = params.VISITID;
    var MODULEID = params.MODULEID;
    var MODULENAME = params.MODULENAME;
    var NOTES = $("#MainContent_txtContextualNotes").val();
    var SUBJID = params.SUBJID;
    var ID = $("#MainContent_hdnNotesId").val();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/UPDATE_NOTE",
        data: '{PVID: "' + PVID + '",RECID: "' + RECID + '",VISITNUM: "' + VISITNUM + '",MODULEID: "' + MODULEID + '",NOTES: "' + NOTES + '",SUBJID:"' + SUBJID + '",ID:"' + ID + '",MODULENAME:"' + MODULENAME + '"}',
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
                $("#MainContent_txtContextualNotes").val('');
                $("#MainContent_hdnNotesId").val('');

                $("#MainContent_btnGetNotes").click();
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

function Cancel_Notes() {
    $("#MainContent_txtContextualNotes").val('');
    $("#MainContent_hdnNotesId").val('');
}

function GetNotes(element) {
    var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
    var Notes = $(element).closest('tr').find('td:eq(1)').find('span').html();

    $("#MainContent_hdnNotesId").val(ID);
    $("#MainContent_txtContextualNotes").val(Notes);
}

function DeleteNotes(element) {
    var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/Delete_NOTE",
        data: '{ID:"' + ID + '"}',
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
                location.reload(true)
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