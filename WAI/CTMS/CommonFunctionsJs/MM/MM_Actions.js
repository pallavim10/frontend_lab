
function RaiseQuery(element) {

    var Subject = $(element).closest('tr').find('td:eq(3)').text().trim();
    var EventCode = $(element).closest('tr').find('td:eq(5)').text().trim();

    var Department = "Medical"

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTINGNAME = $('#MainContent_lblHeader').text();
    }
    else if (window.location.pathname == '/MM_LISTING_DATA_DETAILS.aspx' || window.location.pathname == '/MM_LISTING_DATA_SUBJECT.aspx' || window.location.pathname == '/MM_LISTING_DATA_VISIT.aspx') {
        var LISTINGNAME = $(element).closest('table').parent().parent().parent().parent().parent().parent().parent().prev().find('span').text();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTINGNAME = $(element).closest('table').parent().parent().parent().parent().parent().parent().parent().prev().find('span').text();
    }
    else {
        var LISTINGNAME = $('#lblHeader').text();
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var Source = $('#MainContent_hdnPrimMODULENAME').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var Source = $(element).closest('table').parent().parent().prev().val();
    }
    else if (window.location.pathname == '/MM_LISTING_DATA_DETAILS.aspx' || window.location.pathname == '/MM_LISTING_DATA_SUBJECT.aspx' || window.location.pathname == '/MM_LISTING_DATA_VISIT.aspx') {
        var Source = $('#hdnPrimMODULENAME').val();
    }
    else {
        var Source = $('#hdnPrimMODULENAME').val();
    }

    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (Source == '' || Source == undefined) {
        Source = Rule;
    }

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_LISTING_DATA_DETAILS.aspx' || window.location.pathname == '/MM_LISTING_DATA_SUBJECT.aspx' || window.location.pathname == '/MM_LISTING_DATA_VISIT.aspx') {
        var LISTING_ID = $('#hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnlistid').val();
    }

    var test = "NewQueryPopup.aspx?Subject=" + Subject + "&Department=" + Department + "&Source=" + Source + "&EventCode=" + EventCode + "&LISTINGNAME=" + LISTINGNAME + "&PVID=" + PVID + "&RECID=" + RECID + "&LISTING_ID=" + LISTING_ID + "&Refrence=" + EventCode;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=320,width=700,resizable=no";
    window.open(test, '_blank', strWinProperty);

    return false;

}


function ShowQueryList(element) {

    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    var test = "MM_PopUpQueryList.aspx?PVID=" + PVID + "&RECID=" + RECID;
    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=445,width=1200";
    window.open(test, '_blank', strWinProperty);

    return false;

}

function GenerateAutoQuery(element) {

    var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SOURCE = $('#MainContent_lblHeader').text();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var SOURCE = $(element).closest('table').parent().parent().parent().parent().parent().parent().parent().prev().find('span').text();
    }
    else {
        var SOURCE = $('#lblHeader').text();
    }

    var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
    var RECID = $(element).closest('tr').find('td:eq(2)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var LISTING_ID = $('#MainContent_hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_LISTING_DATA_DETAILS.aspx' || window.location.pathname == '/MM_LISTING_DATA_SUBJECT.aspx' || window.location.pathname == '/MM_LISTING_DATA_VISIT.aspx') {
        var LISTING_ID = $('#hdnlistid').val();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var LISTING_ID = $(element).closest('table').parent().parent().prev().val();
    }
    else {
        var LISTING_ID = $('#hdnlistid').val();
    }

    var REFERENCE = $(element).closest('tr').find('td:eq(5)').text().trim();

    $.ajax({
        type: "POST",
        url: "AjaxFunction_MM.aspx/MM_AutoQuery",
        data: '{SOURCE:"' + SOURCE + '",  PVID: "' + PVID + '", RECID: "' + RECID + '" ,LISTING_ID: "' + LISTING_ID + '",SUBJID: "' + SUBJID + '",REFERENCE: "' + REFERENCE + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {

            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnAutoQuery']").attr('id')).addClass("disp-none");

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

function MarkAsReviewed(element) {

    var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();

    if (window.location.pathname == '/MM_LISTING_DATA.aspx') {
        var SOURCE = $('#MainContent_lblHeader').text();
    }
    else if (window.location.pathname == '/MM_PatientReview.aspx' || window.location.pathname == '/MM_StudyReview.aspx') {
        var SOURCE = $(element).closest('table').parent().parent().parent().parent().parent().parent().parent().prev().find('h3').find('span').text();
    }
    else {
        var SOURCE = $('#lblHeader').text();
    }

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

    $.ajax({
        type: "POST",
        url: "AjaxFunction_MM.aspx/MM_Reviewed",
        data: '{SOURCE:"' + SOURCE + '",  PVID: "' + PVID + '", RECID: "' + RECID + '" ,LISTING_ID: "' + LISTING_ID + '",SUBJID: "' + SUBJID + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {

            var Listingstatus = res.d;

            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReview']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnAnotherReviewed']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewDone_PRIM']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewDone_SECOND']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnPeerReview']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewQuery']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewPatientRev']").attr('id')).addClass("disp-none");
            $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnDataChanged']").attr('id')).addClass("disp-none");

            if (Listingstatus != 'Not Reviewed') {

                if (Listingstatus == 'Reviewed with Peer View') {

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnPeerReview']").attr('id')).removeClass("disp-none");

                }
                else if (Listingstatus == 'Query and Reviewed') {

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewQuery']").attr('id')).removeClass("disp-none");

                }
                else if (Listingstatus == 'Data Changed') {

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnDataChanged']").attr('id')).removeClass("disp-none");

                }
                else if (Listingstatus == 'Primary Reviewed') {

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewDone_PRIM']").attr('id')).removeClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewDone_PRIM']").attr('id')).removeAttr("onclick");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewDone_PRIM']").attr('id')).removeAttr("href");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnReviewDone_PRIM']").attr('id')).addClass("aspNetDisabled");

                }
                else if (Listingstatus == 'Secondary Reviewed') {

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("span[id*='lbtnReviewDone_SECOND']").attr('id')).removeClass("disp-none");

                }

            }
            else {

                alert("This data can not be reviewed.");

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