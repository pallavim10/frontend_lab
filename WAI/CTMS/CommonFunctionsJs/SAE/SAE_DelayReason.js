function Check_DelayReasonEntered(element) {

    if ($('#MainContent_txtReason').val() == '') {
        alert('Please Enter Reason');
        return false;
    }

};

function OpenDelayedReason() {

    var REASON = $('#MainContent_hdn_Delayed_Reason').val()
    var REASONBY = $('#MainContent_hdn_Delayed_ReasonBy').val()
    var DATETIMESERVER = $('#MainContent_hdn_Delayed_DTServer').val()
    var DATETIMEUSER = $('#MainContent_hdn_Delayed_DTUser').val()

    $("#lblDelayed_ReasonBy").text(REASONBY);
    $("#lblDelayed_DTServer").text(DATETIMESERVER);
    $("#lblDelayed_DTUser").text(DATETIMEUSER);
    $("#lblDelayedReason").text(REASON);

    $("#popup_Delayed_REASON").dialog({
        title: "Reason For Delay",
        width: 800,
        height: 320,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}

function OpenDeletedReq_Reason() {

    var REASON = $('#MainContent_hdn_DeletedReq_Reason').val()
    var REASONBY = $('#MainContent_hdn_DeletedReq_ReasonBy').val()
    var DATETIMESERVER = $('#MainContent_hdn_DeletedReq_DTServer').val()
    var DATETIMEUSER = $('#MainContent_hdn_DeletedReq_DTUser').val()

    $("#lblDeleted_Req_ReasonBy").text(REASONBY);
    $("#lblDeleted_Req_DTServer").text(DATETIMESERVER);
    $("#lblDeleted_Req_DTUser").text(DATETIMEUSER);
    $("#lblDeleted_Req_Reason").text(REASON);

    $("#popup_Deleted_Req_REASON").dialog({
        title: "Downgrading Request Reason",
        width: 800,
        height: 320,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}