$(".Datatable").dataTable({
    "bSort": true,
    "ordering": true,
    "bDestroy": true,
    "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
    stateSave: true,
    fixedHeader: true
});

function ViewOtherDetails(element, LISTING_ID, FIELDID) {
    var VALUE = $(element).text();
    var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    if ($(element).text().trim() != '') {
        var test = "MM_LISTING_DATA_Other_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID;
        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
        window.open(test, '_blank', strWinProperty);
        return false;
    }
}


function ViewEventDetails(element, LISTING_ID, FIELDID) {
    var VALUE = $(element).text();
    var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    if ($(element).text().trim() != '') {
        var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID;
        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
        window.open(test, '_blank', strWinProperty);
        return false;
    }
}



function ViewLabDetails(element, LISTING_ID, FIELDID) {
    var VALUE = $(element).text();
    var TEST = $(element).closest('tr').find('td:eq(6)').text().trim();
    var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    if ($(element).text().trim() != '') {
        var test = "MM_LISTING_LAB_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&PVID=" + PVID + "&RECID=" + RECID + "&TEST=" + TEST;
        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
        window.open(test, '_blank', strWinProperty);
        return false;
    }
}



function ViewSubjectDetails(element, LISTING_ID) {
    var VALUE = $(element).text();
    var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
    window.open(test, '_blank', strWinProperty);
    return false;
}



function ViewVisitDetails(element, LISTING_ID) {
    var VALUE = $(element).text();
    var SUBJID = $(element).closest('tr').find('td:eq(3)').text();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    var test = "MM_LISTING_DATA_VISIT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID;;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=620,width=1300";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ViewEventDetails_Other(element, LISTING_ID, PREV_LISTID) {
    var VALUE = $('#hdnValue').val();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SUBJID = $('#MainContent_hdnSUBJID').val().trim();
    }
    else {
        var SUBJID = $('#hdnSUBJID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PREV_LISTID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var PREV_LISTID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var PREV_LISTID = $('#hdnlistid').val();
    }

    var FIELDID = $('#hdnFIELDID').val();
    var TYPE = $('#hdnTYPE').val();

    var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&TYPE=" + TYPE;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ViewLabDetails_Other(element, LISTING_ID, PREV_LISTID) {
    var VALUE = $('#hdnValue').val();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SUBJID = $('#MainContent_hdnSUBJID').val().trim();
    }
    else {
        var SUBJID = $('#hdnSUBJID').val().trim();
    }

    var FIELDID = $('#hdnFIELDID').val();
    var TYPE = $('#hdnTYPE').val();

    var test = "MM_LISTING_DATA_DETAILS.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&SUBJID=" + SUBJID + "&PREV_LISTID=" + PREV_LISTID + "&FIELDID=" + FIELDID + "&TYPE=" + TYPE;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ViewSubjectDetails_Other(element, LISTING_ID, PREV_LISTID) {
    var VALUE = $('#hdnValue').val();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SUBJID = $('#MainContent_hdnSUBJID').val().trim();
    }
    else {
        var SUBJID = $('#hdnSUBJID').val().trim();
    }

    var test = "MM_LISTING_DATA_SUBJECT.aspx?VALUE=" + VALUE + "&LISTING_ID=" + LISTING_ID + "&PREV_LISTID=" + PREV_LISTID + "&SUBJID=" + SUBJID;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1300";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ShowComments(element) {

    var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnlistid').val();
    }

    var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ShowHistory(element) {

    var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    var test = "MM_LISTING_HISTORY.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1000,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function ShowComments_PEER(element) {

    var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnlistid').val();
    }

    var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + Subject + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER";

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}


function ShowComments_btn(element) {

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SUBJID = $('#MainContent_hdnSUBJID').val().trim();
    }
    else {
        var SUBJID = $('#hdnSUBJID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PVID = $('#MainContent_hdnPVID').val().trim();
    }
    else {
        var PVID = $('#hdnPVID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var RECID = $('#MainContent_hdnRECID').val().trim();
    }
    else {
        var RECID = $('#hdnRECID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnPREV_LISTID').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnPREV_LISTID').val();
    }

    var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID;

    //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}


function ShowComments_PEER_btn(element) {

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SUBJID = $('#MainContent_hdnSUBJID').val().trim();
    }
    else {
        var SUBJID = $('#hdnSUBJID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var PVID = $('#MainContent_hdnPVID').val().trim();
    }
    else {
        var PVID = $('#hdnPVID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var RECID = $('#MainContent_hdnRECID').val().trim();
    }
    else {
        var RECID = $('#hdnRECID').val().trim();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnPREV_LISTID').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnPREV_LISTID').val();
    }

    var test = "MM_LISTING_COMMENTS.aspx?SUBJID=" + SUBJID + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&TYPE=PEER";

    //var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=370,width=550,resizable=no,left=850";
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}


function ShowComments_QUERY(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text()

    var test = "MM_QUERY_COMMENTS.aspx?ID=" + ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1000,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}


function ShowHistory_QUERY(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text()

    var test = "MM_QUERY_HISTORY.aspx?ID=" + ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1000,resizable=no";
    window.open(test, '_blank', strWinProperty);
    return false;
}


function ShowHistory_QUERY_Popup(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text()

    $.ajax({
        type: "POST",
        url: "AjaxFunction_MM.aspx/ShowHistory_QUERY_Popup",
        data: '{ID: "' + ID + '"}',
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
        title: "History",
        width: 900,
        height: 450,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;

}


function ShowComments_QUERY_Popup(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text()

    $.ajax({
        type: "POST",
        url: "AjaxFunction_MM.aspx/ShowComments_QUERY_Popup",
        data: '{ID: "' + ID + '"}',
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
        title: "Comments",
        width: 900,
        height: 450,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}